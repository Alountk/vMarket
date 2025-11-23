using System.Text.RegularExpressions;

namespace Videogames.Domain.ValueObjects;

public class Email
{
    private static readonly Regex EmailRegex = new(
        @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
        RegexOptions.Compiled | RegexOptions.IgnoreCase);

    public string Value { get; private set; }

    private Email(string value)
    {
        Value = value;
    }

    public static Email Create(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            throw new ArgumentException("Email cannot be empty", nameof(email));
        }

        if (!EmailRegex.IsMatch(email))
        {
            throw new ArgumentException("Invalid email format", nameof(email));
        }

        return new Email(email.ToLowerInvariant());
    }

    public override string ToString() => Value;

    public override bool Equals(object? obj)
    {
        if (obj is Email other)
        {
            return Value.Equals(other.Value, StringComparison.OrdinalIgnoreCase);
        }
        return false;
    }

    public override int GetHashCode() => Value.GetHashCode();
}
