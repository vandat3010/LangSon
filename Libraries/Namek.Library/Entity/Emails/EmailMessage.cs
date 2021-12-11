using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Net.Mail;
using Namek.Library.Data;
using Newtonsoft.Json;

namespace Namek.Library.Entity.Emails
{
    /// <summary>
    ///     Specifies fields used for sending emails
    /// </summary>
    public class EmailMessage : BaseEntity
    {
        private string _attachmentSerialized = string.Empty;
        public string To { get; set; }

        [NotMapped]
        public IList<UserInfo> Tos { get; set; }

        public string TosSerialized
        {
            get => JsonConvert.SerializeObject(Tos);
            set => Tos = JsonConvert.DeserializeObject<List<UserInfo>>(value);
        }

        [NotMapped]
        public IList<UserInfo> Ccs { get; set; }

        public string CcsSerialized
        {
            get => JsonConvert.SerializeObject(Ccs);
            set => Ccs = JsonConvert.DeserializeObject<List<UserInfo>>(value);
        }

        [NotMapped]
        public IList<UserInfo> Bccs { get; set; }

        public string BccsSerialized
        {
            get => JsonConvert.SerializeObject(Bccs);
            set => Bccs = JsonConvert.DeserializeObject<List<UserInfo>>(value);
        }

        [NotMapped]
        public IList<UserInfo> ReplyTos { get; set; }

        public string ReplyTosSerialized
        {
            get => JsonConvert.SerializeObject(ReplyTos);
            set => ReplyTos = JsonConvert.DeserializeObject<List<UserInfo>>(value);
        }

        public string Subject { get; set; }

        public string EmailBody { get; set; }

        public bool IsEmailBodyHtml { get; set; }

        [NotMapped]
        public IDictionary<string, string> Headers { get; set; }

        public string HeadersSerialized => JsonConvert.SerializeObject(Headers);

        [NotMapped]
        public IList<Attachment> Attachments => string.IsNullOrEmpty(_attachmentSerialized)
            ? new List<Attachment>()
            : JsonConvert.DeserializeObject<List<Attachment>>(_attachmentSerialized);

        [NotMapped]
        public EmailTemplate OriginalEmailTemplate { get; set; }

        public string AttachmentsSerialized
        {
            get => JsonConvert.SerializeObject(Attachments);
            set => _attachmentSerialized = value;
        }

        public virtual EmailAccount EmailAccount { get; set; }

        public int EmailAccountId { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime SendingDate { get; set; }

        public bool IsSent { get; set; }

        public void AddAttachment(string attachmentPath, string attachmentName = null)
        {
            var attachment = new Attachment(attachmentPath);
            attachment.ContentDisposition.CreationDate = File.GetCreationTime(attachmentPath);
            attachment.ContentDisposition.ModificationDate = File.GetLastWriteTime(attachmentPath);
            attachment.ContentDisposition.ReadDate = File.GetLastAccessTime(attachmentPath);
            if (!string.IsNullOrEmpty(attachmentName))
                attachment.Name = attachmentName;
            Attachments.Add(attachment);
        }

        public void AddAttachment(byte[] attachmentBytes, string attachmentName)
        {
            var ms = new MemoryStream(attachmentBytes);
            var attachment = new Attachment(ms, attachmentName);
            attachment.ContentDisposition.CreationDate = DateTime.UtcNow;
            attachment.ContentDisposition.ModificationDate = DateTime.UtcNow;
            attachment.ContentDisposition.ReadDate = DateTime.UtcNow;
            Attachments.Add(attachment);
        }

        /// <summary>
        ///     Specifies a user in email communication
        /// </summary>
        [NotMapped]
        public class UserInfo
        {
            public UserInfo(string name, string email)
            {
                Name = name;
                Email = email;
            }

            public string Name { get; set; }

            public string Email { get; set; }
        }
    }
}