using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;


namespace Models
{
    [Table("Grad")]
    public class Grad
    {
        [Key]
        [Column("ID")]
        [DataType("integer")]
        public int ID {get;set;}

        [Required]
        [MaxLength(100)]
        public string Naziv { get; set; }

        [Required]
        public Drzava Drzava {get;set;}
    }
}