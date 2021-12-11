using Namek.Admin.CustomFilters;
using System.Web.Mvc;
using Namek.Library.Enums;
using Namek.Admin.AttributeCustom; 
using Namek.Library.Infrastructure;
using Namek.Core.ActionResult; 
using Namek.Entity.EntityNewModel;
using Namek.Data.UnitOfWork;
using Namek.Core;
using System;
using Action = Namek.Library.Enums.Action;
using Namek.Library.Helpers;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Linq;

namespace Namek.Admin.Controllers
{
    [Authorize]
    [AuthenticateUser]
    public class CommonController : AdminBaseController
    {
        //private readonly IUserRepository _userRepo = EngineContext.Current.Resolve<IUserRepository>(); 
        IUnitOfWork _unit;
        public CommonController()
        {
            _unit = new UnitOfWork();
        }
  
        public JsonResult GetListUserByRoleId(RequestUserModel request)
        {
            Expression<Func<User, bool>> where = x => true;
            if (request.RoleId > 0)
            {
                where = ExpressionHelpers.CombineAnd(where, a => a.RoleId == request.RoleId && a.IsDelete == false && a.IsActive == true);
            }
            var items = _unit.Users.Get(where, "Id", false, 0, 10000);

            var model = from g in items select new { g.Id, g.FullName };

            return RespondSuccess(model);
        }

        //Lấy danh sách nhân đại lý
        [HttpGet]
        [Route("get")]
        public JsonResult GetListAgency()
        {
            Expression<Func<User, bool>> where = x => true;
            where = ExpressionHelpers.CombineAnd(where, a => a.RoleName.Contains("Agency") && a.IsDelete == false && a.IsActive == true);
            var items = _unit.Users.Get(where, "Id", false, 0, 10000);

            var model = from g in items select new { agencyId = g.Id, fullName = g.FullName };

            return RespondSuccess(model);
        }
         
       
    }
    public class RequestUserModel
    {
        public int? Count { get; set; }

        public int? Page { get; set; }

        public bool? Ascending { get; set; }

        public string Name { get; set; }
        public string RoleName { get; set; }
        public string Mobile { get; set; }

        public string Email { get; set; }

        public string DateFrom { get; set; }

        public string DateTo { get; set; }

        public int? CategoryId { get; set; }
        public byte? RoleId { get; set; }
        public int? ProvinceId { get; set; }
    }
}