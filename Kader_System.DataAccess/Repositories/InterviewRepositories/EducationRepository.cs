﻿using Kader_System.Domain.Interfaces.EducationRepositories;
using Kader_System.Domain.Models.Interviews;

namespace Kader_System.DataAccess.Repositories.InterviewRepositories
{
    public class EducationRepository(KaderDbContext context) : BaseRepository<Education>(context), IEducationRepository
    {

    }
}