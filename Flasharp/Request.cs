using System.Net;
using System.Text.Json;

namespace Flasharp;

public class Request
{
    public HttpListenerRequest HttpRequest { get; }

    public Dictionary<string, string> Params { get; }

    public string Method => HttpRequest.HttpMethod;

    public string Path => HttpRequest.Url?.AbsolutePath ?? "/";

    public string FullPath => HttpRequest.Url?.ToString() ?? "/";

    public string ContentType => HttpRequest.ContentType ?? string.Empty;

    public long ContentLength => HttpRequest.ContentLength64;

    public IReadOnlyDictionary<string, string> Headers =>
        HttpRequest.Headers.AllKeys!
            .ToDictionary(k => k!, k => HttpRequest.Headers[k]!);

    public IPAddress? RemoteIp =>
        HttpRequest.RemoteEndPoint?.Address;

    public Dictionary<string, string> QueryParams =>
        HttpRequest.QueryString.AllKeys!
            .ToDictionary(k => k!, k => HttpRequest.QueryString[k]!);

    public bool HasQuery => QueryParams.Count > 0;

    public bool IsJson =>
        ContentType.Contains("application/json", StringComparison.OrdinalIgnoreCase);

    private string? _body;

    public async Task<string> Body()
    {
        if (_body != null)
            return _body;

        using var reader = new StreamReader(HttpRequest.InputStream, HttpRequest.ContentEncoding);
        _body = await reader.ReadToEndAsync();

        return _body;
    }

    public async Task<T?> Body<T>(JsonSerializerOptions? options = null)
    {
        var body = await Body();

        if (string.IsNullOrWhiteSpace(body))
            return default;

        return JsonSerializer.Deserialize<T>(body, options);
    }

    public Request(HttpListenerRequest req)
    {
        HttpRequest = req;
        Params = new Dictionary<string, string>();
    }
}
