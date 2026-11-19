using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsAppSport.Data;

public class WorkoutContext : DbContext
{
    public DbSet<Workout> Workouts { get; set; }

    public WorkoutContext(DbContextOptions<WorkoutContext> options)
        : base(options)
    {
    }

    public WorkoutContext()
    {
    }

    protected override void OnConfiguring(
        DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite("Data Source=workouts.db");
        }
    }
}

