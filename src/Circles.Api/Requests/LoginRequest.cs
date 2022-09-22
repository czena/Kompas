using System.ComponentModel.DataAnnotations;

namespace Circles.Api.Requests;

public record LoginRequest([Required]string Login, [Required]string Password);