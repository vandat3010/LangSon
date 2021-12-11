using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Namek.Entity.EntityModel
{
    public class ReporterPreAgency
    {
        public int Id { get; set; }
        public int ReporterId { get; set; }
        public int PreAgencyId { get; set; }
        public int DisplayOrder { get; set; }
    }
}