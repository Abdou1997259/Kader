namespace Kader_System.Domain.Dtos.Response;

public class SelectListResponse 
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}
public class SelectListForUserResponse
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
}
public class SelectListInEnglishResponse
{
    public int Id { get; set; }
    public string NameInEnglish { get; set; } = string.Empty;
}
public class SelectListMoreResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}
public class SpecificSelectListResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string NameInEnglish { get; set; } = string.Empty;   
}
public class SelectListForNewsResponse
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
}
public class ListOfEmployeeRequestTypesResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;            
    public string NameCheck { get; set; } = string.Empty;
    public string? LinkPath { get; set; }
    public bool IsActive { get; set; } 
}

public class SelectListLookupResponse
{
    public int Id { get; set; }
    public required string Name_ar { get; set; }
    public required string Name_en { get; set; }
}
public class CheckBox
{
    public string DisplayValue { get; set; } = string.Empty;
    public bool IsSelected { get; set; }
}
public class SelectListForUserRequest : SelectListForUserResponse
{
}

