﻿// Copyright 2021-2024 Deepgram .NET SDK contributors. All Rights Reserved.
// Use of this source code is governed by a MIT license that can be found in the LICENSE file.
// SPDX-License-Identifier: MIT

namespace Deepgram.Models.Live.v1;

public record SpeechStartedResponse
{
    /// <summary>
    /// TODO
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	[JsonPropertyName("type")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public LiveType? Type { get; set; } = LiveType.SpeechStarted;

    /// <summary>
    /// TODO
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	[JsonPropertyName("channel_index")]
    public int[]? Channel { get; set; }

    /// <summary>
    /// TODO
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
	[JsonPropertyName("timestamp")]
    public decimal? Timestamp { get; set; }
}
