# Flasharp ⚡

A minimalist web framework for C# designed for small projects and prototypes.

```cs
using Flasharp;

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

## About

Flasharp is inspired by Express.js and created as a lightweight alternative to ASP.NET.
It’s meant for hobby projects, learning, and rapid prototyping. For larger applications, ASP.NET remains the recommended choice.

## CRUD Example

```cs
using Flasharp;

var app = new App();
const int PORT = 3000;

var users = new List<User>();

app.Get("/", async (req, res) =>
{
    return await res.Text("Hello, World!");
});

app.Get("/users", async (req, res) =>
{
    return await res.Json(users);
});

app.Post("/users", async (req, res) =>
{
    var userBody = await req.Body<CreateUserDTO>();
    var user = new User(users.Count + 1, userBody!.username);
    users.Add(user);

    return await res.Status(201).Json(new { message = "User created successfully!", user });
});

app.Get("/users/:id", async (req, res) =>
{
    var userId = int.Parse(req.Params["id"]);
    User? user = users.FirstOrDefault(u => u.id == userId);

    return user != null 
        ? await res.Json(user) 
        : await res.Status(404).Json(new { message = $"User {userId} not found!" });
});

await app.Listen(PORT, () =>
{
    Console.WriteLine($"Server running on port {PORT}");
});

record User(int id, string username);
record CreateUserDTO(string username);
```

## Author's Note

This is a personal hobby project to learn more about web frameworks.
I created it because I wanted a simpler alternative to ASP.NET for small projects and prototypes — although ASP.NET is a great framework.

## License

This project is licensed under the MIT License. See [LICENSE](LICENSE.txt) for details.