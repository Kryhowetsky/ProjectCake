using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ProjectCake.Data
{
    public class FileMap
    {
        public FileMap(EntityTypeBuilder<File> entityBuilder)
        {
            entityBuilder.HasKey(t => t.Id);
            entityBuilder.Property(t => t.Name);
            entityBuilder.Property(t => t.Path);
            entityBuilder.Property(t => t.ParentId);
            entityBuilder.Property(t => t.Description);
            entityBuilder.Property(t => t.AddedDate);
        }
    }
}