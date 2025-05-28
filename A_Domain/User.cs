namespace Domain;

using System;
using System.Text.RegularExpressions;

public class User
{
    private string _emailAddress;
    private string _password;

    public string Username
    {
        get => _emailAddress;
        set
        {
            if (!IsValidEmail(value))
                throw new ArgumentException("Username must be a valid email address.");
            _emailAddress = value;
        }
    }

    public string Password
    {
        get => _password;
        set
        {
            if (!IsValidPassword(value))
                throw new ArgumentException("Password must be at least 6 characters long and contain at least one uppercase letter, one lowercase letter, one number, and one special character.");
            _password = value;
        }
    }

    private bool IsValidEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return false;

        string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        return Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase);
    }

    private bool IsValidPassword(string password)
    {
        if (string.IsNullOrEmpty(password) || password.Length < 6)
            return false;

        var pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&]).{6,}$";
        return Regex.IsMatch(password, pattern);
    }
}
