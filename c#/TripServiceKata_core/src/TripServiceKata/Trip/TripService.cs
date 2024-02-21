using System.Collections.Generic;
using TripServiceKata.Exception;
using TripServiceKata.User;

namespace TripServiceKata.Trip
{
    public class TripService
    {
        public List<Trip> GetTripsByUser(User.User user)
        {
            User.User loggedUser = GetLoggedUser() 
                                   ?? throw new UserNotLoggedInException();
            
            //if (loggedUser == null)
            //{
            //    throw new UserNotLoggedInException();
            //}
            
            var isFriend = user.IsFriend(loggedUser);
            
            return (isFriend)
                ? GetTripByUser(user) 
                : new List<Trip>();
            
        }

        public virtual User.User GetLoggedUser()
        {
            return UserSession.GetInstance().GetLoggedUser();
        }

        public virtual List<Trip> GetTripByUser(User.User user)
        {
            return TripDAO.FindTripsByUser(user);
        }
    }
}
