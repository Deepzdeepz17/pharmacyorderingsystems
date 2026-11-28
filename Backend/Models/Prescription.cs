
using System.ComponentModel.DataAnnotations;

namespace Backend.Models;

public class Prescription
{
    public int PrescriptionId { get; set; }

    // References the shared Users table
    public int UserId { get; set; }

    [Required]
    [StringLength(255)]
    public string FileName { get; set; } = string.Empty;

    [Required]
    [StringLength(30)]
    public string Status { get; set; } = "Pending";

    public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
}
