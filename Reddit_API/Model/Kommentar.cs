namespace Model
{
    public class Kommentar
    {
        //Konstrukterer
        public Kommentar(int kommentarID, string tekst, Bruger bruger, int upvotes, int downvotes, DateTime dato = new DateTime())
        {
            this.KommentarID = kommentarID;
            this.Tekst = tekst;
            this.Bruger = bruger;
            this.UpVotes = upvotes;
            this.DownVotes = downvotes;
            this.Dato = dato;
        }
        public Kommentar(int kommentarID , string tekst, Bruger bruger, int upvotes, int downvotes)
        {
            this.KommentarID = kommentarID;
            this.Tekst = tekst;
            this.Bruger = bruger;
            this.UpVotes = upvotes;
            this.DownVotes = downvotes;
        }

        //Felter
        public int KommentarID { get; set; }   
        public string Tekst { get; set; }
        public Bruger Bruger { get; set; }
        public int UpVotes { get; set; }
        public int DownVotes { get; set; }
        public DateTime Dato { get; set; } = DateTime.Now; 
       

        //Tom konstrukter
        public Kommentar ()
        {

        }

    }
}
