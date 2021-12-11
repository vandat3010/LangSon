using Namek.Entity.EntityNewModel.Mapping;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Namek.Entity.EntityModel
{
    [Table("Reporter")]
    public class Reporter : BaseEntity
    {
        public string FirstName { get; set; }
        public string MidleName { get; set; }
        public string LastName { get; set; }

        public string FullName { get; set; }
        public string UnsignedName { get; set; }
        public string Address { get; set; }
        public int? PreAgency_Id { get; set; }
        public int? CurrentAgency_Id { get; set; }
        public string CardNumber { get; set; }
        public short Type { get; set; }
        public DateTime? StartedDate { get; set; }
        public DateTime? EndDate { get; set; }

        public string ListNewspapers { get; set; }
        public byte Gender { get; set; }

        public bool? IsDelete { get; set; }
        public bool? IsLocked { get; set; }

        public bool? IsDistrictManager { get; set; }
        public DateTime? LockedToDateTime { get; set; }
        public DateTime? LockedDateTimeLast { get; set; }

        public DateTime? CreateDate { get; set; }
        public DateTime? ModifyDate { get; set; }
        public int? WorkContent_Id { get; set; }
        public string OtherWork { get; set; }

        public DateTime? Birthday { get; set; }


    }

}
