using System.ComponentModel.DataAnnotations;

namespace Circles.Api.Requests;

public record GetDescriptionRequest([Required]int Id);