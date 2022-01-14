using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeamChat.Models
{
    [Table("Messages")]
    public class Message
    {
        [Key]
        public int Message_ID {get;set;}

        [Required]
        public string Text {get;set;} 

        [Required]
        public User Sender {get;set;}
             
        [Required]
        public DateTime Time {get;set;}     

        public Room Reciver {get;set;}
    }
}