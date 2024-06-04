namespace Kader_System.Domain.Dtos.Response;

public class GetEachSubMainWithActions
{
    public int SubMainId { get; set; }
    public List<ActionsWithClaimValue> Actions { get; set; } = [];
}

public class ActionsWithClaimValue
{
    public int ActionId { get; set; }
    public string ActionName { get; set; } = string.Empty;
    public string ClaimValue { get; set; } = string.Empty;
    public bool IsSelected { get; set; }
}
