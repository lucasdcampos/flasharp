namespace Flasharp;

public class App : RouteManager
{
    private HttpServer _server = new();

    public async Task Listen(int port, Action? action = null)
    {
        foreach (var route in RouteDefinitions)
            _server.RegisterRoute(route.Method, route.Path, route.Handler, route.Middlewares.ToList());

        action?.Invoke();
        await _server.Start(port);
    }

    public void Use(string prefix, Router router)
    {
        foreach (var route in router.RouteDefinitions)
        {
            var fullPath = $"{prefix.TrimEnd('/')}/{route.Path.TrimStart('/')}";
            var combined = new List<Action<Request, Response>>(router.RouterMiddlewares);
            combined.AddRange(route.Middlewares);
            _server.RegisterRoute(route.Method, fullPath, route.Handler, combined);
        }
    }
}