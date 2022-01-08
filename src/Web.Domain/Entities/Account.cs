using Web.Domain.Interfaces;

namespace Web.Domain.Entities;
public class Account
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public Profile Profile { get; set; }
}