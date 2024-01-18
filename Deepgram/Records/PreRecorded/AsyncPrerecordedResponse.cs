﻿namespace Deepgram.Records.PreRecorded;
public record AsyncPrerecordedResponse
{
    /// <summary>
    /// Id of transcription request returned when
    /// a CallBack has been supplied in request
    /// </summary>
    [JsonPropertyName("request_id")]
    public string? RequestId { get; set; }
}

