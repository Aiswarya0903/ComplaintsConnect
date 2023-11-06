using Complaints.Data.Models;
using Complaints.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;
using System.Reflection.Emit;

namespace Complaints.Data
{
    public partial class ComplaintsDbContext: DbContext
    {
        private string _Connstr;
        public ComplaintsDbContext(string Connstr)
        {
            _Connstr = Connstr;
        }

        public ComplaintsDbContext(DbContextOptions<ComplaintsDbContext> options)
           : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_Connstr);
                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                //optionsBuilder.UseSqlServer("Data Source=34.238.198.17;Initial Catalog=Fin_Gatway;User ID=halcyonapp;Password=nw21dbu$3r;  persist Security Info=true; Connect Timeout=120;");
            }
        }

         public virtual DbSet<Product> Product { get; set; }
        
         public virtual DbSet<Company> Company { get; set; }
         
         public virtual DbSet<Complaint> Complaint { get; set; }
         public virtual DbSet<State> State { get; set; }   

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.ProductId);
                entity.Property(e => e.CompanyId);
                entity.Property(e => e.ProductName)
                    .HasMaxLength(500)
                    .IsUnicode(false);
                
            });
            
            modelBuilder.Entity<Company>(entity =>
            {
                entity.HasKey(e => e.CompanyId);
                entity.Property(e => e.CompanyName)
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });
            modelBuilder.Entity<State>(entity =>
            {
                entity.HasKey(e => e.StateId);
                entity.Property(e => e.StateName)
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Complaint>(entity =>
            {
                entity.HasKey(e => e.ComplaintId);
                entity.Property(e => e.ProductId);
                entity.Property(e => e.CompanyId);
                entity.Property(e => e.StateId);
                entity.Property(e => e.ConsumerDisputed)
                   .HasMaxLength(500)
                   .IsUnicode(false);
                entity.Property(e => e.CompanyResponse)
                   .HasMaxLength(500)
                   .IsUnicode(false);
                entity.Property(e => e.Submittedvia)
                   .HasMaxLength(500)
                   .IsUnicode(false);
                entity.Property(e => e.Issue)
                   .HasMaxLength(500)
                   .IsUnicode(false);
                entity.Property(e => e.SubIssue)
                   .HasMaxLength(500)
                   .IsUnicode(false);
                entity.Property(e => e.Timely)
                   .HasMaxLength(500)
                   .IsUnicode(false);
                entity.Property(e => e.ConsumerConsent)
                   .HasMaxLength(500)
                   .IsUnicode(false);
                entity.Property(e => e.ZipCode)
                   .HasMaxLength(500)
                   .IsUnicode(false);
                entity.Property(e => e.SubProduct)
                    .HasMaxLength(500)
                    .IsUnicode(false);
                entity.Property(e => e.DateReceived).HasColumnType("datetime");
                entity.Property(e => e.DateSentToCompany).HasColumnType("datetime");
                entity.Property(e => e.ComplaintWhatHappened)
                    .HasMaxLength(500)
                    .IsUnicode(false);
                entity.Property(e => e.Tags)
                    .HasMaxLength(500)
                    .IsUnicode(false);
                entity.Property(e => e.HasNarrative)
                    .HasDefaultValue(false);
               
                
            });
            


        }
    }
}
