using System;
using System.Runtime.Serialization;

namespace Core.Exceptions;

[Serializable]
public class CoreException : SystemException
{
    public CoreException() : base("Core Exception")
    {
    }

    public CoreException(string msg) : base(msg)
    {
    }

    public CoreException(string msg, Exception e) : base(msg, e)
    {
    }
    
    protected CoreException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}