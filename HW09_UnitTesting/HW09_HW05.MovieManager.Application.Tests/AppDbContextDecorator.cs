using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW09_HW05.MovieManager.Application.Tests;
public class AppDbContextDecorator<T>(DbContextOptions<T> options) where T : DbContext
{
    private T CreateDbContextInstance() => (T)Activator.CreateInstance(typeof(T), options)!;

    public void AddAndSave<TEntity>(TEntity entityToSave) where TEntity : class
        => Using(CreateDbContextInstance(), context =>
        {
            context.Add(entityToSave);
            context.SaveChanges(); 
        });


    public void AddAndSaveRange<TEntity>(IEnumerable<TEntity> entitiesToSave) where TEntity : class
        => Using(CreateDbContextInstance(), context =>
        {
            context.Add(entitiesToSave);
            context.SaveChanges();
        });

    public void Assert(Action<T> assert) => Using(CreateDbContextInstance(), assert);

    public void Clar() => Using(CreateDbContextInstance(), context => context.Database.EnsureDeleted());

    private static void Using<TDisposable>(TDisposable disposable, Action<TDisposable> action) where TDisposable : IDisposable
    {
        using(disposable)
            action(disposable);
    }
}
