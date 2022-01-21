using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;


namespace Models
{
    [Table("Lokacija")]
    public class Lokacija
    {
        [Key]
        [Column("ID")]
        [DataType("integer")]
        public int ID {get;set;}

        [Required]
        [MaxLength(100)]
        [Column("Mesto")]
        public string Naziv { get; set; }

        [Required]
        [MaxLength(100)]
        [Column("Ulica")]
        public string Ulica { get; set; }
    }
}