namespace Core2WebAPI

{

    public class Team

    {

        public int TeamID { get; set; }

        public String Name { get; set; }

        public String University { get; set; }

        public String Mascot { get; set; }



        public Team()

        {



        }



        public Team(int id, String name, String university)

        {

            this.TeamID = id;

            this.Name = name;

            this.University = university;

        }

        public Team(int id, String name, String university, String mascot)

        {

            this.TeamID = id;

            this.Name = name;

            this.University = university;

            this.Mascot = mascot;

        }



    }

}
