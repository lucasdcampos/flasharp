namespace Flasharp;

public class Route
{
    public string[] Segments { get; set; } = [];
    public Func<Request, Response, Task<Response>> Handler { get; set; } = null!;
    public List<Action<Request, Response>> Middlewares { get; set; } = new();
}
