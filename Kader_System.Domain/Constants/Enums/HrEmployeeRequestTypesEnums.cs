﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kader_System.Domain.Constants.Enums
{
    public enum HrEmployeeRequestTypesEnums
    {
        #region Permessions
        LeavePermission = 1,
        DelayPermission = 2,
        #endregion

        #region Requests
        VacationRequest = 3,
        SalaryIncreaseRequest = 4,
        AllowanceRequest = 5,
        ResignationRequest = 6,
        TerminateContract = 7,
        LoanRequest = 8 
        #endregion
    }
}