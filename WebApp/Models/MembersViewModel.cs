using Business.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Models;

public class MembersViewModel
{
    public AddMemberForm AddForm { get; set; } = new();
    public EditMemberForm EditForm { get; set; } = new();
    public IEnumerable<Member> Members { get; set; } = [];

    public List<SelectListItem> Days { get; } = Enumerable.Range(1, 31)
       .Select(d => new SelectListItem { Value = d.ToString(), Text = d.ToString() })
       .ToList();

    public List<SelectListItem> Months { get; } = new List<SelectListItem>
    {
        new("January", "1"),
        new("February", "2"),
        new("March", "3"),
        new("April", "4"),
        new("May", "5"),
        new("June", "6"),
        new("July", "7"),
        new("August", "8"),
        new("September", "9"),
        new("October", "10"),
        new("November", "11"),
        new("December", "12"),
    };

    public List<SelectListItem> Years { get; } = Enumerable.Range(1900, DateTime.Now.Year - 1899)
        .Reverse()
        .Select(y => new SelectListItem { Value = y.ToString(), Text = y.ToString() })
        .ToList();

}

