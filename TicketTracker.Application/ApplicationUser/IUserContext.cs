namespace TicketTracker.Application.ApplicationUser
{
    public interface IUserContext
    {
        CurrentUser GetCurrentUser();
        Task<IEnumerable<int>> GetUserTeamsIds(string userId);
    }
}