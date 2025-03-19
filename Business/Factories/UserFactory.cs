using Business.Models;
using Data.Models;

namespace Business.Factories;

public static class UserFactory
{
    public static AppUser Create(CreateAccountForm form)
    {
        return new AppUser
        {
            UserName = form.Email,
            FirstName = form.FirstName,
            LastName = form.LastName,
            Email = form.Email
        };
    }
}
