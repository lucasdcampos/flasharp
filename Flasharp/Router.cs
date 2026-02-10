namespace Flasharp;

public class Router : RouteManager
{
    internal List<Action<Request, Response>> RouterMiddlewares { get; } = new();
    public void Use(Action<Request, Response> middleware) => RouterMiddlewares.Add(middleware);
}