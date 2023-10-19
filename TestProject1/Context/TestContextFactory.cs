using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Logging;

namespace TestProject1.Context;

public class TestContextFactorySqlite : IDesignTimeDbContextFactory<TestContextSqlite>, IContextFactory
{
    public TestContext CreateDbContext() => CreateDbContext(Array.Empty<string>());
    public TestContextSqlite CreateDbContext(string[] args)
    {
        var cs = @"Data Source=StrongTypedId.db";
        var builder = new DbContextOptionsBuilder<TestContext>().UseSqlite(cs).LogTo(Console.WriteLine, LogLevel.Information);
        var context = new TestContextSqlite(builder.Options);

        return context;
    }
}

public class TestContextFactorySqlServer : IDesignTimeDbContextFactory<TestContextSql>, IContextFactory
{
    public TestContext CreateDbContext() => CreateDbContext(Array.Empty<string>());
    public TestContextSql CreateDbContext(string[] args)
    {
        var cs = @"Server=(localdb)\mssqllocaldb;Database=StrongTypedId;Trusted_Connection=True;";
        var builder = new DbContextOptionsBuilder<TestContext>().UseSqlServer(cs).LogTo(Console.WriteLine, LogLevel.Information);
        var context = new TestContextSql(builder.Options);

        return context;
    }
}

public interface IContextFactory
{
    TestContext CreateDbContext();
}