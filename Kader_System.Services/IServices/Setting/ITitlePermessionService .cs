﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kader_System.Services.IServices.Setting
{
    public interface ITitlePermessionService
    {
        public Task<Response<DTOUserPermessions>> GetAllTitlePermession(int titleId,string lang);
    }
}