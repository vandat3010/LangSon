using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Namek.Entity.EntityModel
{
    public class ReporterCurrentAgency
    {
        public int Id { get; set; }
        public int ReporterId { get; set; }
        public int CurrentAgencyId { get; set; }
        public int DisplayOrder { get; set; }
    }
}