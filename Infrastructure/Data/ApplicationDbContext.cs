using Domain.Models;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options) {}


        public DbSet<Chat> Chats { get; set; }
        public DbSet<Choice> Choices { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<Goal> Goals { get; set; }
        public DbSet<ChecklistItem> ChecklistItems { get; set; }
        public DbSet<MoodEntries> MoodEntries { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Survey> Surveys { get; set; }
        public DbSet<Supervisor> Supervisors { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<SurveyResponse> SurveyResponses { get; set; }
        public DbSet<QuestionResponse> QuestionResponses { get; set; }
        public DbSet<Acheivements> Acheivements { get; set; }
        public DbSet<StudentsAcheivements>  StudentsAcheivements { get; set; }
        public DbSet<ChatGroupMessage> ChatGroups { get; set; }
        public DbSet<MentalHealth> MentalHealthRecords { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Student>().HasOne<ApplicationUser>()
                .WithOne().HasForeignKey<Student>(s => s.UserId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Supervisor>().HasOne<ApplicationUser>()
                .WithOne().HasForeignKey<Supervisor>(s => s.UserId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Doctor>().HasOne<ApplicationUser>()
                .WithOne().HasForeignKey<Doctor>(d => d.UserId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SurveyResponse>().HasOne(sr => sr.Student)
                .WithMany(s => s.SurveyResponses).HasForeignKey(sr => sr.StudentId).OnDelete(DeleteBehavior.Cascade);

            //modelBuilder.Entity<QuestionResponse>().HasOne(qr => qr.SurveyResponse)
            //    .WithMany(sr => sr.QuestionResponses).HasForeignKey(qr => qr.SurveyResponseId).OnDelete(DeleteBehavior.NoAction);

            //modelBuilder.Entity<QuestionResponse>().HasOne(qr => qr.Choice)
            //    .WithMany().HasForeignKey(qr => qr.ChoiceId).OnDelete(DeleteBehavior.NoAction);

            //modelBuilder.Entity<QuestionResponse>().HasOne(qr => qr.Question)
            //    .WithMany().HasForeignKey(qr => qr.QuestionId).OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Goal>().HasOne(g => g.Student)
                .WithMany(s => s.Goals).HasForeignKey(g => g.StudentId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MoodEntries>().HasOne(m => m.Student)
                .WithMany(s => s.MoodEntries).HasForeignKey(m => m.StudentId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Chat>().HasOne(c => c.Sender)
                .WithMany(s => s.SentChats).HasForeignKey(c => c.SenderId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Chat>().HasOne(c => c.Receiver)
                .WithMany(s => s.ReceivedChats).HasForeignKey(c => c.ReceiverId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Notification>().HasOne<ApplicationUser>()
                .WithMany().HasForeignKey(n => n.UserId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Question>().HasOne(q => q.Survey)
                .WithMany(s => s.Questions).HasForeignKey(q => q.SurveyId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Choice>().HasOne(c => c.Question)
                .WithMany(s => s.Choices).HasForeignKey(c => c.QuestionId).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<StudentsAcheivements>().HasKey(c => new { c.Id , c.StudentId});

            // Optional Settings 

            modelBuilder.Entity<Notification>()
                .Property(n => n.Title).HasMaxLength(200).IsRequired();

            modelBuilder.Entity<Notification>()
                .Property(n => n.Content).HasMaxLength(2000).IsRequired();
        }
    }
}