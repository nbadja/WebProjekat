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
        public Lokacija Mesto {get;set;}


        [JsonIgnore]
        [ForeignKey("ProdavnicaID")]
        public List<Storage> Storages {get;set;}
    }
}