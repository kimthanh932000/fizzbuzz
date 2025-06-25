namespace Backend.Models.Entities.Configurations
{
    public class GameConfiguration : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(256);

            builder.Property(e => e.AuthorName)
                .IsRequired()
                .HasMaxLength(256);

            builder.Property(e => e.Range)
                .IsRequired()
                .HasDefaultValue(100);

            builder.Property(e => e.DurationInSeconds)
                .IsRequired();

            builder
                .HasMany(g => g.Rules)
                .WithOne(r => r.Game)
                .HasForeignKey(r => r.GameId);
        }
    }
}
