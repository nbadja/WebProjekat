using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeamChat.Models
{
    [Table("Users")]
    public class User
    {
        [Key]
        public int ID {get;set;}

        [RegularExpression("a-zA-Z")]
        [Required]
        [MaxLength(50)]
        public string Name {get;set;}
        
        [RegularExpression("a-zA-Z")]
        [Required]
        [MaxLength(50)]
        public string Lastname {get;set;}
       
        [RegularExpression("^([a-zA-Z0-9_\\-\\.]+)@([a-zA-Z0-9_\\-\\.]+)\\.([a-zA-Z]{2,5})$")]
        [Required]
        [MaxLength(255)]
        public string Email {get;set;}
        
        [Required]
        [MinLength(6)]
        public string Password {get;set;}

        public bool Online {get;set;}
    }
}