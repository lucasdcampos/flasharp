using Flasharp;

var app = new App();

var logger = (Request req, Response res) => Console.WriteLine($"{req.Method} {req.Path}");
var auth = (Request req, Response res) => { /* logica de auth */ };

app.Get("/dashboard", logger, auth, async (req, res) => {
    return await res.Text("Dashboard OK");
});

await app.Listen(3000);