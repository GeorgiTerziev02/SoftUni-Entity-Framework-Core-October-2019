namespace P01_StudentSystem.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using P01_StudentSystem.Data.Models;

    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> course)
        {
            course
                .HasKey(c => c.CourseId);

            course
                .Property(c => c.Name)
                .HasMaxLength(DataValidation.Course.NameMaxLength)
                .IsUnicode(true)
                .IsRequired(true);

            course
                .Property(c => c.Description)
                .IsRequired(false)
                .IsUnicode(true);

            course
                .Property(c => c.StartDate)
                .IsRequired(true);

            course
                .Property(c => c.EndDate)
                .IsRequired(true);

            course
                .Property(c => c.Price)
                .IsRequired(true);

            course
                .HasMany(c => c.HomeworkSubmissions)
                .WithOne(h => h.Course)
                .HasForeignKey(h => h.CourseId);

            course
                .HasMany(c => c.StudentsEnrolled)
                .WithOne(s => s.Course)
                .HasForeignKey(s => s.CourseId);

            course
                .HasMany(c => c.Resources)
                .WithOne(r => r.Course)
                .HasForeignKey(r => r.CourseId);
        }
    }
}
