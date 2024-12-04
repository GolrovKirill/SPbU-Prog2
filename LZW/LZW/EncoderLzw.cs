namespace LZW;

/// <summary>
/// Encodes the input file.
/// </summary>
public static class EncoderLZW
{
    private class EncodeExpression
    {
        private const int ByteSize = 8;

        private readonly List<byte> expression = [];

        private byte current = 0;

        private int size = 0;

        public int SizeSymbol { get; set; } = 8;

        public int MaxSymbols { get; set; } = 256;

        private IEnumerable<byte> ByteRepresentation(int number)
        {
            var representation = new byte[SizeSymbol];

            for (var i = SizeSymbol - 1; i >= 0; --i)
            {
                representation[i] = (byte)(number % 2);
                number /= 2;
            }

            return representation;
        }

        private void AddByteToExpression()
        {
            expression.Add(current);
            size = 0;
            current = 0;
        }

        /// <summary>
        /// Add number in expression.
        /// </summary>
        public void AddByte(int number)
        {
            var bytesRepresentation = ByteRepresentation(number);

            foreach (var bit in bytesRepresentation)
            {
                current = (byte)((current * 2) + bit);
                ++size;

                if (size == ByteSize)
                {
                    AddByteToExpression();
                }
            }
        }

        /// <summary>
        /// Get array of bytes.
        /// </summary>
        /// <returns>Array of bytes.</returns>
        public byte[] ByteArray()
        {
            current <<= ByteSize - size;
            AddByteToExpression();

            return expression.ToArray();
        }
    }

    /// <summary>
    /// Encode starts expression.
    /// </summary>
    /// <param name="text">Starts expression.</param>
    /// <returns>Encode expression.</returns>
    public static byte[] Encoder(byte[] text)
    {
        ArgumentException.ThrowIfNullOrEmpty(nameof(text));

        var dictionary = new TrieEncoder();

        for (var i = 0; i < 256; ++i)
        {
            dictionary.Add([(byte)i], i);
        }

        var currentNumber = 256;
        var code = new EncodeExpression();
        var encodeExpression = new List<byte>();

        foreach (var bytes in text)
        {
            encodeExpression.Add(bytes);

            if (dictionary.GetNumberValue(encodeExpression) != -1)
            {
                continue;
            }

            if (code.MaxSymbols == dictionary.Size())
            {
                code.SizeSymbol += 1;
                code.MaxSymbols *= 2;
            }

            dictionary.Add(encodeExpression, currentNumber);
            currentNumber += 1;

            encodeExpression.RemoveAt(encodeExpression.Count - 1);

            code.AddByte(dictionary.GetNumberValue(encodeExpression));

            encodeExpression.Clear();
            encodeExpression.Add(bytes);
        }

        code.AddByte(dictionary.GetNumberValue(encodeExpression));

        return code.ByteArray();
    }
}