using cavitt.net.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace cavitt.net.Interfaces
{
    public interface IRolesRepository
    {
        Task<bool> CreateInitialRoles();
        List<RoleDto> GetAllRoles();
        Task<bool> AssignRole(string userName, string roleName);
        Task<List<string>> GetUserRolesAsync(string userName);
    }
}
