using System.ComponentModel.DataAnnotations;

namespace Application.DTOs;

public class CreateDriverDTO
{
    
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    [EmailAddress]
    public required string Email { get; set; }
    [Phone]
    public required string PhoneNumber { get; set; }
    
}