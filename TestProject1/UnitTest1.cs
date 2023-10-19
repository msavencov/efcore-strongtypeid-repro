using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TestProject1.Context;
using TestProject1.Entities;
using Xunit;

namespace TestProject1;

public class UnitTest1
{
    [Theory]
    [InlineData(typeof(TestContextFactorySqlite))]
    [InlineData(typeof(TestContextFactorySqlServer))]
    public void Test1(Type factoryType)
    {
        var factory = (IContextFactory)Activator.CreateInstance(factoryType)!;

        using (var context = factory.CreateDbContext())
        {
            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();

                context.Set<Person>().Add(new Person { Name = $"test {context.GetType().Name}" });
                context.SaveChanges();
            }
        }

        using (var context = factory.CreateDbContext())
        {
            var personId = new PersonId(1);
            var personIdInteger = 1;
            
            var typed = context.Set<Person>().FirstOrDefault(t => t.Id == personId); // works as expected
            var person = context.Set<Person>().FirstOrDefault(t => t.Id == personIdInteger); // throws InvalidCastException int -> PersonId

            Assert.NotNull(typed);
            Assert.NotNull(person);
        }
    }
}