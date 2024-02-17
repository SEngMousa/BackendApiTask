namespace Application.DTOs;

public class DriverAlphabetizedDTO
{
    
    public int Id { get; set; }
    public required string UserName { get; set; }
 
    public required string Email { get; set; }
    public required string PhoneNumber { get; set; }
}