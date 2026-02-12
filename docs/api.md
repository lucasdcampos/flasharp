# Argo API

API Reference.

## 🚀 Quick Start

Initialize your application and start listening on a port in seconds.

```cs
using Argo;

var app = new App();

app.Get("/", () => "Hello, World!");

await app.Listen(3000, () => {
    Console.WriteLine("Server running on http://localhost:3000");
});
```

## 🛣️ Routing

Argo provides an expressive API for HTTP verbs. You can return strings, objects (JSON), or manual responses.

```cs
// Simple string response
app.Get("/hello", () => "Hello World");

// Automatic JSON serialization
app.Get("/user", () => new { Id = 1, Name = "Argo" });

// Async handling with Request and Response
app.Post("/data", async (req, res) => {
    var body = await req.Body();
    return await res.Status(Status.Created).Send(body);
});
```

## 🧩 Modularity

Organize your codebase using Router to group related routes under a common prefix.

```cs
var api = new Router();

api.Get("/v1", () => new { version = "1.0" });

app.Use("/api", api); // Routes will be accessible at /api/v1
```

## 🛡️ Middlewares

Inject logic before your handlers to manage authentication, logging, or validation.

```cs
// Inline middleware
app.Get("/secure",
    (req, res) => Console.WriteLine("Auth check..."),
    async (req, res) => await res.Text("Secure data")
);

// Router-level middleware
var secureRouter = new Router();
secureRouter.Use((req, res) => {
    if (!req.Headers.ContainsKey("Authorization"))
        res.Status(Status.Unauthorized);
});
```

## 📦 Request & Response

### Request

* req.Params: Route parameters (e.g., :id).
* req.QueryParams: URL query strings.
* await req.Body<T>(): Deserializes JSON body to a specific type.

### Response
* res.Status(int): Sets the HTTP status code.
* res.Json(object): Sends a JSON response.
* res.Text(string): Sends a plain text response.
* res.Html(string): Sends an HTML response.