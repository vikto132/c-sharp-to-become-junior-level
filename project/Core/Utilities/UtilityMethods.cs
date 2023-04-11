using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System;

namespace Core.Utilities;

public static class UtilityMethods
{
    public static bool IsValidEmail(string email)
    {
        var rx = new Regex(
            @"^[-!#$%&'*+/0-9=?A-Z^_a-z{|}~](\.?[-!#$%&'*+/0-9=?A-Z^_a-z{|}~])*@[a-zA-Z](-?[a-zA-Z0-9])*(\.[a-zA-Z](-?[a-zA-Z0-9])*)+$");
        return rx.IsMatch(email);
    }

    public static bool IsValidStaffEmail(string email)
    {
        return IsValidEmail(email);
    }

    public static bool IsValidContact(string value)
    {
        var regexPhone = new Regex(@"^[0-9]+$");
        return regexPhone.Match(value).Success;
    }

    public static bool IsHtml(string value)
    {
        var regexHtml = new Regex(@"<\s*([^ >]+)[^>]*>.*?<\s*/\s*\1\s*>");
        return regexHtml.IsMatch(value);
    }

    public static string Combine(string baseUrl, string path)
    {
        return baseUrl.EndsWith("/") ? $"{baseUrl}{path}" : $"{baseUrl}/{path}";
    }

    public static string CombineUrl(string baseUrl, params string[] paths)
    {
        Guards.NotNullOrEmpty(baseUrl, nameof(baseUrl));
        if (paths.IsNullOrEmpty()) return baseUrl;
        var normalizedBaseUrl = baseUrl;
        var builder = new StringBuilder();
        builder.Append(normalizedBaseUrl);
        foreach (var path in paths)
        {
            if (!path.StartsWith("/")) builder.Append("/");
            builder.Append(path);
        }

        return builder.ToString();
    }

    public static string GeneratePricePrintCode(decimal salesAmount)
    {
        var salesAmountStr = $"{salesAmount}";
        if (string.IsNullOrWhiteSpace(salesAmountStr))
        {
            return string.Empty;
        }

        if (!salesAmountStr.Contains("."))
        {
            return salesAmountStr.Length switch
            {
                1 => $"0{salesAmountStr}00",
                < 4 => $"{salesAmountStr.PadRight(4, '0')}",
                _ => $"{salesAmountStr}00"
            };
        }

        var amountArr = salesAmountStr.Split(".");
        var beforeDot = amountArr[0];
        var afterDot = amountArr[1];
        if (afterDot.Length > 2)
        {
            afterDot = afterDot[..2];
        }

        return $"{beforeDot.PadLeft(2, '0')}{afterDot.PadRight(2, '0')}";
    }
}

public static class Guards
{
    public static void NotNullOrEmpty(string value, string name)
    {
        if (string.IsNullOrEmpty(value)) throw new ArgumentException($"'{name}' can not be null or empty.");
    }

    public static void NotNullOrEmpty<T>(IEnumerable<T> obj, string name)
    {
        if (obj.IsNullOrEmpty()) throw new ArgumentException($"'{name}' can not be null or empty.");
    }

    public static void PositiveNumber(int value, string name)
    {
        if (value < 0) throw new ArgumentException($"'{name}' can not be a negative number.");
    }

    public static void ValidEmail(string emailAddress)
    {
        NotNullOrEmpty(emailAddress, nameof(emailAddress));
        if (!UtilityMethods.IsValidEmail(emailAddress))
            throw new ArgumentException($"'{emailAddress}' is not a valid email.");
    }

    public static void ValidId(long id, string name)
    {
        if (id <= 0) throw new ArgumentException($"'{name}' is not valid id.");
    }

    public static void ValidAmount(decimal value, string name)
    {
        if (value < 0) throw new ArgumentException($"'{name}' must be a positive number.");
    }

    public static void NotNull(object obj, string name)
    {
        if (obj == null) throw new ArgumentNullException(name);
    }

    public static void ValidateEnum<TEnum>(int value, string fieldName)
    {
        if (!Enum.IsDefined(typeof(TEnum), value))
            throw new ArgumentException($"Invalid value for the type '{fieldName}'.");
    }
}