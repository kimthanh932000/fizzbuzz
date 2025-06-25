
namespace Backend.Models.Entities.Configurations
{
    public class RuleConfiguration : IEntityTypeConfiguration<Rule>
    {
        public void Configure(EntityTypeBuilder<Rule> builder)
        {
            builder.Property(e => e.DivisibleBy)
                .IsRequired();

            builder.Property(e => e.Word)
                .IsRequired()
                .HasMaxLength(10);
        }
    }
}
