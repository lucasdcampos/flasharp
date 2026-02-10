namespace Flasharp;

public class Router
{
    internal List<(string Method, string Path, Action<Request, Response>[] Middlewares, Func<Request, Response, Task<Response>> Handler)> RouteDefinitions { get; } = new();
    internal List<Action<Request, Response>> RouterMiddlewares { get; } = new();

    public void Use(Action<Request, Response> middleware) => RouterMiddlewares.Add(middleware);

    // GET
    public void Get(string path, Func<Request, Response, Task<Response>> h) 
        => AddRoute("GET", path, Array.Empty<Action<Request, Response>>(), h);
    public void Get(string path, Action<Request, Response> m1, Func<Request, Response, Task<Response>> h) 
        => AddRoute("GET", path, new[] { m1 }, h);
    public void Get(string path, Action<Request, Response> m1, Action<Request, Response> m2, Func<Request, Response, Task<Response>> h) 
        => AddRoute("GET", path, new[] { m1, m2 }, h);

    // POST
    public void Post(string path, Func<Request, Response, Task<Response>> h) 
        => AddRoute("POST", path, Array.Empty<Action<Request, Response>>(), h);
    public void Post(string path, Action<Request, Response> m1, Func<Request, Response, Task<Response>> h) 
        => AddRoute("POST", path, new[] { m1 }, h);
    public void Post(string path, Action<Request, Response> m1, Action<Request, Response> m2, Func<Request, Response, Task<Response>> h) 
        => AddRoute("POST", path, new[] { m1, m2 }, h);

    // PUT
    public void Put(string path, Func<Request, Response, Task<Response>> h) 
        => AddRoute("PUT", path, Array.Empty<Action<Request, Response>>(), h);
    public void Put(string path, Action<Request, Response> m1, Func<Request, Response, Task<Response>> h) 
        => AddRoute("PUT", path, new[] { m1 }, h);

    // DELETE
    public void Delete(string path, Func<Request, Response, Task<Response>> h) 
        => AddRoute("DELETE", path, Array.Empty<Action<Request, Response>>(), h);
    public void Delete(string path, Action<Request, Response> m1, Func<Request, Response, Task<Response>> h) 
        => AddRoute("DELETE", path, new[] { m1 }, h);

    private void AddRoute(string method, string path, Action<Request, Response>[] middlewares, Func<Request, Response, Task<Response>> handler)
    {
        RouteDefinitions.Add((method, path, middlewares, handler));
    }
}