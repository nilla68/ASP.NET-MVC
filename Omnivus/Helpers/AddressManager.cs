using Microsoft.EntityFrameworkCore;
using Omnivus.Data;

namespace Omnivus.Helpers
{
    public class AddressManager
    {
        private readonly ApplicationDbContext _context;

        public AddressManager(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddOrUpdateToAddressAsync(ApplicationUser user, ApplicationAddress address)
        {
            var existingAddress = await _context.Addresses.FirstOrDefaultAsync(x => x.Street == address.Street && x.PostCode == address.PostCode && x.City == address.City);
            if (existingAddress is null)
            {
                _context.Addresses.Add(address);
                await _context.SaveChangesAsync();

                await AddAddressToUserAsync(user, address);
            }
            else
            {
                await AddAddressToUserAsync(user, existingAddress);
            }
        }

        public async Task<ApplicationAddress> GetAddressAsync(ApplicationUser user)
        {
            var userAddress = await _context.UserAddresses
                .Include(ua => ua.Address)
                .Where(ua => ua.UserId == user.Id)
                .FirstOrDefaultAsync();

            if (userAddress is null)
                throw new Exception("User not found");

            return userAddress.Address;
        }

        private async Task AddAddressToUserAsync(ApplicationUser user, ApplicationAddress address)
        {
            var userAdddress = _context.UserAddresses.Where(ua => ua.UserId == user.Id).FirstOrDefault();
            if (userAdddress is not null)
                _context.UserAddresses.Remove(userAdddress);

            var newUserAdddress = new ApplicationUserAddress { UserId = user.Id, AddressId = address.Id };
            _context.UserAddresses.Add(newUserAdddress);
            
            await _context.SaveChangesAsync();
        }
    }
}
