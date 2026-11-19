using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinFormsAppSport.Data;

namespace WinFormsAppSport.Services;

public class WorkoutService
{
    private readonly WorkoutContext _context;

    public WorkoutService(WorkoutContext context)
    {
        _context = context;
    }

    public List<Workout> GetAll()
    {
        return _context.Workouts.ToList();
    }

    public Workout? GetById(int id)
    {
        return _context.Workouts.FirstOrDefault(x => x.Id == id);
    }

    public void Add(Workout workout)
    {
        _context.Workouts.Add(workout);
        _context.SaveChanges();
    }
}

