using Argo;

var app = new App();

app.Get("/index", async (req, res) =>
{
  return await res.Html("<h1>My website</h1>");
});

await app.Listen();