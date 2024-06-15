using SQLite;

namespace Imobiliare
{
    [Table("Houses")]
    public class House 
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
    }
}
