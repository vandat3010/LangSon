using Namek.Entity.RequestModel;

namespace Namek.Admin.Models.ActivityLog
{
    public class UserModel : BaseModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
    }
}