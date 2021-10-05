
using System;
public abstract class CustomExceptionBase : Exception
{
    public CustomExceptionBase() : base() { }
    public CustomExceptionBase(string message) : base(message)
    {
    }
}