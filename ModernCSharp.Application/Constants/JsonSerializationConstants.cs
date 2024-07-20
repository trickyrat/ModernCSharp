﻿using System.Text.Json;

namespace ModernCSharp.Application.Constants;

public static class JsonSerializationConstants
{
    public static JsonSerializerOptions DefaultOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = true
    };
}
