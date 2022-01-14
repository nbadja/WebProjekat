using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeamChat.Models
{
    [Table("Rooms")]
    public class Room
    {
        [Key]
        public int Room_ID { get; set; }

        [Required]
        [MinLength(6)]
        [MaxLength(50)]
        public string Password {get;set;}
        
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Range(1, 20)]
        public int maxConnections { get; set; }
           
        public int currentConnections { get; set; }
                
    }
}