using System;

public class SaldoInsuficienteException : Exception
{
    public SaldoInsuficienteException()
    {
    }

    public SaldoInsuficienteException(string message)
        : base(message)
    {
    }

    public SaldoInsuficienteException(string message, Exception inner)
        : base(message, inner)
    {
    }
}