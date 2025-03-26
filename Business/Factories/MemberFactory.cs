using Business.Models;
using Data.Entities;

namespace Business.Factories;

public static class MemberFactory
{
    public static MemberEntity CreateEntity(MemberRegistrationForm form)
    {
        return new MemberEntity
        {
            UserName = form.Email,
            FirstName = form.FirstName,
            LastName = form.LastName,
            Email = form.Email
        };
    }
}
