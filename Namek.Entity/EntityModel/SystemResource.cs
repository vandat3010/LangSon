using System.ComponentModel;

namespace Namek.Entity.EntityModel
{
    public class SystemResource
    {
        [DisplayName("Id")]
        public int Id { get; set; } // Id (Primary key)
        [DisplayName("Region")]
        public int? Region { get; set; } // Region
        [DisplayName("Service Type")]
        public int? ServiceType { get; set; } // ServiceType
        [DisplayName("Server Name")]
        public string ServerName { get; set; } // ServerName (length: 250)
        [DisplayName("Public IP")]
        public string PublicIp { get; set; } // PublicIP (length: 15)
        [DisplayName("Local IP")]
        public string LocalIp { get; set; } // LocalIP (length: 15)
        [DisplayName("Control Panel")]
        public string ControlPanel { get; set; } // ControlPanel (length: 250)
        [DisplayName("API Address")]
        public string ApiAddress { get; set; } // APIAddress (length: 250)
        [DisplayName("API Username")]
        public string ApiUserName { get; set; } // APIUserName (length: 50)
        [DisplayName("API Password")]
        public string ApiPassword { get; set; } // APIPassword (length: 50)
        [DisplayName("Server Type")]
        public int? ServerType { get; set; } // ServerType
        [DisplayName("Os")]
        public string Os { get; set; } // Os (length: 50)
        [DisplayName("Maximum Account")]
        public int? MaximumAccount { get; set; } // MaximumAccount
        [DisplayName("HDD Capacity")]
        public double? HddCapacity { get; set; } // HDDCapacity
        [DisplayName("Priority")]
        public int? Priority { get; set; } // Priority
        [DisplayName("Description")]
        public string Description { get; set; } // Description (length: 1000)
        [DisplayName("Active")]
        public bool IsActive { get; set; } // IsActive
        [DisplayName("Current Account")]
        public int? CurrentAccount { get; set; } // CurrentAccount
    }
}