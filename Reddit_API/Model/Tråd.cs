namespace Model
{
    public class Tråd
    {
        //Konstrukterer 
        public Tråd (int trådID, Bruger bruger, string overskrift, int upvotes, int downvotes, DateTime dato, string indhold, List<Kommentar> kommentarListe)
        {
            this.TrådID = trådID;
            this.Bruger = bruger;
            this.Overskrift = overskrift;
            this.UpVotes = upvotes;
            this.DownVotes = downvotes;
            this.Dato = dato;
            this.Indhold = indhold;
            this.KommentarListe = kommentarListe;
        }
        public Tråd(int trådID, Bruger bruger, string overskrift, int upvotes, int downvotes, DateTime dato, string indhold)
        {
            this.TrådID = trådID;
            this.Bruger = bruger;
            this.Overskrift = overskrift;
            this.UpVotes = upvotes;
            this.DownVotes = downvotes;
            this.Dato = dato;
            this.Indhold = indhold;
        }

        //Felter
        public int TrådID { get; set; }
        public Bruger Bruger { get; set; }
        public string Overskrift { get; set; }
        public int UpVotes { get; set; }
        public int DownVotes { get; set; }
        public DateTime Dato { get; set; } = DateTime.Now;
        public string Indhold { get; set; }
        public List<Kommentar> KommentarListe { get; set; } = new List<Kommentar>();
        
        //Tom kontruktor
        public Tråd()
        {

        }
      
    }

}
