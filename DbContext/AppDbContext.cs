using ChatApp.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Context
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Conversation> Conversations { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Conversation>()
                 .HasOne<User>(u => u.Sender)
                 .WithMany()
                 .HasForeignKey(c => c.SenderId)
                 .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Conversation>()
                .HasOne<User>(u => u.Reciever)
                .WithMany()
                .HasForeignKey(c => c.RecieverId)
                 .OnDelete(DeleteBehavior.NoAction);

        }
    }
}

