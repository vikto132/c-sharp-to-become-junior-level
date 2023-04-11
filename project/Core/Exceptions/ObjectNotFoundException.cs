using System;
using System.Runtime.Serialization;

namespace Core.Exceptions;

[Serializable]
public class ObjectNotFoundException : BusinessRuleException
{
    public ObjectNotFoundException() : base("Object not found exception") {}
        
    public ObjectNotFoundException(string msg) : base(msg) {}
        
    public ObjectNotFoundException(string msg, Exception e) : base(msg, e) {}
        
    protected ObjectNotFoundException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}