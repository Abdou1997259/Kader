﻿namespace Kader_System.Services.IServices.Auth
{
    public interface IUserContextService
    {
        string? UserId { get; }
        public bool IsAdmin();
        Task<int> GetLoggedCurrentCompany();
        Task<List<int>> GetLoggedCurrentCompanies();
    }
}
