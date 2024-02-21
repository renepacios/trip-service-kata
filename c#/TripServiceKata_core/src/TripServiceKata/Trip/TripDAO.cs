using TripServiceKata.Exception;

namespace TripServiceKata.Trip
{
    public interface ITripDAO
    {
        List<Trip> GetTripByUser(User.User user);
    }

    public class TripDAO : ITripDAO
    {
        public static List<Trip> FindTripsByUser(User.User user)
        {
            throw new DependendClassCallDuringUnitTestException(
                        "TripDAO should not be invoked on an unit test.");
        }

        public List<Trip> GetTripByUser(User.User user)
        {
            return FindTripsByUser(user);
        }
    }
}
