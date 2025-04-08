using Business.Dtos;
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
            Email = form.Email,
            ImageFile = form.ImageFile,
            PhoneNumber = form.Phone,
            JobTitle = form.JobTitle,
            Address = new MemberAddressEntity { StreetName = form.Street, PostalCode = form.PostalCode, City = form.City},
        };
    }

    public static Member CreateModel(MemberEntity entity)
    {
        return new Member
        {
            Id = entity.Id,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            JobTitle = entity.JobTitle,
            Email = entity.Email!,
            Phone = entity.PhoneNumber,
            ImageFileName = entity.ImageFile,
            Street = entity.Address!.StreetName,
            City = entity.Address.City,
            PostalCode = entity.Address.PostalCode
        };
    }

   public static MemberEntity CreateEntity(MemberUpdateForm form, MemberEntity existingEntity)
{
    existingEntity.FirstName = form.FirstName;
    existingEntity.LastName = form.LastName;
    existingEntity.PhoneNumber = form.Phone;
    existingEntity.JobTitle = form.JobTitle;
    existingEntity.ImageFile = form.ImageFileName;

    if (form.Day > 0 && form.Month > 0 && form.Year > 0)
    {
        existingEntity.DateOfBirth = new DateOnly(form.Year, form.Month, form.Day);
    }

    if (existingEntity.Address == null)
        existingEntity.Address = new MemberAddressEntity();

    existingEntity.Address.StreetName = form.Street;
    existingEntity.Address.City = form.City;
    existingEntity.Address.PostalCode = form.PostalCode;

    return existingEntity;
}
}
