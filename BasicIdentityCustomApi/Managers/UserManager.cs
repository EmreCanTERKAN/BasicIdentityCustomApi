using BasicIdentityCustomApi.Data;
using BasicIdentityCustomApi.Dtos;
using BasicIdentityCustomApi.Entities;
using BasicIdentityCustomApi.Services;
using BasicIdentityCustomApi.Types;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

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
            var userEntity = _context.Users.Where(x => x.Email.ToLower() == userDto.Email.ToLower()).FirstOrDefault();

            if (userEntity is null)
            {
                var user = new UserEntity()
                {
                    Email = userDto.Email,
                    Password = userDto.Password,
                };

                _context.Users.Add(user);
                _context.SaveChanges();

                return new ServiceMessage
                {
                    IsSucceed = true,
                    Message = "Kayıt Başarıyla Oluşturuldu"
                };

            }

            return new ServiceMessage
            {
                IsSucceed = false,
                Message = "Bu email kayıtlarımızda mevcut, lütfen başka bir e mail deneyiniz."
            };

            
        }


        // emre@gmail.com - 12345 <== denenen bilgiler
        public async Task<ServiceMessage<UserInfoDto>> LoginUser(LoginUserDto user)
        {
            var userEntity = _context.Users.Where(x => x.Email.ToLower() == user.Email.ToLower()).FirstOrDefault();

            if (userEntity is null)
            {
                return new ServiceMessage<UserInfoDto>
                    
                {
                    IsSucceed = false,
                    Message = "Kullanıcı adı veya şifre hatalı",
                    
                   
                };
            }

            if (userEntity.Password == user.Password)
            {
                return new ServiceMessage<UserInfoDto>
                {

                    IsSucceed = true,
                    Data = new UserInfoDto
                    {
                        Id = userEntity.Id,
                        Email = userEntity.Email,
                        UserType = userEntity.UserType,
                    }

                };
            }
            else
            {
                return new ServiceMessage<UserInfoDto>
                {
                    IsSucceed = false,
                    Message = "Kullanıcı adı veya şifre hatalı",
                   
                    
                };
            }
            

        }
    }
}
