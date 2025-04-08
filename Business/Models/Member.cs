using Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace Business.Models;

public class Member
{
    public string Id { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? JobTitle { get; set; }
    public string? ImageFileName { get; set; }
    public string? Phone { get; set; }
    public string? Street { get; set; }
    public string? City { get; set; }
    public string? PostalCode { get; set; }
    public int Day { get; set; }
    public int Month { get; set; }
    public int Year { get; set; }

}
