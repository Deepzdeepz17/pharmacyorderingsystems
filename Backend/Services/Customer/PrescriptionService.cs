
using Microsoft.AspNetCore.Http;

namespace Backend.Services.Customer;

public class PrescriptionService
{
    private readonly string _uploadFolder;

    private const long MaxFileSize = 5 * 1024 * 1024;

    private static readonly string[] AllowedExtensions =
    {
        ".pdf",
        ".jpg",
        ".jpeg",
        ".png"
    };

    public PrescriptionService(IWebHostEnvironment environment)
    {
        _uploadFolder = Path.Combine(
            environment.ContentRootPath,
            "PrivateUploads",
            "Prescriptions"
        );
    }

    public async Task<string> UploadPrescriptionAsync(IFormFile file)
    {
        if (file == null || file.Length == 0)
            throw new ArgumentException("Please select a valid file.");

        if (file.Length > MaxFileSize)
            throw new ArgumentException("File size cannot exceed 5 MB.");

        string extension = Path.GetExtension(file.FileName).ToLowerInvariant();

        if (!AllowedExtensions.Contains(extension))
            throw new ArgumentException("Only PDF, JPG and PNG files are allowed.");

        Directory.CreateDirectory(_uploadFolder);

        string uniqueFileName = $"{Guid.NewGuid()}{extension}";

        string filePath = Path.Combine(_uploadFolder, uniqueFileName);

        await using (var stream = new FileStream(filePath, FileMode.CreateNew))
        {
            await file.CopyToAsync(stream);
        }

        return uniqueFileName;
    }
}
