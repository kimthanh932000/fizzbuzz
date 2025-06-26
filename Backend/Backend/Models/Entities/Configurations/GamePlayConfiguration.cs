namespace Backend.Models.Entities.Configurations
{
    public class GamePlayConfiguration : IEntityTypeConfiguration<GamePlay>
    {
        public void Configure(EntityTypeBuilder<GamePlay> builder)
        {
            builder.HasOne(s => s.Game)
                .WithOne() 
                .HasForeignKey<GamePlay>(s => s.GameId)
                .OnDelete(DeleteBehavior.Cascade); // Optional: cascade delete if Game is deleted

            builder.Property(gp => gp.RemainingSeconds)
               .IsRequired();

            builder.Property(gp => gp.TotalCorrect)
                .IsRequired();

            builder.Property(gp => gp.TotalIncorrect)
                .IsRequired();
        }
    }
}