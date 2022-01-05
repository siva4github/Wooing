using System.ComponentModel.DataAnnotations;

namespace WooingApi.Dtos;

public class RegisterDto
{
    [Required] public string UserName { get; set; }
    [Required] public string Password { get; set; }
}