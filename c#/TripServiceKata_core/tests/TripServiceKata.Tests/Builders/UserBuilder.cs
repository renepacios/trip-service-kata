

namespace TripServiceKata.Tests.Builders
{
    using User;

    public class UserBuilder
    {
        private List<User> _friends = new List<User>();
        private readonly List<TripServiceKata.Trip.Trip> _trips;

        public UserBuilder()
        {
            _friends = new List<User>();
            _trips = new List<TripServiceKata.Trip.Trip>();
        }

        public User Build()
        {

            var dev = new User();

            foreach (var friend in _friends)
            {
                dev.AddFriend(friend);
            }

            foreach (var trip in _trips)
            {
                dev.AddTrip(trip);
            }

            return dev;

        }

        public UserBuilder WithFriends(params User[] friends)
        {
            _friends.AddRange(friends);
            return this;
        }

        public UserBuilder WithTrips(params TripServiceKata.Trip.Trip[] trips)
        {
            _trips.AddRange(trips);
            return this;
        }

    }
}
