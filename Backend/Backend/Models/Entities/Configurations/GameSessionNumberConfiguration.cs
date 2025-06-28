namespace Backend.Models.Entities.Configurations
{
    public class GameSessionNumberConfiguration : IEntityTypeConfiguration<GameSessionNumber>
    {
        public void Configure(EntityTypeBuilder<GameSessionNumber> builder)
        {
            builder.Property(x => x.Value)
                .IsRequired();
        }
    }
}
