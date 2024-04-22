namespace UniqueList;

public class ListExceptionsAdd(string? message) : SystemException(message)
{
}

public class ListExceptionsRemove(string? message) : SystemException(message)
{
}

public class ListExceptionsChange(string? message) : SystemException(message)
{
}