using SQLite4Unity3d;

namespace packt.FoodyGO.Database
{
    public class InventoryItem
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string ItemType { get; set; }
        public string Name { get; set; }
    }
}