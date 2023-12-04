﻿namespace Deepgram;

/// <summary>
/// Constructor that take a IHttpClientFactory
/// </summary>
/// <param name="apiKey">ApiKey used for Authentication Header and is required</param> 
/// <param name="httpClientFactory"><see cref="IHttpClientFactory"/> for creating instances of HttpClient for making Rest calls</param>
/// <param name="deepgramClientOptions"><see cref="DeepgramClientOptions"/> for HttpClient Configuration</param>
public class PrerecordedClient(string? apiKey, IHttpClientFactory httpClientFactory, DeepgramClientOptions? deepgramClientOptions = null)
    : AbstractRestClient(apiKey, httpClientFactory, nameof(PrerecordedClient), deepgramClientOptions)
{
    #region NoneCallBacks
    /// <summary>
    ///  Transcribe a file by providing a url 
    /// </summary>
    /// <param name="source">Url to the file that is to be transcribed</param>
    /// <param name="prerecordedSchema">Option for the transcription</param>
    /// <param name="deepgramRequestOptions">options specific to this request call</param>
    /// <returns>SyncPrerecordedResponse</returns>
    public async Task<SyncPrerecordedResponse> TranscribeUrlAsync(UrlSource source, PrerecordedSchema? prerecordedSchema)
    {
        VerifyNoCallBack(nameof(TranscribeUrlAsync), prerecordedSchema);
        var stringedOptions = QueryParameterUtil.GetParameters(Logger, prerecordedSchema);
        return await PostAsync<SyncPrerecordedResponse>(
            $"{Constants.LISTEN}?{stringedOptions}",
            CreatePayload(Logger, source));
    }


    /// <summary>
    /// Transcribes a file using the provided byte array
    /// </summary>
    /// <param name="source">file is the form of a byte[]</param>
    /// <param name="prerecordedSchema">Option for the transcription</param>
    /// <param name="deepgramRequestOptions">options specific to this request call</param>
    /// <returns>SyncPrerecordedResponse</returns>
    public async Task<SyncPrerecordedResponse> TranscribeFileAsync(byte[] source, PrerecordedSchema? prerecordedSchema)
    {
        VerifyNoCallBack(nameof(TranscribeFileAsync), prerecordedSchema);
        var stringedOptions = QueryParameterUtil.GetParameters(Logger, prerecordedSchema);
        var stream = new MemoryStream();
        stream.Write(source, 0, source.Length);
        return await PostAsync<SyncPrerecordedResponse>(
            $"{Constants.LISTEN}?{stringedOptions}",
            CreateStreamPayload(stream));
    }

    /// <summary>
    /// Transcribes a file using the provided stream
    /// </summary>
    /// <param name="source">file is the form of a stream</param>
    /// <param name="prerecordedSchema">Options for the transcription</param>
    /// <param name="deepgramRequestOptions">options specific to this request call</param>
    /// <returns>SyncPrerecordedResponse</returns>
    public async Task<SyncPrerecordedResponse> TranscribeFileAsync(Stream source, PrerecordedSchema? prerecordedSchema)
    {
        VerifyNoCallBack(nameof(TranscribeFileAsync), prerecordedSchema);
        var stringedOptions = QueryParameterUtil.GetParameters(Logger, prerecordedSchema);
        return await PostAsync<SyncPrerecordedResponse>(
            $"{Constants.LISTEN}?{stringedOptions}",
            CreateStreamPayload(source));
    }

    #endregion

    #region  CallBack Methods
    /// <summary>
    /// Transcribes a file using the provided byte array and providing a CallBack
    /// </summary>
    /// <param name="source">file is the form of a byte[]</param>
    /// <param name="callBack">CallBack url</param>    
    /// <param name="prerecordedSchema">Options for the transcription</param>
    /// <param name="deepgramRequestOptions">options specific to this request call</param>
    /// <returns>AsyncPrerecordedResponse</returns>
    public async Task<AsyncPrerecordedResponse> TranscribeFileCallBackAsync(byte[] source, string? callBack, PrerecordedSchema? prerecordedSchema)
    {
        VerifyOneCallBackSet(nameof(TranscribeFileCallBackAsync), callBack, prerecordedSchema);

        if (callBack != null)
            prerecordedSchema.Callback = callBack;
        var stringedOptions = QueryParameterUtil.GetParameters(Logger, prerecordedSchema);
        var stream = new MemoryStream();
        stream.Write(source, 0, source.Length);
        return await PostAsync<AsyncPrerecordedResponse>(
            $"{Constants.LISTEN}?{stringedOptions}",
            CreateStreamPayload(stream));
    }

    /// <summary>
    /// Transcribes a file using the provided stream and providing a CallBack
    /// </summary>
    /// <param name="source">file is the form of a stream</param>
    /// <param name="callBack">CallBack url</param>    
    /// <param name="prerecordedSchema">Options for the transcription</param>
    /// <param name="deepgramRequestOptions">options specific to this request call</param>
    /// <returns>AsyncPrerecordedResponse</returns>
    public async Task<AsyncPrerecordedResponse> TranscribeFileCallBackAsync(Stream source, string? callBack, PrerecordedSchema? prerecordedSchema)
    {
        VerifyOneCallBackSet(nameof(TranscribeFileCallBackAsync), callBack, prerecordedSchema);
        if (callBack != null)
            prerecordedSchema.Callback = callBack;
        var stringedOptions = QueryParameterUtil.GetParameters(Logger, prerecordedSchema);
        return await PostAsync<AsyncPrerecordedResponse>(
            $"{Constants.LISTEN}?{stringedOptions}",
            CreateStreamPayload(source));
    }

    /// <summary>
    /// Transcribe a file by providing a url and a CallBack
    /// </summary>
    /// <param name="source">Url to the file that is to be transcribed</param>
    /// <param name="callBack">CallBack url</param>    
    /// <param name="prerecordedSchema">Options for the transcription</param>
    /// <param name="deepgramRequestOptions">options specific to this request call</param>
    /// <returns>AsyncPrerecordedResponse</returns>
    public async Task<AsyncPrerecordedResponse> TranscribeUrlCallBackAsync(UrlSource source, string? callBack, PrerecordedSchema? prerecordedSchema)
    {
        VerifyOneCallBackSet(nameof(TranscribeUrlCallBackAsync), callBack, prerecordedSchema);

        if (callBack != null)
            prerecordedSchema.Callback = callBack;
        var stringedOptions = QueryParameterUtil.GetParameters(Logger, prerecordedSchema);
        return await PostAsync<AsyncPrerecordedResponse>(
            $"{Constants.LISTEN}?{stringedOptions}",
            CreatePayload(Logger, source));
    }
    #endregion

    #region CallbackChecks
    private static void VerifyNoCallBack(string method, PrerecordedSchema? prerecordedSchema)
    {
        if (prerecordedSchema != null && prerecordedSchema.Callback != null)
            throw new Exception($"CallBack cannot be provided as schema option to a synchronous transcription. Use {nameof(TranscribeFileCallBackAsync)} or {nameof(TranscribeUrlCallBackAsync)}");
    }

    private static void VerifyOneCallBackSet(string callingMethod, string? callBack, PrerecordedSchema? prerecordedSchema)
    {
        //check if no CallBack set in either callBack parameter or PrerecordedSchema
        if (prerecordedSchema.Callback == null && callBack == null)
            throw new Exception($"Either provide a CallBack url or set PrerecordedSchema.CallBack.  If no CallBack needed either {nameof(TranscribeUrlAsync)} or {nameof(TranscribeFileAsync)}");

        //check that only one CallBack is set in either callBack parameter or PrerecordedSchema
        if (!string.IsNullOrEmpty(prerecordedSchema.Callback) && !string.IsNullOrEmpty(callBack))
            throw new Exception("CallBack should be set in either the CallBack parameter or PrerecordedSchema.CallBack not in both.");
    }
    #endregion    
}
