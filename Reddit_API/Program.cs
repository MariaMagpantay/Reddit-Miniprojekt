using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http.Json;

using Data;
using Model;
using Service;

var builder = WebApplication.CreateBuilder(args);

// S�tter CORS s� API'en kan bruges fra andre dom�ner
// Vi bruger denne s� vi kan bruge API'en i vores blazor WebApp
var AllowSomeStuff = "_AllowSomeStuff";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: AllowSomeStuff, builder => {
        builder.AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

// Tilf�j DbContext factory som service.
builder.Services.AddDbContext<Tr�dContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("ContextSQLite")));

// Tilf�j DataService s� den kan bruges i endpoints
builder.Services.AddScoped<DataService>();

// Dette kode kan bruges til at fjerne "cykler" i JSON objekterne.
/*
builder.Services.Configure<JsonOptions>(options =>
{
    // Her kan man fjerne fejl der opst�r, n�r man returnerer JSON med objekter,
    // der refererer til hinanden i en cykel.
    // (alts� dobbelrettede associeringer)
    options.SerializerOptions.ReferenceHandler = 
        System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});
*/

var app = builder.Build();


// Seed data hvis n�dvendigt.
using (var scope = app.Services.CreateScope())
{
    var dataService = scope.ServiceProvider.GetRequiredService<DataService>();
    dataService.SeedData(); // Fylder data p�, hvis databasen er tom. Ellers ikke.
}

app.UseHttpsRedirection();

//Tillader CROS
app.UseCors(AllowSomeStuff);

// Middlware der k�rer f�r hver request. S�tter ContentType for alle responses til "JSON".
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
app.MapGet("/api/tr�de", (DataService service) => //Henter alle tr�de, uden kommentarer
{
    return service.GetAlleTr�de().Select(t => new {
        tr�dId = t.Tr�dID,
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


app.MapGet("/api/tr�d/{id}", (DataService service, int id) => //Henter en tr�d p� et bestemt id
{
    return service.GetTr�d(id);
});


///////////Er dennne n�dvendig?
app.MapGet("/api/kommentarer", (DataService service) => //Henter alle kommentarer fra alle tr�de
{
    return service.GetAlleKommentarer().Select(t => new {
        tr�dId = t.Tr�dID,
        kommentarListe = t.KommentarListe
    });
});


//POST
app.MapPost("/api/tr�de", (DataService service, NewTr�dData data) => //Laver en ny tr�d
{
    string result = service.CreateTr�d(data.BrugerID, data.Overskrift, data.Indhold);
    return new { message = result };
});


app.MapPost("/api/tr�d/{id}", (DataService service, NewKommentarData data, int id) => //Laver en ny kommentart p� en bestemt tr�d (specificeret p� Tr�dID)
{
    string result = service.CreateKommentar(id, data.BrugerID, data.Tekst);
    return new { message = result };
});


//PUT
app.MapPut("/api/tr�d/{id}/upvote", (DataService service, int id) => //Opdaterer antal upvotes p� en bestemt tr�d (specificeret p� Tr�dID)
{
    Console.WriteLine("API ramt");
    string result = service.UpVotesTr�d(id);
    Console.WriteLine(result); 
    return new { message = result };
});

app.MapPut("/api/tr�d/{id}/downvote", (DataService service, int id) => //Opdaterer antal downvotes p� en bestemt tr�d (specificeret p� Tr�dID)
{
    string result = service.DownVotesTr�d(id);
    return new { message = result };
});

app.MapPut("/api/kommentar/{id}/upvote", (DataService service, int id) => //Opdaterer antal upvotes p� en bestemt kommentar (specificeret p� KommentarID)
{
    string result = service.UpVotesKommentar(id);
    return new { message = result };
});

app.MapPut("/api/kommentar/{id}/downvote", (DataService service, int id) => //Opdaterer antal downvotes p� en bestemt kommentar (specificeret p� KommentarID)
{
    string result = service.DownVotesKommentar(id);
    return new { message = result };
});




app.Run();

record NewTr�dData(int BrugerID, string Overskrift, string Indhold);
record NewKommentarData(int BrugerID, string Tekst);
//record UpdateUpVotes(int UpVotes);
//record UpdateDownVotes(int DownVotes);