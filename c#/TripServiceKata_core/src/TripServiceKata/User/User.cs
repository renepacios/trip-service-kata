namespace TripServiceKata.User
{
    public class User
    {
        private List<Trip.Trip> trips = new List<Trip.Trip>();
        private List<User> friends = new List<User>();
        public string nombre { get; set; }

        public List<User> GetFriends()
        {
            return friends;
        }

        public void AddFriend(User user)
        {
            friends.Add(user);
        }

        public void AddTrip(Trip.Trip trip)
        {
            trips.Add(trip);
        }

        public List<Trip.Trip> Trips()
        {
            return trips;
        }

        public bool IsFriendOf(User friend)
        {
            //if (friends != null && friends.Count == 1)
            //    return friends[0].Equals(friend);

            foreach (var user in friends)
            {
                if (user.Equals(friend))
                    return true;
            }
            return false;


            //return friends.Contains(friend);
        }
    }
}
