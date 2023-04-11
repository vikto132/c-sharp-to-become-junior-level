using System;
using System.Net;
using System.Runtime.Serialization;
using Core.Constants;
using Core.Models;

namespace Core.Exceptions;

[Serializable]
public class BusinessRuleException : CoreException
{
    public BusinessRuleException(ApiResultCodes code, string message, object detail = null) : base(message)
    {
        ResultDetails = new ExceptionDetails(code, message, detail);
    }

    public BusinessRuleException(ExceptionDetails resultDetails) : base(resultDetails.Message)
    {
        ResultDetails = resultDetails;
    }

    public BusinessRuleException(ExceptionDetails resultDetails, Exception innerException) : base(resultDetails.Message, innerException)
    {
        ResultDetails = resultDetails;
    }

    protected BusinessRuleException(string message, Exception innerException = null) : this(ApiResultCodes.Unknown, message, innerException)
    {
    }
        
    protected BusinessRuleException(ApiResultCodes code, string message, Exception innerException = null) : base(message, innerException)
    {
        ResultDetails = new ExceptionDetails(code, message);
    }

    protected BusinessRuleException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }

    public ExceptionDetails ResultDetails { get; private set; }

    public virtual HttpStatusCode GetHttpStatusCode() => HttpStatusCode.BadRequest;
}