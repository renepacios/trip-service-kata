using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripServiceKata.Tests
{
    public class UserBuilder
    {
        private List<TripServiceKata.User.User> _friends = new List<TripServiceKata.User.User>();
        private readonly List<TripServiceKata.Trip.Trip> _trips;

        public UserBuilder()
        {
            _friends = new List<TripServiceKata.User.User>();
            _trips = new List<TripServiceKata.Trip.Trip>();
        }

        public TripServiceKata.User.User Build()
        {

            var dev = new TripServiceKata.User.User();

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

        public UserBuilder WithFriends(params TripServiceKata.User.User[] friends)
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
