﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kader_System.Domain.DTOs.Request.EmployeesRequests.PermessionRequests
{
    public class DTOCreateLeavePermissionRequest
    {

        public int EmployeeId { get; set; }
        public TimeOnly LeaveTime { get; set; }
        public TimeOnly? BackTime { get; set; }
        public string? Notes { get; set; }

        [AllowedLetters(FileSettings.SpecialChar), MaxFileLettersCount(FileSettings.Length), FileExtensionValidation(FileSettings.AllowedExtension)]
        public IFormFile? Attachment { get; set; }
    }
}
