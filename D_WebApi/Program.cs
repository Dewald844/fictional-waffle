var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.UseHttpsRedirection();

app.MapPost("/login", async (LoginRequest loginRequest) =>
    {
        var user = await new Controller.UserService(
            new Database.UserRepository("Data Source=users.db")).AuthenticateUserAsync(
            loginRequest.UserName, loginRequest.Password);

        if (user == null)
        {
            return Results.Unauthorized();
        }
        return Results.Ok(new
        {
            Message = "Login successful",
            UserName = user.Username
        });
    })
    .WithName("Login")
    .WithOpenApi();

app.Run();

record LoginRequest(string UserName, string Password);
