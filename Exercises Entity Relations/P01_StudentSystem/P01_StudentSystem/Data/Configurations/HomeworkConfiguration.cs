namespace P01_StudentSystem.Data.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using P01_StudentSystem.Data.Models;

    public class HomeworkConfiguration : IEntityTypeConfiguration<Homework>
    {
        public void Configure(EntityTypeBuilder<Homework> homework)
        {
            homework
                .HasKey(h => h.HomeworkId);

            homework
                .Property(h => h.Content)
                .IsUnicode(false)
                .IsRequired(true);

            homework
                .Property(h => h.ContentType)
                .IsRequired(true);

            homework
                .Property(h => h.SubmissionTime)
                .IsRequired(true);

            homework
                .Property(h => h.StudentId)
                .IsRequired(true);

            homework
                .Property(h => h.CourseId)
                .IsRequired(true);
        }
    }
}
