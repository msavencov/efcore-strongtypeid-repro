using System;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace TestProject1.Entities;

public class Person
{
    public PersonId Id { get; set; }
    public string Name { get; set; }
}

public readonly struct PersonId : IEquatable<PersonId>
{
    public readonly int Value;

    public PersonId(int value)
    {
        Value = value;
    }

    public static bool operator ==(PersonId id, int other) => id.Value == other;
    public static bool operator !=(PersonId id, int other) => !(id == other);

    public static bool operator ==(PersonId id, PersonId other) => id.Value == other.Value;
    public static bool operator !=(PersonId id, PersonId other) => !(id == other);
    
    public static explicit operator PersonId(int value) => new(value);
    public static explicit operator int(PersonId id) => id.Value;
    
    public bool Equals(PersonId other)
    {
        return Value == other.Value;
    }

    public override bool Equals(object? obj)
    {
        return obj is PersonId other && Equals(other);
    }

    public override int GetHashCode()
    {
        return Value;
    }
}

public class PersonIdConverter : ValueConverter<PersonId, int>
{
    public PersonIdConverter() : base(personId => personId.Value, value => new PersonId(value))
    {
    }
}

