namespace EmiratesIslamic.Core.Models;

public class User
{
    public int Id { get; set; }

    public string FullName { get; set; }

    public string Email { get; set; }

    public string? ImagePath { get; set; }

    public string PhoneNumber { get; set; }

    public string Role { get; set; }

    public int RoleId { get; set; }

    public string UserName { get; set; }
}