namespace Backend.Models.Entities.Configurations
{
    public class GamePlayNumberConfiguration : IEntityTypeConfiguration<GamePlayNumber>
    {
        public void Configure(EntityTypeBuilder<GamePlayNumber> builder)
        {
            builder.Property(x => x.Value)
                .IsRequired();
        }
    }
}
