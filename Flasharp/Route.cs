namespace Flasharp;

public class Route
{
    public required string[] Segments;
    public required Func<Request, Response, Task<Response>> Handler;
}