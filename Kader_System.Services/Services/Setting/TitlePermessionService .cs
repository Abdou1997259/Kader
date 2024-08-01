using Kader_System.DataAccesss.DbContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Kader_System.Services.Services.Setting
{
    public class TitlePermessionService(KaderDbContext _context, IPermessionStructureService permession) : ITitlePermessionService
    {
        public async Task<Response<DTOUserPermessions>> GetAllTitlePermession(int titleId, string lang)
        {
            var permStruct = (await permession.GetAllPermessionStructure(lang)).DataList;
            var titlePermessions = await _context.TitlePermissions
                                                .Where(x => x.TitleId == titleId)
                                                .FirstOrDefaultAsync();

            var titlePermessionsIds = titlePermessions.Permissions
                                                   .Split(',')
                                                   .Select(int.Parse)
                                                   .ToArray();

            var actionNames = await GetActionNamesAsync(lang);

            foreach (var perm in permStruct)
            {
                var permissionsDict = new Dictionary<string, bool>();

                foreach (var actionId in perm.actions)
                {
                    if (actionNames.TryGetValue(actionId, out var actionName))
                    {
                        permissionsDict[actionName] = titlePermessionsIds.Contains(actionId);
                    }
                }
                perm.permissions = permissionsDict;
            }

            return new Response<DTOUserPermessions>()
            {
                Check = true,
                DataList = permStruct,
                Msg = ""
            };
        }
        private async Task<Dictionary<int, string>> GetActionNamesAsync(string lang)
        {
            var actions = await _context.Actions
                                        .AsNoTracking()
                                        .ToListAsync();
            return actions.ToDictionary(a => a.Id, a => lang == "ar" ?  a.Name :a.NameInEnglish);
        }
    }
}
