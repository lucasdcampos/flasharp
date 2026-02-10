using System.Threading.Tasks;

namespace Flasharp;

public class App
{
    private HttpServer _server;

    public App() => _server = new();

    public async Task Listen(int port, Action? action = null)
    {
        action?.Invoke();
        await _server.Start(port);
    }

    public void Use(string prefix, Router router)
    {
        foreach (var route in router.RouteDefinitions)
        {
            var fullPath = $"{prefix.TrimEnd('/')}/{route.Path.TrimStart('/')}";
            var allRouteMiddlewares = new List<Action<Request, Response>>(router.RouterMiddlewares);
            allRouteMiddlewares.AddRange(route.Middlewares);

            _server.RegisterRoute(route.Method, fullPath, route.Handler, allRouteMiddlewares);
        }
    }

    // GET
    public void Get(string path, Func<Request, Response, Task<Response>> h) => Map("GET", path, null, h);
    public void Get(string path, Action<Request, Response> m1, Func<Request, Response, Task<Response>> h) => Map("GET", path, new() { m1 }, h);
    public void Get(string path, Action<Request, Response> m1, Action<Request, Response> m2, Func<Request, Response, Task<Response>> h) => Map("GET", path, new() { m1, m2 }, h);

    // POST
    public void Post(string path, Func<Request, Response, Task<Response>> h) => Map("POST", path, null, h);
    public void Post(string path, Action<Request, Response> m1, Func<Request, Response, Task<Response>> h) => Map("POST", path, new() { m1 }, h);
    public void Post(string path, Action<Request, Response> m1, Action<Request, Response> m2, Func<Request, Response, Task<Response>> h) => Map("POST", path, new() { m1, m2 }, h);

    // PUT
    public void Put(string path, Func<Request, Response, Task<Response>> h) => Map("PUT", path, null, h);
    public void Put(string path, Action<Request, Response> m1, Func<Request, Response, Task<Response>> h) => Map("PUT", path, new() { m1 }, h);

    // PATCH
    public void Patch(string path, Func<Request, Response, Task<Response>> h) => Map("PATCH", path, null, h);
    public void Patch(string path, Action<Request, Response> m1, Func<Request, Response, Task<Response>> h) => Map("PATCH", path, new() { m1 }, h);

    // DELETE
    public void Delete(string path, Func<Request, Response, Task<Response>> h) => Map("DELETE", path, null, h);
    public void Delete(string path, Action<Request, Response> m1, Func<Request, Response, Task<Response>> h) => Map("DELETE", path, new() { m1 }, h);

    // Método genérico para suportar listas longas de middlewares manualmente
    public void Map(string method, string path, List<Action<Request, Response>>? middlewares, Func<Request, Response, Task<Response>> handler)
    {
        _server.RegisterRoute(method, path, handler, middlewares);
    }
}