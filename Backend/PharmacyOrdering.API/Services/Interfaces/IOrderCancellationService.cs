
using PharmacyOrdering.API.DTOs.Cancellations;

namespace PharmacyOrdering.API.Services.Interfaces;

public interface IOrderCancellationService
{
    Task<CancellationResponseDto> RequestCancellationAsync(
        int orderId,
        int customerId,
        CreateCancellationDto dto);

    Task<IEnumerable<CancellationResponseDto>> GetMyCancellationsAsync(
        int customerId);

    Task<bool> ReviewCancellationAsync(
        int cancellationId,
        ReviewCancellationDto dto,
        int adminId);

    Task<IEnumerable<CancellationResponseDto>> GetAllCancellationsAsync();
}
