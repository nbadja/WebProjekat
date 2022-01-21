using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;


namespace Models
{
    [Table("Predmet")]
    public class Predmet
    {
        [Key]
        [DataType("integer")]
        public int ID {get;set;}

        [Required]
        [MaxLength(50)]
        public string Naziv { get; set; }

        [Required]
        [MaxLength(50)]
        public string BarCode { get; set; }

        [Required]
        [DataType("integer")]
        public int Cena {get;set;}

        [JsonIgnore]
        [ForeignKey("PredmetID")]
        public List<Storage> Storages {get;set;}
    }
}