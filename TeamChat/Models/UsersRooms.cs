using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeamChat.Models
{
    [Table("UserRooms")]
    public class UsersRooms
    {
        [Key]
        public int Spoj_ID { get; set; }
        
        [Required]
        public User User {get;set;}

        [Required]
        public Room Room {get;set;}
    
    }
}