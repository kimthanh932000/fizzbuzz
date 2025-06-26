namespace Backend.Models.Entities.Configurations
{
    public class ScoreConfiguration : IEntityTypeConfiguration<Score>
    {
        public void Configure(EntityTypeBuilder<Score> builder)
        {
            builder.HasOne(s => s.Game)
                .WithOne() 
                .HasForeignKey<Score>(s => s.GameId)
                .OnDelete(DeleteBehavior.Cascade); // Optional: cascade delete if Game is deleted

            builder.Property(gp => gp.TotalCorrect)
                .IsRequired();

            builder.Property(gp => gp.TotalIncorrect)
                .IsRequired();
        }
    }
}