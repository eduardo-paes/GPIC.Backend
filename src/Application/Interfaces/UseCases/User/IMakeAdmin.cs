namespace Application.Interfaces.UseCases.User
{
    public interface IMakeAdmin
    {
        Task<string> ExecuteAsync(Guid? userId);
    }
}