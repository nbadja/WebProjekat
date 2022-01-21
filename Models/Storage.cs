using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;


namespace Models
{
    [Table("Storage")]
    public class Storage
    {
        [Key]
        [DataType("integer")]
        public int ID {get;set;}

        [Required]
        public Prodavnica Prodavnica {get;set;}

        [Required]
        public Predmet Predmet {get;set;}
    }
}