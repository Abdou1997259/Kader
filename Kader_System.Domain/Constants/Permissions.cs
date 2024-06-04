namespace Kader_System.Domain.Constants;

public static class Permissions
{
    public static List<GetPermissionsWithActions> GeneratePermissionsList(string module)
    {
        if (module == "Setting")
            return
            [
                new()
                {
                    ClaimValue = $"superAdminRole.{module}.View",
                    ActionId = (int)ActionsEnums.View
                }
            ];

        else
            return
            [
                new()
                {
                    ClaimValue = $"Permissions.{module}.View",
                    ActionId = (int)ActionsEnums.View
                },
                new()
                {
                    ClaimValue = $"Permissions.{module}.Print",
                    ActionId = (int)ActionsEnums.Print
                },
                new()
                {
                    ClaimValue = $"Permissions.{module}.Edit",
                    ActionId = (int)ActionsEnums.Edit
                },
                new()
                {
                    ClaimValue = $"Permissions.{module}.Delete",
                    ActionId = (int)ActionsEnums.Delete
                },
                new()
                {
                    ClaimValue = $"Permissions.{module}.ForceDelete",
                    ActionId = (int)ActionsEnums.ForceDelete
                }
            ];
    }

    public static List<GetPermissionsWithActions> GenerateAllPermissions()
    {
        List<GetPermissionsWithActions> allPermissions = [];

        var modules = Enum.GetValues(typeof(PermissionsModulesEnums));

        foreach (var module in modules)
            allPermissions.AddRange(GeneratePermissionsList(module.ToString()!));

        return allPermissions;
    }

    public static class Setting
    {
        public const string View = "Permissions.Setting.View";
    }

    public static class MainScreenCat
    {
        public const string View = "Permissions.MainScreenCat.View";
        public const string Print = "Permissions.MainScreenCat.Print";
        public const string Create = "Permissions.MainScreenCat.Create";
        public const string Edit = "Permissions.MainScreenCat.Edit";
        public const string Delete = "Permissions.MainScreenCat.Delete";
        public const string ForceDelete = "Permissions.MainScreenCat.ForceDelete";
    }
    public static class HR
    {
        public const string View = "Permissions.HR.View";
        public const string Print = "Permissions.HR.Print";
        public const string Create = "Permissions.HR.Create";
        public const string Edit = "Permissions.HR.Edit";
        public const string Delete = "Permissions.HR.Delete";
        public const string ForceDelete = "Permissions.HR.ForceDelete";
    }
    public static class Transaction
    {
        public const string View = "Permissions.Transaction.View";
        public const string Print = "Permissions.Transaction.Print";
        public const string Create = "Permissions.Transaction.Create";
        public const string Edit = "Permissions.Transaction.Edit";
        public const string Delete = "Permissions.Transaction.Delete";
        public const string ForceDelete = "Permissions.Transaction.ForceDelete";
    }
}