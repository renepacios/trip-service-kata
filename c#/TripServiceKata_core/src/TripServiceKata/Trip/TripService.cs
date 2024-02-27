using TripServiceKata.Exception;
using TripServiceKata.User;

namespace TripServiceKata.Trip
{



    public class TripService
    {
        IUserSession _userSession;
        private readonly ITripDAO _tripDao;

        public TripService() 
            : this(UserSession.GetInstance(), new TripDAO())
        {
            //_userSession = UserSession.GetInstance();
            //_tripDao = new TripDAO();
        }

        public TripService(IUserSession userSession, ITripDAO tripDao)
        {
            _ = userSession ?? throw new NullReferenceException(nameof(userSession));
            _ = tripDao ?? throw new NullReferenceException(nameof(tripDao));
            _userSession = userSession;
            _tripDao = tripDao;
        }

        public List<Trip> GetTripsByUser(User.User user)
        {
            User.User loggedUser = GetLoggedUser();
            _ = loggedUser ?? throw new UserNotLoggedInException();

            var isFriend = user.IsFriendOf(loggedUser);

            return isFriend 
                ? FindTripsByUser(user) 
                : new List<Trip>();

     
        }

        protected virtual List<Trip> FindTripsByUser(User.User user)
        {
            return _tripDao.GetTripsBy(user);
            //return TripDAO.FindTripsByUser(user);
        }

        protected virtual User.User GetLoggedUser()
        {
            return _userSession.GetLoggedUser();
        }
    }
}
