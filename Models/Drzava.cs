using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;


namespace Models
{
    [Table("Drzava")]
    public class Drzava
    {
        [Key]
        [Column("ID")]
        [DataType("integer")]
        public int ID {get;set;}

        [Required]
        [MaxLength(100)]
        public string Naziv { get; set; }
    }
}