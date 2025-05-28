var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularDevClient", policy =>
    {
        policy.WithOrigins("http://localhost:4200")  // Angular app URL
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

app.MapPost("/login", async (LoginRequest loginRequest) =>
    {
        var user = await new
                Controller.UserService(
                    new Database.UserRepository("Data Source=../B_Database/paltrack.db"))
                        .AuthenticateUserAsync(loginRequest.UserName, loginRequest.Password);

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

app.MapGet("/olympic-winners", async () =>
    {
        var winners = await new
                Controller.OlympicWinnerService(
                    new Database.OlympicWinnerRepository("Data Source=../B_Database/paltrack.db"))
                        .GetOlympicWinnersAsync();

        return Results.Ok(winners);
    })
    .WithName("GetOlympicWinners")
    .WithOpenApi();

app.UseCors("AllowAngularDevClient");

app.Run();

record LoginRequest(string UserName, string Password);
