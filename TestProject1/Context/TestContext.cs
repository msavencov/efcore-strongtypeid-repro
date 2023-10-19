using Microsoft.EntityFrameworkCore;
using TestProject1.Entities;

namespace TestProject1.Context;

public class TestContextSqlite : TestContext
{
    public TestContextSqlite(DbContextOptions<TestContext> options) : base(options)
    {
    }
}

public class TestContextSql : TestContext
{
    public TestContextSql(DbContextOptions<TestContext> options) : base(options)
    {
    }
}
public abstract class TestContext : DbContext
{
    public TestContext(DbContextOptions<TestContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var person = modelBuilder.Entity<Person>();
        
        person.HasKey(t => t.Id);
        person.Property(t => t.Id).HasConversion<PersonIdConverter>().ValueGeneratedOnAdd();
    }
}