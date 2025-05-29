using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace ComparacaoPrecos.Models;

public class UserViewModel
{
    [Required]
    public String Username { get; set; }

    [Required]
    public String Role { get; set; }

    public String Password { get; set; }

}