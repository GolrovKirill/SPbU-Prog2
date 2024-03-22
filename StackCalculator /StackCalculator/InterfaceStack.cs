namespace StackCalculator;

public interface InterfaceStack
{
    double Pop(); //  Извлекает и возвращает верхнее рациональное число стека
    void Push(double element); // Добавление нового рациональого числа в верх стека
    bool Count(); // Проверяет что стек пуст, true - пустой
}