# Argo ⚡

Argo is a minimalist web framework for C#, inspired by Express.js.

It’s designed for **small projects, learning, and rapid prototyping**, offering a simple and expressive API without the overhead of ASP.NET.


## Minimal Example
```cs
using Argo;

var app = new App();
const int PORT = 3000;

app.Get("/", async (req, res) =>
{
    return await res.Text("Hello, World!");
});

await app.Listen(PORT, () =>
{
    Console.WriteLine($"Server running on port {PORT}");
});
```

## Features

- 🚀 Express-like API (`Get`, `Post`, etc.)
- 🧩 Minimal setup — no boilerplate
- 📦 Built-in JSON and text responses
- 🛣️ Route params (`/users/:id`)
- 📄 Automatic request body parsing
- 🎯 Ideal for learning and prototypes

## API Overview

```cs
// Creates a new Argo application
var app = new App();

// Starts the server on the given port
await app.Listen(3000);

// Routes
// Available methods: Get, Post, Put, Patch, Delete
app.Get("/hello", async (req, res) =>
{
    return await res.Text("Hello!");
});

// Params
// Access route parameters (e.g. /users/:id)
req.Params["id"];

// Body
// Parses the request body into the given type
var body = await req.Body<MyDTO>();

// Response
// Status can be chained with any response
res.Text("Hello");
res.Json(object);
res.Status(201);

// Middleware
// All requests will call this method
app.Use((req, res) =>
{
    Console.WriteLine($"{req.Method} {req.Path}");
});
```

## Author's Note

This is a personal hobby project to learn more about web frameworks.
I created it because I wanted a simpler alternative to ASP.NET for small projects and prototypes — although ASP.NET is a great framework.

## License

This project is licensed under the MIT License. See [LICENSE](LICENSE.txt) for details.
