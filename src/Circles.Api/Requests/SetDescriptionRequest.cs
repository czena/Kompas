using System.ComponentModel.DataAnnotations;

namespace Circles.Api.Requests;

public record SetDescriptionRequest([Required]int Id, string Description);