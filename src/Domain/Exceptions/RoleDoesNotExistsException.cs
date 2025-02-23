namespace Domain.Exceptions;

public class RoleDoesNotExistsException : Exception
{
    public RoleDoesNotExistsException(string roleName) : base($"Role with name {roleName} does not exists.")
    {
    }

    public RoleDoesNotExistsException() : base("Role does not exists.")
    {
    }
}