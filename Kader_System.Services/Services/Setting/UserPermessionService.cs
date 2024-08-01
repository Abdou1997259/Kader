using Kader_System.DataAccesss.DbContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Kader_System.Services.Services.Setting
{
    public class UserPermessionService(KaderDbContext _context, IStringLocalizer<SharedResource> sharLocalizer, IMapper mapper, IPermessionStructureService permession) : IUserPermessionService
    {
        public async Task<Response<DTOUserPermessionsForUser>> GetAllUserPermession(string userId, string lang)
        {
            var permStruct = (await permession.GetAllPermessionStructureForUser(lang)).DataList;
            var userPermessions = await _context.UserPermissions
                                                .Where(x => x.UserId == userId)
                                                .FirstOrDefaultAsync();

            var userPermessionIds = userPermessions.Permission
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
                        permissionsDict[actionName] = userPermessionIds.Contains(actionId);
                    }
                }
                perm.permissions = permissionsDict;
            }

            return new Response<DTOUserPermessionsForUser>()
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
