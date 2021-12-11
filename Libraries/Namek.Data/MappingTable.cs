using Namek.Data.Database;
using Namek.Library.Entity.Logging;
using Namek.Library.Entity.Modules; 
using Namek.Library.Entity.Pages;
using Namek.Library.Entity.PermissionActions; 
using Namek.Library.Entity.Users;

namespace Namek.Data
{
    public class UserMap : BaseEntityConfiguration<User> { }

    public class ActivityLogTypeMap : BaseEntityConfiguration<ActivityLogType> { }

    public class ActivityLogMap : BaseEntityConfiguration<ActivityLog> { }

    public class LogMap : BaseEntityConfiguration<Log> { }
     
 

    public class ModuleMap : BaseEntityConfiguration<Module> { }

    public class PermissionActionMap : BaseEntityConfiguration<PermissionAction> { }

    public class PageMap : BaseEntityConfiguration<Page> { }
}