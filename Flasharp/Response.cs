using System.Net;
using System.Text;
using System.Text.Json;

namespace Flasharp;

public class Response
{
    public HttpListenerResponse HttpResponse { get; }
    
    public Response(HttpListenerResponse res)
    {
        HttpResponse = res;
    }

    public Response Status(int status)
    {
        HttpResponse.StatusCode = status;
        return this;
    }

    public async Task<Response> Text(string text)
    {
        byte[] buffer = System.Text.Encoding.UTF8.GetBytes(text);
        
        HttpResponse.ContentType = "text/plain; charset=utf-8";
        HttpResponse.ContentLength64 = buffer.Length;

        await HttpResponse.OutputStream.WriteAsync(buffer, 0, buffer.Length);

        return this;
    }

    public async Task<Response> Json(object data, JsonSerializerOptions? options = null)
    {
        string json = JsonSerializer.Serialize(data, options);

        byte[] buffer = Encoding.UTF8.GetBytes(json);

        HttpResponse.ContentType = "application/json; charset=utf-8";
        HttpResponse.ContentLength64 = buffer.Length;

        await HttpResponse.OutputStream.WriteAsync(buffer);

        return this;
    }

    public async Task<Response> Html(string html)
    {
        byte[] buffer = Encoding.UTF8.GetBytes(html);

        HttpResponse.ContentType = "text/html; charset=utf-8";
        HttpResponse.ContentLength64 = buffer.Length;

        await HttpResponse.OutputStream.WriteAsync(buffer, 0, buffer.Length);

        return this;
    }

    public Task<Response> Send(string text)
    {
        return Text(text);
    }

    public Task<Response> Send(object data)
    {
        return Json(data);
    }
}