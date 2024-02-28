using System.Collections.Generic;
using TripServiceKata.Exception;

namespace TripServiceKata.Trip
{
    using User;

    public interface ITripDAO
    {
        public List<Trip> GetTripsBy(User user);

    }

    public class TripDAO : ITripDAO
    {
        public static List<Trip> FindTripsByUser(User user)
        {
            throw new DependendClassCallDuringUnitTestException(
                        "TripDAO should not be invoked on an unit test.");
        }

        public List<Trip> GetTripsBy(User user)
        {
            return FindTripsByUser(user);
        }
    }
}
