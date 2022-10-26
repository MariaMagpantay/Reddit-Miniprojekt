namespace Model
{
    public class Bruger
    {
        //Konstrukter
        public Bruger (int brugerID, string brugernavn)
        {
            this.BrugerID = brugerID;
            this.BrugerNavn = brugernavn;
        }

        //Felter
        public int BrugerID { get; set; }
        public string BrugerNavn{ get; set; }
    
       //Tom konstrukter
       public Bruger()
        {

        }
      
    }

}
