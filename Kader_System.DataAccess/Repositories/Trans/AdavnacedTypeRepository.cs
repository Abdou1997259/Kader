


namespace Kader_System.DataAccess.Repositories.Trans
{
    public class AdavnacedTypeRepository : IAdvancedTypesRepository
    {
        private readonly KaderDbContext _context;
        public AdavnacedTypeRepository(KaderDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AdvancedType>> GetAllAdvancedTypes()
        {
            return await _context.AdvancedTypes.ToListAsync();
        }
    }
}
