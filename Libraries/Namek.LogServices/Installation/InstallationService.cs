using System;
using System.Collections.Generic;
using Namek.Library.Entity.Logging;
using Namek.Library.Enums;
using Namek.Library.Helpers;
using Namek.LogServices.Logging;
using Namek.LogServices.Users;

namespace Namek.LogServices.Installation
{
    public class InstallationService : IInstallationService
    {
        #region Ctors

        public InstallationService(IActivityLogService activityLogService,
            IActivityLogTypeService activityLogTypeService, IUserService userService)
        {
            _activityLogService = activityLogService;
            _activityLogTypeService = activityLogTypeService;
            _userService = userService;
        }

        #endregion

        #region Public methods

        public virtual void InstallData(string defaultUserEmail, bool installSampleData = true)
        {
            //InstallActivityLogTypes();

            if (installSampleData)
                InstallActivityLog(defaultUserEmail);
        }

        #endregion

        #region private

        private readonly IActivityLogService _activityLogService;
        private readonly IActivityLogTypeService _activityLogTypeService;
        private readonly IUserService _userService;

        #endregion

        #region protected

        protected virtual void InstallActivityLogTypes()
        {
            var activityLogTypes = new List<ActivityLogType>
            {
                //admin area activities
                new ActivityLogType
                {
                    SystemKeyword = ActivityLogTypeEnum.AddNewServicePack.GetDisplayName(),
                    Enabled = true,
                    Name = ActivityLogTypeEnum.AddNewServicePack.GetDescription()
                },
                new ActivityLogType
                {
                    SystemKeyword = ActivityLogTypeEnum.EditServicePack.GetDisplayName(),
                    Enabled = true,
                    Name = ActivityLogTypeEnum.AddNewServicePack.GetDescription()
                },
                new ActivityLogType
                {
                    SystemKeyword = ActivityLogTypeEnum.DeleteServicePack.GetDisplayName(),
                    Enabled = true,
                    Name = ActivityLogTypeEnum.DeleteServicePack.GetDescription()
                }
            };
            _activityLogTypeService.Insert(activityLogTypes);
        }

        protected virtual void InstallActivityLog(string defaultUserEmail)
        {
            //default customer/user
            var defaultCustomer = _userService.FirstOrDefault(x => x.Email == defaultUserEmail);
            if (defaultCustomer == null)
                throw new Exception("Cannot load default user");

            var activityLogTypeName = ActivityLogTypeEnum.EditServicePack.GetDisplayName();

            _activityLogService.Insert(new ActivityLog
            {
                ActivityLogType = _activityLogTypeService.First(alt => alt.SystemKeyword.Equals(activityLogTypeName)),
                Comment = "Sửa gói dịch vụ ('Cloud Server')",
                CreatedOnUtc = DateTime.UtcNow,
                User = defaultCustomer,
                IpAddress = "127.0.0.1"
            });
        }

        #endregion
    }
}