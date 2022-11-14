namespace Houses.Core.Services.Contracts
{
    public interface IUserService
    {
        Task<bool> ExistsById(string userId);

        Task<string> GetUserId(string userId);
    }
}
