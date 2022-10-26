using Model;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class TrådContext : DbContext
    {
        //Pas... Felter måske?? De ændre i hvert fald tabelnavnene i databasen
        public DbSet<Tråd> Tråde => Set<Tråd>();
        public DbSet<Kommentar> Kommentarer => Set<Kommentar>();
        public DbSet<Bruger> Brugerer => Set<Bruger>();

        public TrådContext(DbContextOptions<TrådContext> options)
           : base(options)
        {
            // Den her er tom. Men ": base(options)" sikre at constructor
            // på DbContext super-klassen bliver kaldt.
        }
    }
}
