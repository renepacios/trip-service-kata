using System.Collections.Generic;
using TripServiceKata.Exception;
using TripServiceKata.User;

namespace TripServiceKata.Trip
{
    public class TripService
    {
        private IUserSession _userSession;
        private ITripDAO _tripDao;

        public TripService() 
            : this(UserSession.GetInstance(), new TripDAO())
        {
            //_userSession = UserSession.GetInstance();
            //_tripDao = new TripDAO();

        }

        public TripService(IUserSession userSession,
            ITripDAO tripDao)
        {
            _userSession = userSession ?? throw new NullReferenceException(nameof(userSession));
            _tripDao = tripDao ?? throw new NullReferenceException(nameof(tripDao));
        }


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
            return _userSession.GetLoggedUser();
        }

        public virtual List<Trip> GetTripByUser(User.User user)
        {
          //  return TripDAO.FindTripsByUser(user);
            return _tripDao.GetTripByUser(user);
        }
    }
}
