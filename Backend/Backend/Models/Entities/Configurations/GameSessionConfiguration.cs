namespace Backend.Models.Entities.Configurations
{
    public class GameSessionConfiguration : IEntityTypeConfiguration<GameSession>
    {
        public void Configure(EntityTypeBuilder<GameSession> builder)
        {
            builder.HasOne(s => s.Game)
                .WithMany(g => g.GameSessions)
                .HasForeignKey(s => s.GameId)
                .OnDelete(DeleteBehavior.Cascade); // Optional: cascade delete if Game is deleted

            builder.HasMany(g => g.PlayNumbers)
               .WithOne(n => n.GameSession)
               .HasForeignKey(n => n.GameSessionId);

            builder.Property(gp => gp.StartTime)
                .IsRequired();

            builder.Property(gp => gp.RemainingSeconds)
               .IsRequired();

            builder.Property(gp => gp.TotalCorrect)
                .IsRequired()
                .HasDefaultValue(0);

            builder.Property(gp => gp.TotalIncorrect)
                .IsRequired()
                .HasDefaultValue(0);

            builder.Property(gp => gp.IsExpired)
                .IsRequired()
                .HasDefaultValue(false);
        }
    }
}