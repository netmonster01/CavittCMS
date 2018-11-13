using cavitt.net.Dtos;
using cavitt.net.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace cavitt.net.Interfaces
{
    public interface IUsersRepository
    {
      
        List<UserDto> GetAllUsersAsync();

        UserDto UpdateProfile(UserDto user);

        ApplicationUser UpdateProfile(ApplicationUser user);

    }
}
