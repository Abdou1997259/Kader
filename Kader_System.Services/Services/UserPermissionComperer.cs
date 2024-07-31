using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kader_System.Services.Services
{
    public class UserPermissionComperer:IEqualityComparer<UserPermission>
    {
        public bool Equals(UserPermission x, UserPermission y)
        {
            // Check if both objects are the same instance
            if (ReferenceEquals(x, y))
                return true;

            // Check if one object is null while the other is not
            if (x == null || y == null)
                return false;

            // Compare properties to determine equality
            return  
                   x.UserId == y.UserId &&
                   x.SubScreenId == y.SubScreenId &&
                   x.Permission == y.Permission &&
                   x.TitleId == y.TitleId;
        }

        public int GetHashCode(UserPermission obj)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));

            // Combine the hash codes of the properties
            int hashId=  obj.UserId?.GetHashCode() ?? 0;
         
            int hashSubScreenId = obj.SubScreenId.GetHashCode();
            int hashPermission = obj.Permission?.GetHashCode() ?? 0;
            int hashTitleId = obj.TitleId.GetHashCode();

            // Combine all hash codes into a single hash code
            return hashId  ^ hashSubScreenId ^ hashPermission ^ hashTitleId;
        }
    }
}
