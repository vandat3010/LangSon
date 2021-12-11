using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Namek.Entity.EntityModel
{
    public class SendMailRequestModel
    {
        public string from { get; set; }
        public string to { get; set; }
        public string subject { get; set; }
        public string body { get; set; }
        public bool isBodyHtml { get; set; }
        public List<FileUploadContact> lstFileUpload { get; set; }
    }

    public class FileUploadContact
    {
        public string DirectoryPath { get; set; }
        public string FileName { get; set; }
        public byte[] FileBytes { get; set; }
        public string Extention { get; set; }

    }

    public class SendMailMsg
    {
        public int Status { get; set; }
        public string Msg { get; set; }
    }
}
