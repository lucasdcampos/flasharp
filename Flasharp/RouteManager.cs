namespace Flasharp;

public abstract class RouteManager
{
    internal List<(string Method, string Path, Action<Request, Response>[] Middlewares, Func<Request, Response, Task<Response>> Handler)> RouteDefinitions { get; } = new();

    #region GET
    public void Get(string path, Func<Request, Response, Task<Response>> h) => AddRoute("GET", path, [], h);
    public void Get(string path, Func<string> h) => AddRoute("GET", path, [], async (req, res) => await res.Text(h()));
    public void Get(string path, Func<Task<string>> h) => AddRoute("GET", path, [], async (req, res) => await res.Text(await h()));
    public void Get(string path, Func<object> h) => AddRoute("GET", path, [], async (req, res) => await res.Json(h()));
    public void Get(string path, Action<Request, Response> m1, Func<Request, Response, Task<Response>> h) => AddRoute("GET", path, [m1], h);
    public void Get(string path, Action<Request, Response> m1, Action<Request, Response> m2, Func<Request, Response, Task<Response>> h) => AddRoute("GET", path, [m1, m2], h);
    #endregion

    #region POST
    public void Post(string path, Func<Request, Response, Task<Response>> h) => AddRoute("POST", path, [], h);
    public void Post(string path, Func<string> h) => AddRoute("POST", path, [], async (req, res) => await res.Text(h()));
    public void Post(string path, Func<Task<string>> h) => AddRoute("POST", path, [], async (req, res) => await res.Text(await h()));
    public void Post(string path, Func<object> h) => AddRoute("POST", path, [], async (req, res) => await res.Json(h()));
    public void Post(string path, Action<Request, Response> m1, Func<Request, Response, Task<Response>> h) => AddRoute("POST", path, [m1], h);
    public void Post(string path, Action<Request, Response> m1, Action<Request, Response> m2, Func<Request, Response, Task<Response>> h) => AddRoute("POST", path, [m1, m2], h);
    #endregion

    #region PUT
    public void Put(string path, Func<Request, Response, Task<Response>> h) => AddRoute("PUT", path, [], h);
    public void Put(string path, Func<object> h) => AddRoute("PUT", path, [], async (req, res) => await res.Json(h()));
    public void Put(string path, Action<Request, Response> m1, Func<Request, Response, Task<Response>> h) => AddRoute("PUT", path, [m1], h);
    #endregion

    #region PATCH
    public void Patch(string path, Func<Request, Response, Task<Response>> h) => AddRoute("PATCH", path, [], h);
    public void Patch(string path, Func<object> h) => AddRoute("PATCH", path, [], async (req, res) => await res.Json(h()));
    public void Patch(string path, Action<Request, Response> m1, Func<Request, Response, Task<Response>> h) => AddRoute("PATCH", path, [m1], h);
    #endregion

    #region DELETE
    public void Delete(string path, Func<Request, Response, Task<Response>> h) => AddRoute("DELETE", path, [], h);
    public void Delete(string path, Func<string> h) => AddRoute("DELETE", path, [], async (req, res) => await res.Text(h()));
    public void Delete(string path, Action<Request, Response> m1, Func<Request, Response, Task<Response>> h) => AddRoute("DELETE", path, [m1], h);
    #endregion

    protected void AddRoute(string method, string path, Action<Request, Response>[] middlewares, Func<Request, Response, Task<Response>> handler)
    {
        RouteDefinitions.Add((method, path, middlewares, handler));
    }
}