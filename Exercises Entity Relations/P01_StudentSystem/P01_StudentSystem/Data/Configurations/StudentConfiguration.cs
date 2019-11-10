namespace P01_StudentSystem.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using P01_StudentSystem.Data.Models;

    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> student)
        {
            student
                .HasKey(s => s.StudentId);

            student
                .Property(s => s.Name)
                .HasMaxLength(DataValidation.Student.NameMaxLength)
                .IsUnicode(true)
                .IsRequired(true);

            student
                .Property(s => s.PhoneNumber)
                .HasMaxLength(DataValidation.Student.PhoneLength)
                .IsRequired(false)
                .IsUnicode(false);

            student
                .Property(s => s.RegisteredOn)
                .IsRequired(true);

            student
                .Property(s => s.Birthday)
                .IsRequired(false);

            student
                .HasMany(s => s.HomeworkSubmissions)
                .WithOne(h => h.Student)
                .HasForeignKey(h=>h.StudentId);

            student
                .HasMany(s => s.CourseEnrollments)
                .WithOne(c => c.Student)
                .HasForeignKey(c => c.StudentId);
        }
    }
}
