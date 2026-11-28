
using System.ComponentModel.DataAnnotations;

namespace Backend.DTOs.Customer;

// Prescription upload request
public class PrescriptionUploadRequest
{
    [Required(ErrorMessage = "Please select a prescription file")]
    public IFormFile File { get; set; } = null!;
}

// Prescription response
public class PrescriptionResponse
{
    public int PrescriptionId { get; set; }

    public string FileName { get; set; } = string.Empty;

    public string Status { get; set; } = "Pending";

    public DateTime UploadedAt { get; set; }
}
