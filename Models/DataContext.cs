using Microsoft.EntityFrameworkCore;
using System;

public class DataContext : DbContext
{

    public DataContext()
    {
    }
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=ManageOrderTicket;MultipleActiveResultSets=True;Integrated Security=False;User ID=sa;Password=123456");
        }
    }

    #region DbSet
    public DbSet<Cinema> Cinemas { get; set; }
    public DbSet<Film> Films { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<FilmSchedule> FilmSchedules { get; set; }
    #endregion

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cinema>(entity =>
        {
            entity.ToTable("Cinema");
            entity.HasKey(e => e.ID);
            entity.Property(e => e.Name).IsRequired();
            entity.HasIndex(e => e.Name).IsUnique();
        });
        modelBuilder.Entity<Film>(entity =>
        {
            entity.ToTable("Film");
            entity.HasKey(e => e.ID);
            entity.Property(e => e.Name).IsRequired();
            entity.HasIndex(e => e.Name).IsUnique();
        });
        modelBuilder.Entity<FilmSchedule>(entity =>
        {
            entity.ToTable("FilmSchedule");
            entity.HasKey( e => e.ID);
            entity.HasOne(e => e.Film)
                   .WithMany(f => f.FilmSchedules)
                   .HasForeignKey(e => e.FilmID)
                   .HasConstraintName("FK_FilmSchedule_Film");
            entity.HasOne(e => e.Cinema)
                   .WithMany(c => c.FilmSchedules)
                   .HasForeignKey(e => e.CinemaID)
                   .HasConstraintName("FK_FilmSchedule_Cinema");
        });
        modelBuilder.Entity<Order>(entity =>
        {
            entity.ToTable("Order");
            entity.HasKey(e => e.ID);
            entity.Property(e => e.AmountTicket).IsRequired();
            entity.HasOne(e => e.FilmSchedule)
                   .WithMany(o => o.Orders)
                   .HasForeignKey(e => e.FilmScheduleID)
                   .HasConstraintName("FK_Order_FilmSchedule");
        });
    }
}
