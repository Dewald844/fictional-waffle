namespace Controller;

using System;
using System.Threading.Tasks;

public class UserService
{
    private readonly Database.UserRepository _userRepository;

    public UserService(Database.UserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Domain.User> AuthenticateUserAsync(string email, string password)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email cannot be empty.", nameof(email));
        if (string.IsNullOrWhiteSpace(password))
            throw new ArgumentException("Password cannot be empty.", nameof(password));

        var user = await _userRepository.GetUserByEmailAsync(email);

        if (user == null)
            return null;

        // Replace this with secure password hash
        if (user.Password == password)
            return user;

        return null;
    }
}
