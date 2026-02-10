using Flasharp;

var app = new App();
const int PORT = 3000;

var users = new List<User>();

app.Use((req, res) =>
{
    Console.WriteLine(req.Method + " " + req.Path);
});

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
    var user = new User(users.Count+1, userBody!.username);
    users.Add(user);

    return await res.Status(201).Json(new {message = "User created succesfully!", user});
});

app.Get("/users/:id", async (req, res) =>
{
    var userId = int.Parse(req.Params["id"]);
    User? user = null;
    foreach (var u in users)
    {
        if(u.id == userId)
        {
            user = u;
        }
    }
    return user != null ? await res.Json(user!) : await res.Status(404).Json(new { message = $"User {userId} not found!"});
});

await app.Listen(PORT, () =>
{
    Console.WriteLine($"Server running on port {PORT}");
});

record User(int id, string username);
record CreateUserDTO(string username);