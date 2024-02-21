namespace TripServiceKata.User;

public interface IUserSession
{
    bool IsUserLoggedIn(User user);
    User GetLoggedUser();
}