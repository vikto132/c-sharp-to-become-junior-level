using System;
using Core.Constants;
using Core.Converter;
using Newtonsoft.Json;

namespace Core.Models;

public class ExceptionDetails
{
    public ExceptionDetails(ApiResultCodes code, string errorMessage, object details = null)
    {
        Code = code;
        Message = errorMessage;
        Details = details ?? string.Empty;
    }

    [JsonProperty("code"), JsonConverter(typeof(EnumToNumberConverter))]
    public ApiResultCodes Code { get; set; }

    [JsonProperty("message", NullValueHandling = NullValueHandling.Ignore)]
    public string Message { get; set; }

    [JsonProperty("details", NullValueHandling = NullValueHandling.Ignore)]
    public object Details { get; set; }

    public override string ToString()
    {
        var str = $"({(int)Code} {Code}) {Message}";
        if (Details != null)
            str += Environment.NewLine + Details;
        return str;
    }
}