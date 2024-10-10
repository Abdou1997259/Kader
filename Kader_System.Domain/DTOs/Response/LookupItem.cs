public interface ISelectListItem
{
    int Id { get; set; }
    string Name { get; set; }
}

public class SelectListItemLookup : ISelectListItem
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public class SelectListItemLookupImage : ISelectListItem
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string ImagePath { get; set; }
}