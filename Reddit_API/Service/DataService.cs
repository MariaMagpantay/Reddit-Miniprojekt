using Data;
using Microsoft.EntityFrameworkCore;
using Model;
using System.Linq;

namespace Service
{
    public class DataService
    {
        private TrådContext db { get; }

        public DataService(TrådContext db)
        {
            this.db = db;
        }

        //Test data
        public void SeedData()
        {
            Tråd tråd = db.Tråde.FirstOrDefault()!;
            Bruger brugersalsa = new Bruger(1, "SalsaMan");
            Bruger bror = new Bruger(2, "Brother");
            Bruger suppemand = new Bruger(3, "HSM Fan no.1");
            Kommentar testKommentar = new Kommentar(1, "test", brugersalsa, 0, 0, new DateTime(2022, 10, 12));
            Kommentar testKommentar2 = new Kommentar(2, "JEG ELSKER SUPPE OG SHARPAY", suppemand, 2, 1, new DateTime(2022, 10, 12));
            Kommentar testKommentar3 = new Kommentar(3, "Jeg tester lige kommentaren", bror, 2, 5, new DateTime(2022, 10, 21));
            Tråd testTråd = new Tråd(1, bror, "test overskrift", 0, 0, new DateTime(2022, 10, 12), "test indhold");
            Tråd testTråd1 = new Tråd(2, brugersalsa, "test test", 0, 2, new DateTime(2022, 10, 21), "test indhold til tråd 2");
            testTråd.KommentarListe.Add(testKommentar);
            testTråd.KommentarListe.Add(testKommentar2);
            testTråd1.KommentarListe.Add(testKommentar3);
            if (tråd == null) //Hvis der ingen tråde er i databasen, indsættes testTråd og testTråd1
            {
                db.Tråde.Add(testTråd);
                db.Tråde.Add(testTråd1);
            }

            db.SaveChanges();
        }


        //GET metoder
        public List<Tråd> GetAlleTråde() //Henter alle tråde, uden kommentarer og includerer brugeren=forfatteren af tråden
        {
            return db.Tråde.Include(t => t.Bruger).OrderByDescending(w => w.Dato).Take(50).ToList();
        }

        public Tråd GetTråd(int id) //Henter en tråd på et specifikt id, med tilhørende kmmentarer. Include Bruger = forfatteren af tråden. ThenInclude Bruger = forfatter af kommentaren
        {
            return db.Tråde.Include(t => t.Bruger).Include(t => t.KommentarListe).ThenInclude(t => t.Bruger).FirstOrDefault(t => t.TrådID == id);
            //var result = await httpClient.GetFromJsonAsync<Tråd>("api/tråd/" + id);
            //return result;
        }

        //////////Er nedenstående metode nødvendig?
        public List<Tråd> GetAlleKommentarer() //Henter alle kommentarer, og hvilken TrådID de tilhører
        {
            return db.Tråde.Include(t => t.KommentarListe).ThenInclude(t => t.Bruger).ToList();
        }



        //POST metoder
        public string CreateTråd(int brugerID, string overskrift, string indhold) //Laver en ny tråd
        {
            Bruger bruger = db.Brugerer.FirstOrDefault(b => b.BrugerID == brugerID);
            db.Tråde.Add(new Tråd { Bruger = bruger, Overskrift = overskrift, Indhold = indhold });
            db.SaveChanges();
            return "Tråd created";
        }

        public string CreateKommentar(int trådID, int brugerID, string tekst) //Laver en ny kommentar på et bestemt TrådID
        {
            Tråd tråd = db.Tråde.FirstOrDefault(t => t.TrådID == trådID); 
            Bruger bruger = db.Brugerer.FirstOrDefault(b => b.BrugerID == brugerID);
            tråd.KommentarListe.Add(new Kommentar { Bruger = bruger, Tekst = tekst });
            db.SaveChanges();
            return "Kommentar created";
        }


        //PUT metoder
        public string UpVotesTråd(int trådID) //Opdaterer antal upvotes på et bestemt TrådID
        {
            Tråd tråd = db.Tråde.Where(t => t.TrådID == trådID).FirstOrDefault<Tråd>();
            if (tråd != null)
            {
                tråd.UpVotes = tråd.UpVotes + 1;
               
                db.SaveChanges();
            }
            return "Tråd stemmer updated";
        }
        public string DownVotesTråd(int trådID) //Opdaterer antal downvotes på et bestemt TrådID
        {
            Tråd tråd = db.Tråde.Where(t => t.TrådID == trådID).FirstOrDefault<Tråd>();
            if (tråd != null)
            {
                tråd.DownVotes ++;

                db.SaveChanges();
            }
            return "Tråd stemmer updated";
        }

        public string UpVotesKommentar(int kommentarID) //Opdaterer antal upvotes på et bestemt KommentarID
        {
            Kommentar kommentar = db.Kommentarer.Where(k => k.KommentarID == kommentarID).FirstOrDefault<Kommentar>();
            if (kommentar != null)
            {
                kommentar.UpVotes ++;
               
                db.SaveChanges();
            }
            return "Kommentar stemmer updated";
        }

        public string DownVotesKommentar(int kommentarID) //Opdaterer antal downvotes på et bestemt KommentarID
        {
            Kommentar kommentar = db.Kommentarer.Where(k => k.KommentarID == kommentarID).FirstOrDefault<Kommentar>();
            if (kommentar != null)
            {
                kommentar.DownVotes ++;

                db.SaveChanges();
            }
            return "Kommentar stemmer updated";
        }
    }
}
