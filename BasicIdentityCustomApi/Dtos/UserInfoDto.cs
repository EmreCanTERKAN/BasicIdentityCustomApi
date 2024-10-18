using BasicIdentityCustomApi.Entities;

namespace BasicIdentityCustomApi.Dtos
{
    public class UserInfoDto
    {
        // Verilerin veri tabanından çekilip Controllera gelmesidir.
        public int Id { get; set; }
        public string Email { get; set; }
        public UserType UserType { get; set; }

    }
}
