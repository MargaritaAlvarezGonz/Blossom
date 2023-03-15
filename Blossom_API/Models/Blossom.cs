using System.Data;

namespace Blossom_API.Models
{
    //Aquí pongo los campos que vamos a llenar
    public class Blossom
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }

    }
}
