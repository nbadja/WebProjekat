using Microsoft.EntityFrameworkCore;

namespace TeamChat.Models
{
    public class ChatContext : DbContext
    {
        public DbSet<User> Users {get;set;}

        public DbSet<Room> Rooms {get;set;}

        public DbSet<Message> Messages {get;set;}

        public DbSet<UsersRooms> UsersRooms {get;set;}

        public ChatContext(DbContextOptions options) : base(options)
        {

        }
    }
}