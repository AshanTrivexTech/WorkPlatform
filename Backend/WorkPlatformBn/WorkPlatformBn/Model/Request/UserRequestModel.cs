namespace WorkPlatformBn.Model.Request;

public class UserRequestModel
{
    public string Name { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public int UserType { get; set; }

    public int WorkerType { get; set; }

    public bool IsCompanyUser { get; set; }

    public List<Category> Categories { get; set; }
}
