namespace Kader_System.Api.Helpers
{
    [AttributeUsage(AttributeTargets.Method)]
    public class PermissionAttribute : Attribute
    {
        public int SubScreenId { get; set; }
        public Permission Permission { get; set; }
        public PermissionAttribute(Permission permission, int screenId)
        {
            this.Permission = permission;
            this.SubScreenId = screenId;

        }

    }
}
