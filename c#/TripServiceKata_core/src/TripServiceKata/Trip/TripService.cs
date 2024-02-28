using TripServiceKata.Exception;
using TripServiceKata.User;

namespace TripServiceKata.Trip
{


    public class TripService
    {
        IUserSession _userSession;
        ITripDAO _tripDAO;

        public TripService() 
        : this(UserSession.GetInstance(), new TripDAO())
        {
            
        }

        public TripService(IUserSession userSession, ITripDAO tripDao)
        {
            _userSession = userSession;
            _tripDAO = tripDao;
        }



        public List<Trip> GetTripsByUser(User.User user)
        {
            _ = user ?? throw new ArgumentNullException(nameof(user));

            User.User loggedUser = GetLoggedUser();

            if (loggedUser == null) throw new UserNotLoggedInException();

            var isFriend = user.IsFriendOf(loggedUser);

            return isFriend
                ? FindTripsByUser(user)
                : new List<Trip>();


        }

        protected virtual User.User GetLoggedUser()
        {
            return _userSession.GetLoggedUser();
        }

        protected virtual List<Trip> FindTripsByUser(User.User user)
        {
            //return TripDAO.FindTripsByUser(user);
            return _tripDAO.GetTripsBy(user);
        }
    }
}
