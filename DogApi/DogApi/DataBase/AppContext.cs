using DogApi.Models;
using Microsoft.EntityFrameworkCore;

namespace DogApi.DataBase;

public class AppContext : DbContext
{
    public DbSet<Dog> Dogs { get; set; }

    public AppContext(DbContextOptions options) : base(options)
    {
    }
}