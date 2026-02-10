namespace Flasharp;

public class App
{
    private HttpServer _server;

    public App()
    {
        _server = new();
    }

    public async Task Listen(int port, Action? action = null)
    {
        action!();

        await _server.Start(port);
    }

    public void Get(string path, Func<Request, Response, Task<Response>> handler)
    {
        _server.RegisterRoute("GET", path, handler);
    }

    public void Post(string path, Func<Request, Response, Task<Response>> handler)
    {
        _server.RegisterRoute("POST", path, handler);
    }

    public void Put(string path, Func<Request, Response, Task<Response>> handler)
    {
        _server.RegisterRoute("PUT", path, handler);
    }

    public void Patch(string path, Func<Request, Response, Task<Response>> handler)
    {
        _server.RegisterRoute("PATCH", path, handler);
    }

    public void Delete(string path, Func<Request, Response, Task<Response>> handler)
    {
        _server.RegisterRoute("DELETE", path, handler);
    }
}