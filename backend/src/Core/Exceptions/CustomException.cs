using System;

namespace Fwks.FwksService.Core.Exceptions;

public class CustomException : Exception
{
    public CustomException()
        : base("This is a custom exception message")
    {

    }
}
