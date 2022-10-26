using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.Json;

using Data;
using Model;
using Service;

var builder = WebApplication.CreateBuilder(args);

// Sætter CORS så API'en kan bruges fra andre domæner
// Vi bruger denne så vi kan bruge API'en i vores blazor WebApp
var AllowSomeStuff = "_AllowSomeStuff";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: AllowSomeStuff, builder => {
        builder.AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

// Tilføj DbContext factory som service.
builder.Services.AddDbContext<TrådContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("ContextSQLite")));

// Tilføj DataService så den kan bruges i endpoints
builder.Services.AddScoped<DataService>();

// Dette kode kan bruges til at fjerne "cykler" i JSON objekterne.
/*
builder.Services.Configure<JsonOptions>(options =>
{
    // Her kan man fjerne fejl der opstår, når man returnerer JSON med objekter,
    // der refererer til hinanden i en cykel.
    // (altså dobbelrettede associeringer)
    options.SerializerOptions.ReferenceHandler = 
        System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});
*/

var app = builder.Build();


// Seed data hvis nødvendigt.
using (var scope = app.Services.CreateScope())
{
    var dataService = scope.ServiceProvider.GetRequiredService<DataService>();
    dataService.SeedData(); // Fylder data på, hvis databasen er tom. Ellers ikke.
}

app.UseHttpsRedirection();

//Tillader CROS
app.UseCors(AllowSomeStuff);

// Middlware der kører før hver request. Sætter ContentType for alle responses til "JSON".
app.Use(async (context, next) =>
{
    context.Response.ContentType = "application/json; charset=utf-8";
    await next(context);
});


//MapGet test
app.MapGet("/", (DataService service) =>
{
    return new { message = "Hello World!" };
});


//GET 
app.MapGet("/api/tråde", (DataService service) => //Henter alle tråde, uden kommentarer
{
    return service.GetAlleTråde().Select(t => new {
        trådId = t.TrådID,
        dato = t.Dato,
        overskrift = t.Overskrift,
        indhold = t.Indhold,
        upvotes = t.UpVotes,
        downvotes = t.DownVotes,
        bruger = new
        {
            t.Bruger.BrugerID,
            t.Bruger.BrugerNavn
        }

    });
});


app.MapGet("/api/tråd/{id}", (DataService service, int id) => //Henter en tråd på et bestemt id
{
    return service.GetTråd(id);
});


///////////Er dennne nødvendig?
app.MapGet("/api/kommentarer", (DataService service) => //Henter alle kommentarer fra alle tråde
{
    return service.GetAlleKommentarer().Select(t => new {
        trådId = t.TrådID,
        kommentarListe = t.KommentarListe
    });
});


//POST
app.MapPost("/api/tråde", (DataService service, NewTrådData data) => //Laver en ny tråd
{
    string result = service.CreateTråd(data.BrugerID, data.Overskrift, data.Indhold);
    return new { message = result };
});


app.MapPost("/api/tråd/{id}", (DataService service, NewKommentarData data, int id) => //Laver en ny kommentart på en bestemt tråd (specificeret på TrådID)
{
    string result = service.CreateKommentar(id, data.BrugerID, data.Tekst);
    return new { message = result };
});


//PUT
app.MapPut("/api/tråd/{id}/upvote", (DataService service, int id) => //Opdaterer antal upvotes på en bestemt tråd (specificeret på TrådID)
{
    Console.WriteLine("API ramt");
    string result = service.UpVotesTråd(id);
    Console.WriteLine(result); 
    return new { message = result };
});

app.MapPut("/api/tråd/{id}/downvote", (DataService service, int id) => //Opdaterer antal downvotes på en bestemt tråd (specificeret på TrådID)
{
    string result = service.DownVotesTråd(id);
    return new { message = result };
});

app.MapPut("/api/kommentar/{id}/upvote", (DataService service, int id) => //Opdaterer antal upvotes på en bestemt kommentar (specificeret på KommentarID)
{
    string result = service.UpVotesKommentar(id);
    return new { message = result };
});

app.MapPut("/api/kommentar/{id}/downvote", (DataService service, int id) => //Opdaterer antal downvotes på en bestemt kommentar (specificeret på KommentarID)
{
    string result = service.DownVotesKommentar(id);
    return new { message = result };
});




app.Run();

record NewTrådData(int BrugerID, string Overskrift, string Indhold);
record NewKommentarData(int BrugerID, string Tekst);
//record UpdateUpVotes(int UpVotes);
//record UpdateDownVotes(int DownVotes);