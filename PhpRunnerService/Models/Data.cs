﻿using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
//using static liluclass.Converters.JsonCustomConverters;

namespace PhpRunnerService.Models
{
    public class DataPhpRunnerOne<T>
    {
        [JsonPropertyName("data")]
        public T Data { get; set; }

        [JsonPropertyName("success")]
        public bool Success { get; set; }
    }

    public class DataPhpRunnerMany<T>
    {
        [JsonPropertyName("data")]
        public List<T> Data { get; set; }

        [JsonPropertyName("success")]
        public bool Success { get; set; }
    }
}