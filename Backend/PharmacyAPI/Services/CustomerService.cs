
using PharmacyAPI.DTOs.Customer;
using PharmacyAPI.Interfaces;
using PharmacyAPI.Models;

namespace PharmacyAPI.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        // GET ALL CUSTOMERS
        public async Task<List<CustomerResponseDto>> GetAllCustomersAsync()
        {
            var customers =
                await _customerRepository.GetAllCustomersAsync();

            return customers.Select(MapToDto).ToList();
        }

        // GET CUSTOMER BY ID
        public async Task<CustomerResponseDto?> GetCustomerByIdAsync(int id)
        {
            var customer =
                await _customerRepository.GetCustomerByIdAsync(id);

            if (customer == null)
                return null;

            return MapToDto(customer);
        }

        // SEARCH CUSTOMERS
        public async Task<List<CustomerResponseDto>> SearchCustomersAsync(
            string searchTerm)
        {
            var customers =
                await _customerRepository.SearchCustomersAsync(searchTerm);

            return customers.Select(MapToDto).ToList();
        }

        // ACTIVATE OR DEACTIVATE CUSTOMER
        public async Task<CustomerResponseDto?> UpdateCustomerStatusAsync(
            int id, CustomerStatusDto dto)
        {
            var customer =
                await _customerRepository.GetCustomerByIdAsync(id);

            if (customer == null)
                return null;

            customer.IsActive = dto.IsActive;

            await _customerRepository.UpdateAsync(customer);

            return MapToDto(customer);
        }

        // MAP USER TO CUSTOMER DTO
        private static CustomerResponseDto MapToDto(User customer)
        {
            return new CustomerResponseDto
            {
                Id = customer.Id,
                Name = customer.Name,
                Email = customer.Email,
                PhoneNumber = customer.PhoneNumber,
                Address = customer.Address,
                IsActive = customer.IsActive,
                CreatedAt = customer.CreatedAt
            };
        }
    }
}
