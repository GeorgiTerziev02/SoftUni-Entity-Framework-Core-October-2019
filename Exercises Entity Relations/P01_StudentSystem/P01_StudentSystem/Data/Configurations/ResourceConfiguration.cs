﻿namespace P01_StudentSystem.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using P01_StudentSystem.Data.Models;

    public class ResourceConfiguration : IEntityTypeConfiguration<Resource>
    {
        public void Configure(EntityTypeBuilder<Resource> resource)
        {
            resource
                .HasKey(r => r.ResourceId);

            resource
                .Property(r => r.Name)
                .HasMaxLength(DataValidation.Resource.NameMaxLength)
                .IsRequired(true)
                .IsUnicode(true);

            resource
                .Property(r => r.Url)
                .IsRequired(true)
                .IsUnicode(false);

            resource
                .Property(r => r.ResourceType)
                .IsRequired(true);

            resource
                .Property(r => r.CourseId)
                .IsRequired(true);
        }
    }
}
