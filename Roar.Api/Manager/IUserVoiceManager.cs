using Roar.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roar.Api.Manager
{
    public interface IUserVoiceManager
    {
        string SaveVoiceData(byte[] voicedata, string fileName);
        string SaveUserVoiceData(EmployeeEnrollment employeeEnrollment);
    }
}
