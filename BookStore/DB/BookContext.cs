using Microsoft.EntityFrameworkCore;
using BookStore.DB.Model;

namespace BookStore.DB;

public class BookContext : DbContext
{
    public DbSet<BookEntity> Books { get; set; } = null!;

    public DbSet<AuthorEntity> Authors { get; set; } = null!;
    public DbSet<PublishHouseEntity> PublishHouses { get; set; } = null!;

    public BookContext(DbContextOptions<BookContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<BookEntity>()
            .HasMany<AuthorEntity>(b => b.Authors)
            .WithMany(c => c.Books)
            .UsingEntity<BooksAuthors>(
                e =>
                    e.HasOne(x => x.AuthorEntity).WithMany().HasForeignKey(k => k.AuthorId),
                e =>
                    e.HasOne(x => x.BookEntity).WithMany().HasForeignKey(k => k.BookId)
            );

        modelBuilder.Entity<BookEntity>()
            .HasOne<PublishHouseEntity>(p => p.PublishingHouseEntity)
            .WithMany(c => c.Books).HasForeignKey(k => k.PublishingHouseId );
    }
}