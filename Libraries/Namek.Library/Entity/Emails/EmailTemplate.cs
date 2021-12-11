using Namek.Library.Data;

namespace Namek.Library.Entity.Emails
{
    public class EmailTemplate : BaseEntity
    {
        public string TemplateName { get; set; }

        public string TemplateSystemName { get; set; }

        public string Template { get; set; }

        public bool IsMaster { get; set; }

        public int? ParentEmailTemplateId { get; set; }

        public virtual EmailTemplate ParentEmailTemplate { get; set; }

        public int? EmailAccountId { get; set; }

        public virtual EmailAccount EmailAccount { get; set; }

        public string Subject { get; set; }

        public string AdministrationEmail { get; set; }

        public bool IsSystem { get; set; }
    }
}