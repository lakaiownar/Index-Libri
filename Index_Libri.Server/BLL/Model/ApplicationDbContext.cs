using Microsoft.EntityFrameworkCore;

namespace Index_Libri.Server.BLL.Model
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public virtual DbSet<ApplicationUser> ApplicationUser { get; set; }
        public virtual DbSet<BookList> BookList { get; set; }
        public virtual DbSet<Book> Book { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationUser>(entity =>
            {
                entity.HasKey(e => e.UserEmail);
                entity.Property(e => e.UserEmail);
                entity.Property(e => e.Token);

                // Configure one-to-one relationship with BookList
                entity.HasOne(e => e.BookList)
                      .WithOne(b => b.ApplicationUser)
                      .HasForeignKey<BookList>(b => b.UserEmail)
                      .IsRequired();
            });

            modelBuilder.Entity<BookList>(entity =>
            {
                entity.HasKey(e => e.BookListId);
                entity.Property(e => e.BookListId).ValueGeneratedOnAdd();
                entity.Property(e => e.UserEmail);

                // Configure one-to-many relationship with Book
                entity.HasMany(e => e.Books)
                      .WithOne(b => b.BookList)
                      .HasForeignKey(b => b.BookListId);
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasKey(e => e.ISBN);
                entity.Property(e => e.ISBN);
                entity.Property(e => e.GoogleId);
                entity.Property(e => e.Title);
                entity.Property(e => e.Author);
                entity.Property(e => e.Pages);
                entity.Property(e => e.Rating);
                entity.Property(e => e.BookCover);
                entity.Property(e => e.Status);
            });
        }
    }
}
