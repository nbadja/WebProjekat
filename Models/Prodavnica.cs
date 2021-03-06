using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;


namespace Models
{
    [Table("Prodavnica")]
    public class Prodavnica
    {
        [Key]
        [DataType("integer")]
        public int ID {get;set;}

        [Required]
        [MaxLength(50)]
        public string Naziv { get; set; }

        [Required]
        public Drzava Drzava {get;set;}

        [Required]
        public Grad Grad {get;set;}

        [Required]
        public string Adresa {get;set;}

        [JsonIgnore]
        [ForeignKey("ProdavnicaID")]
        public List<Storage> Storages {get;set;}
    }
}