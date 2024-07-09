using System.ComponentModel.DataAnnotations;

namespace PlayersAPI.Models
{
    public class Player
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string BirthPlace { get; set; }
    }
}
