using SQLite4Unity3d;

namespace packt.FoodyGO.Database
{
    public class Player
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int Level { get; set; }
        public int Experience { get; set; }
    }
}