namespace LZW;

/// <summary>
/// Decodes the input file.
/// </summary>
public class DecoderLZW
{
    private class DecodeExpression
    {
        private const int SizeByte = 8;

        private readonly List<int> expression = [];

        private int current = 0;

        private int size = 0;

        public int SizeSymbol { get; set; } = 8;

        public int MaxSymbols { get; set; } = 256;

        private IEnumerable<byte> ByteRepresentation(byte bytes)
        {
            var representation = new byte[SizeByte];

            for (var i = SizeByte - 1; i >= 0; --i)
            {
                representation[i] = (byte)(bytes % 2);
                bytes /= 2;
            }

            return representation;
        }

        private void AddIntToExpression()
        {
            expression.Add(current);
            size = 0;
            current = 0;
        }

        /// <summary>
        /// Add byte in int.
        /// </summary>
        /// <returns>True if new int added.</returns>
        public bool AddByte(byte bytes)
        {
            var representation = ByteRepresentation(bytes);

            var number = false;

            foreach (var bit in representation)
            {
                current = (current * 2) + bit;

                ++size;

                if (size != SizeSymbol)
                {
                    continue;
                }

                AddIntToExpression();
                number = true;
            }

            return number;
        }

        /// <summary>
        /// Transform container into array of ints.
        /// </summary>
        /// <returns>Int array.</returns>
        public int[] IntArray()
        {
            AddIntToExpression();
            return expression.ToArray();
        }
    }

    /// <summary>
    /// Decode input expression.
    /// </summary>
    /// <returns>Decode string.</returns>
    public static byte[] Decoder(byte[] code)
    {
        ArgumentException.ThrowIfNullOrEmpty(nameof(code));

        var dictionary = new Dictionary<int, List<byte>>();

        for (var i = 0; i < 256; ++i)
        {
            dictionary.Add(i, [(byte)i]);
        }

        var currentNumber = 256;
        var encode = new DecodeExpression();

        foreach (var b in code)
        {
            if (currentNumber == encode.MaxSymbols)
            {
                encode.SizeSymbol += 1;
                encode.MaxSymbols *= 2;
            }

            if (encode.AddByte(b))
            {
                currentNumber += 1;
            }
        }

        var intArray = encode.IntArray();

        var decode = new List<byte>();
        currentNumber = 256;

        for (var i = 0; i < intArray.Length - 1; ++i)
        {
            decode.AddRange(dictionary[intArray[i]]);

            var codeExpression = new List<byte>();
            codeExpression.AddRange(dictionary[intArray[i]]);

            if (!dictionary.ContainsKey(intArray[i + 1]))
            {
                codeExpression.Add(codeExpression[0]);

                dictionary.Add(currentNumber, codeExpression);
            }
            else
            {
                codeExpression.Add(dictionary[intArray[i + 1]][0]);

                dictionary.Add(currentNumber, codeExpression);
            }

            currentNumber += 1;
        }

        return decode.ToArray();
    }
}