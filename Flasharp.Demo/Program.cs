using Flasharp;

var app = new App();

app.Get("/", (req, res) =>
{
   return res.Send("API is up and running"); 
});

app.Get("/hello", (req, res) =>
{
   return res.Send(new {message = "Hello"}); 
});

app.Get("/error", (req, res) =>
{
   return res.Status(Status.InternalServerError).Send(new {message = "Oops!"}); 
});

app.Get("/index", (req, res) =>
{
   return res.Html("<h1>Hello!</h1>"); 
});

app.Get("/test", () => "Minimal route working");
app.Get("/test2", (req, res) => res.Send("Minimal route working"));

await app.Listen(3000);