using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Roar.Api.Models;
using Roar.Api.Utility;

namespace Roar.Api.Manager
{
    public class PunchManager
    {
        public void InsertPunch(Punch punch)
        {
            SqlConnection conn = null;
            SqlDataReader rdr = null;

            try
            {
                var connectionString = ConfigurationManager.ConnectionStrings["TimeEntities"].ConnectionString;
                conn = new SqlConnection(connectionString);
                DataTable laborAssociationList = new DataTable();
                laborAssociationList.Columns.Add("JobCategoryId", typeof(Guid));
                laborAssociationList.Columns.Add("JobCategoryItemId", typeof(Guid));
                conn.Open();
                SqlCommand cmd = new SqlCommand("dbo.svc_insertpunch", conn);
                //cmd.Parameters.Add(new SqlParameter("@Punch", punch));
                cmd.Parameters.Add(new SqlParameter("@clientId", punch.ClientId));
                cmd.Parameters.Add(new SqlParameter("@employeeUid", punch.EmployeeUid));
                cmd.Parameters.Add(new SqlParameter("@punchDateTime", (punch.PunchDateTime.Trim(TimeSpan.TicksPerMinute))));
                cmd.Parameters.Add(new SqlParameter("@sourceType", punch.PunchSourceTypeId));
                cmd.Parameters.Add(new SqlParameter("@activityType", punch.PunchActivityTypeId));
                cmd.Parameters.Add(new SqlParameter("@statusType", punch.PunchStatusTypeId));
                cmd.Parameters.Add(new SqlParameter("@departmentUid", punch.DepartmentUid == null ? (object)DBNull.Value : punch.DepartmentUid));
                cmd.Parameters.Add(new SqlParameter("@canBeProcessed", punch.CanBeProcessed));
                cmd.Parameters.Add(new SqlParameter("@isActive", punch.IsActive));
                cmd.Parameters.Add(new SqlParameter("@isOverrideDisplayDate", punch.IsOverrideDisplayDate));
                cmd.Parameters.Add(new SqlParameter("@isRound", punch.IsRound));
                cmd.Parameters.Add(new SqlParameter("@isTransfer", punch.IsTransfer));
                cmd.Parameters.Add(new SqlParameter("@noteText", (punch.Note != null && !string.IsNullOrWhiteSpace(punch.Note.NoteText)) ? punch.Note.NoteText : (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@noteType", (punch.Note != null && default(int) != punch.Note.NoteTypeId) ? punch.Note.NoteTypeId : (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@createdBy", punch.UserKey.ToString()));
                cmd.Parameters.Add(new SqlParameter("@application", "Time"));
                cmd.Parameters.Add(new SqlParameter("@ipAddress", punch.IPAddress == null ? (object)DBNull.Value : punch.IPAddress));
                cmd.Parameters.Add(new SqlParameter("@latitude", punch.Latitude == null ? (object)DBNull.Value : punch.Latitude));
                cmd.Parameters.Add(new SqlParameter("@longitude", punch.Longitude == null ? (object)DBNull.Value : punch.Longitude));
                cmd.Parameters.Add(new SqlParameter("@accuracy", punch.Accuracy == null ? (object)DBNull.Value : punch.Accuracy));
                cmd.Parameters.Add(new SqlParameter("@enqueuedTimeUtc", punch.EnqueuedTimeUtc.HasValue ? punch.EnqueuedTimeUtc.Value : (object)DBNull.Value));
                cmd.Parameters.Add(new SqlParameter("@isSystemUpdated", punch.IsSystemUpdated));
                cmd.Parameters.Add(new SqlParameter { ParameterName = "@categoryAssociations", SqlDbType = SqlDbType.Structured, Value = laborAssociationList, TypeName = "dbo.JobAssociationArray" });
                cmd.CommandType = CommandType.StoredProcedure;
                rdr = cmd.ExecuteReader();
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
                if (rdr != null)
                {
                    rdr.Close();
                }
            }
        }
    }
}