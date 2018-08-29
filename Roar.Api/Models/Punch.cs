using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Roar.Api.Models
{
    public class Punch
    {
        public Nullable<Guid> PunchId { get; set; }

        public long EmployeeUid { get; set; }

        public System.DateTime PunchDateTime { get; set; }

        public string DisplayPunchDateTime { get; set; }

        public Note Note { get; set; }

        public Nullable<Guid> NoteId { get; set; }

        public byte PunchSourceTypeId { get; set; }

        public byte PunchActivityTypeId { get; set; }

        public byte PunchStatusTypeId { get; set; }

        public Nullable<long> DepartmentUid { get; set; }

        public Guid? ReasonCategoryId { get; set; }

        public Guid TimeCardExceptionId { get; set; }

        public Guid TimeCardExceptionPolicyId { get; set; }

        public DateTime? PayPeriodStartDate { get; set; }

        public DateTime? PayPeriodEndDate { get; set; }

        public bool CanBeProcessed { get; set; }
        public bool IsActive { get; set; }
        public bool IsOverrideDisplayDate { get; set; }
        public bool IsRound { get; set; }


        public bool IsTransfer { get; set; }

        public Guid? UserKey { get; set; }


        public int ClientId { get; set; }

        public bool IsPersistedToDatabase { get; set; }

        public PunchStatusType PunchStatusType { get; set; }

        public bool IsEdited { get; set; }

        public bool IsScheduleRounded { get; set; }

        public PunchStatus PunchStatus
        {
            get
            {
                return (PunchStatus)PunchStatusTypeId;
            }
        }

        public string IPAddress { get; set; }

        public decimal? Latitude { get; set; }

        public decimal? Longitude { get; set; }

        public decimal? Accuracy { get; set; }

        public System.DateTime? ProcessedDateTime { get; set; }

        public string EmployeeBadge { get; set; }

        public DateTime? EnqueuedTimeUtc { get; set; }

        public string UnassignedReason { get; set; }

        public bool? IsSystemUpdated { get; set; }

        public List<LaborPunchAssociation> LaborAssociations { get; set; }
    }
    public class PunchStatusType 
    {
        public bool TransferDefault { get; set; }
    }
    public class Note
    {
        public Nullable<Guid> NoteId { get; set; }
        public int NoteTypeId { get; set; }

        public string NoteText { get; set; }
    }

    public enum PunchStatus
    {
        None,
        Auto,
        In,
        Out,
    }
    public class LaborPunchAssociation
    {
        public Guid Id { get; set; }

        public Nullable<Guid> PunchId { get; set; }

        public Guid LaborCategoryId { get; set; }

        public Guid LaborCategoryItemId { get; set; }

        public int ClientId { get; set; }

        public long EmployeeUid { get; set; }
    }
}