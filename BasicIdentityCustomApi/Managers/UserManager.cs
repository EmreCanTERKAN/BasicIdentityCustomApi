using BasicIdentityCustomApi.Data;
using BasicIdentityCustomApi.Dtos;
using BasicIdentityCustomApi.Entities;
using BasicIdentityCustomApi.Services;
using BasicIdentityCustomApi.Types;

namespace BasicIdentityCustomApi.Managers
{
    public class UserManager : IUserService
    {

        private readonly IdentityDbContext _context;

        public UserManager(IdentityDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceMessage> AddUser(AddUserDto userDto)
        {
            var user = new UserEntity()
            {
                Email = userDto.Email,
                Password = userDto.Password,
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return  new ServiceMessage
            {
                IsSucceed = true,
                Message = "Kayıt Başarıyla Oluşturuldu"
            };
        }
    }
}
