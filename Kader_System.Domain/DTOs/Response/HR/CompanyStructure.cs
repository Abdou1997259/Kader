﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kader_System.Domain.DTOs.Response.HR
{

    public class CompanyResponse
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }

        public string? MangerName { get; set; }
        public int Level { get; set; } = 1;
        public List<ManagementResponse> Children { get; set; }
    }

    public class ManagementResponse
    {
        public int ManagementId { get; set; }
     
        public int ManagerId {  get; set; }
        public string NameEn { get;set; }
        public string NameAr { get; set; }
        public string ManagementName { get;set; }

        public string ManagerName { get; set; } 
        public int Level { get; set; } = 2;
        public List<DepartmentResponse> Children { get; set; }
    }

    public class DepartmentResponse
    {
        public int DepartmentId { get; set; }
        public int Level { get; set; } = 3;
        public string DepartmentName { get; set;}
        public int ManagementId { get;set; }
        public string ManagerName { get; set;}
        public int ManagerId { get; set; }
        public string NameEn { get; set; }
        public string NameAr{   get; set;   }
        public List<EmployeeResponse> Children { get; set; }
    }

    public class EmployeeResponse
    {
        public int EmployeeId { get; set; }
        public int Level { get; set; } = 4;
        public string NameEn { get; set; }
        public string NameAr { get; set;  }
        public string EmployeeName { get; set; }

        public string JobName { get; set; } 
    }

}
