using Healthcare.Api.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Healthcare.Api.Repository.EntitiesConfiguration
{
    public class HemogramaEntityConfiguration : IEntityTypeConfiguration<Hemograma>
    {
        public void Configure(EntityTypeBuilder<Hemograma> builder)
        {
            builder.ToTable("Hemograma").HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Metodo).HasColumnName("Metodo");
            builder.Property(x => x.GlobulosRojos).HasColumnName("GlobulosRojos");
            builder.Property(x => x.GlobulosBlancos).HasColumnName("GlobulosBlancos");
            builder.Property(x => x.Hemoglobina).HasColumnName("Hemoglobina");
            builder.Property(x => x.Hematocrito).HasColumnName("Hematocrito");
            builder.Property(x => x.VCM).HasColumnName("VCM");
            builder.Property(x => x.HCM).HasColumnName("HCM");
            builder.Property(x => x.CHCM).HasColumnName("CHCM");
            builder.Property(x => x.NeutrofilosCayados).HasColumnName("NeutrofilosCayados");
            builder.Property(x => x.NeutrofilosSegmentados).HasColumnName("NeutrofilosSegmentados");
            builder.Property(x => x.Eosinofilos).HasColumnName("Eosinofilos");
            builder.Property(x => x.Basofilos).HasColumnName("Basofilos");
            builder.Property(x => x.Linfocitos).HasColumnName("Linfocitos");
            builder.Property(x => x.Monocitos).HasColumnName("Monocitos");
        }
    }
}
