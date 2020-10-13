using Messaging.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Messaging.Data
{
    public class MessagingDbContext : DbContext
    {
        public MessagingDbContext(DbContextOptions<MessagingDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Block> Blocks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Block>()
                .HasOne(b => b.BlockedUser)
                .WithMany(u => u.BlockerUserList)
                .HasForeignKey(b => b.FKBlockedUserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<Block>()
                .HasOne(b => b.BlockerUser)
                .WithMany(u => u.BlockedUserList)
                .HasForeignKey(b => b.FKBlockerUserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<Message>()
                .HasOne(m => m.Receiver)
                .WithMany(u => u.ReceivedMessages)
                .HasForeignKey(m => m.FKReceiverId)
                .IsRequired()
                .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<Message>()
                .HasOne(m => m.Sender)
                .WithMany(u => u.SentMessages)
                .HasForeignKey(m => m.FKSenderId)
                .IsRequired()
                .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<User>()
                .HasMany(u => u.BlockedUserList)
                .WithOne(b => b.BlockerUser)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .HasMany(u => u.BlockerUserList)
                .WithOne(b => b.BlockedUser)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .HasMany(u => u.SentMessages)
                .WithOne(m => m.Sender)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .HasMany(u => u.ReceivedMessages)
                .WithOne(m => m.Receiver)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
