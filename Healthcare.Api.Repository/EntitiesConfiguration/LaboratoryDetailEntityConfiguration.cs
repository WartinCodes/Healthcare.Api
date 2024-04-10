using Healthcare.Api.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Healthcare.Api.Repository.EntitiesConfiguration
{
    public class LaboratoryDetailEntityConfiguration : IEntityTypeConfiguration<LaboratoryDetail>
    {
        public void Configure(EntityTypeBuilder<LaboratoryDetail> builder)
        {
            builder.ToTable("LaboratoryDetail").HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.HasOne(x => x.Study)
               .WithMany()
               .HasForeignKey(x => x.IdStudy)
               .IsRequired(false);

            builder.Property(x => x.GlobulosRojos);
            builder.Property(x => x.GlobulosBlancos);
            builder.Property(x => x.Hemoglobina);
            builder.Property(x => x.Hematocrito);
            builder.Property(x => x.VCM);
            builder.Property(x => x.HCM);
            builder.Property(x => x.CHCM);
            builder.Property(x => x.NeutrofilosCayados);
            builder.Property(x => x.NeutrofilosSegmentados);
            builder.Property(x => x.Eosinofilos);
            builder.Property(x => x.Basofilos);
            builder.Property(x => x.Linfocitos);
            builder.Property(x => x.Monocitos);
            builder.Property(x => x.Eritrosedimentacion1);
            builder.Property(x => x.Eritrosedimentacion2);
            builder.Property(x => x.Plaquetas);
            builder.Property(x => x.Glucemia);
            builder.Property(x => x.Uremia);
            builder.Property(x => x.Creatininemia);
            builder.Property(x => x.ColesterolTotal);
            builder.Property(x => x.ColesterolHdl);
            builder.Property(x => x.Trigliceridos);
            builder.Property(x => x.Uricemia);
            builder.Property(x => x.BilirrubinaDirecta);
            builder.Property(x => x.BilirrubinaIndirecta);
            builder.Property(x => x.BilirrubinaTotal);
            builder.Property(x => x.TransaminasaGlutamicoOxalac);
            builder.Property(x => x.TransaminasaGlutamicoPiruvic);
            builder.Property(x => x.FosfatasaAlcalina);
            builder.Property(x => x.TirotrofinaPlamatica);
        }
    }
}
