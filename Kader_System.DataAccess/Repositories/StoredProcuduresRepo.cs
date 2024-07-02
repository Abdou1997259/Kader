namespace Kader_System.DataAccess.Repositories
{
    public class StoredProcuduresRepo(KaderDbContext db) : IStoredProcuduresRepo
    {
        private readonly KaderDbContext _db = db;
        public async Task<IEnumerable<SpCacluateSalary>> SpCacluateSalaries(DateOnly startCalculationDate, int EmpId)
        {
            var endCalculationDate = startCalculationDate.AddMonths(1).AddDays(-1);

            var result = await _db.SpCacluateSalariesModel.FromSql($"exec Sp_Cacluate_Salary {startCalculationDate},{endCalculationDate},{EmpId}").ToListAsync();



            return result;

        }
    }
}
