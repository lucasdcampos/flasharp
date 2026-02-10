using System.Net;

namespace Flasharp;

public class HttpServer
{
    private HttpListener _listener { get; }
    private Dictionary<string, List<Route>> _routes;
    private List<Action<Request, Response>> _middlewareFunctions;

    public HttpServer()
    {
        _listener = new();
        _routes = new();
        _middlewareFunctions = new();
    }

    internal void AddMiddleware(Action<Request, Response> func)
    {
        _middlewareFunctions.Add(func);
    }

    internal void RegisterRoute(string method, string path, Func<Request, Response, Task<Response>> handler)
    {
        var segments = path
            .Trim('/')
            .Split('/', StringSplitOptions.RemoveEmptyEntries);

        _routes.TryAdd(method, new List<Route>());
        _routes[method].Add(new Route
        {
            Segments = segments,
            Handler = handler
        });
    }

    internal async Task Start(int port)
    {
        _listener.Prefixes.Add($"http://localhost:{port}/");
        _listener.Start();

        await HandleRequests();
    }

    private async Task HandleRequests()
    {
        while (true)
        {
            var context = await _listener.GetContextAsync();
            await HandleContext(context);
        }
    }

    private async Task HandleContext(HttpListenerContext context)
    {
        var request = context.Request;
        var response = context.Response;

        foreach(var f in _middlewareFunctions)
        {
            f(new Request(request), new Response(response));
        }

        var pathSegments = GetPathSegments(request);

        if (!_routes.TryGetValue(request.HttpMethod, out var routes))
        {
            WriteNotFound(response);
            return;
        }

        var handled = await TryHandleRoute(routes, request, response, pathSegments);

        if (!handled)
            WriteNotFound(response);
    }

    private async Task<bool> TryHandleRoute(
        List<Route> routes,
        HttpListenerRequest request,
        HttpListenerResponse response,
        string[] pathSegments)
    {
        foreach (var route in routes)
        {
            if (!MatchRoute(route, pathSegments, request, out var req))
                continue;

            try
            {
                await route.Handler(req, new Response(response));
            }
            catch
            {
                response.StatusCode = 500;
            }
            finally
            {
                response.Close();
            }

            return true;
        }

        return false;
    }

    private static bool MatchRoute(
        Route route,
        string[] pathSegments,
        HttpListenerRequest listenerRequest,
        out Request request)
    {
        request = null!;

        if (route.Segments.Length != pathSegments.Length)
            return false;

        var req = new Request(listenerRequest);

        for (int i = 0; i < pathSegments.Length; i++)
        {
            var segment = route.Segments[i];

            if (segment.StartsWith(":"))
            {
                req.Params[segment[1..]] = pathSegments[i];
            }
            else if (segment != pathSegments[i])
            {
                return false;
            }
        }

        request = req;
        return true;
    }

    private static void WriteNotFound(HttpListenerResponse response)
    {
        response.StatusCode = 404;
        response.Close();
    }

    private static string[] GetPathSegments(HttpListenerRequest request)
    {
        return request.Url!.AbsolutePath
            .Trim('/')
            .Split('/', StringSplitOptions.RemoveEmptyEntries);
    }
}