namespace TripServiceKata.Tests.Trip
{
    using Exception;
    using FluentAssertions;
    using TripServiceKata.Trip;
    using User;

    public class TripServiceTest
    {
        public static User LOGGED_USER = null;
        public static User FRIEND_USER = new User();
        public static User ANOTHER_USER = new User();

        public static Trip GALICIA = new Trip();
        public static Trip BARCELONA = new Trip();


        [Fact]
        public void not_allow_get_trips_WhenUserIsNotLoggedIn()
        {
            var tripService = new TestTripService();
            var user = new User();
            tripService
                .Invoking(s => s.GetTripsByUser(user))
                .Should()
                .Throw<UserNotLoggedInException>();

        }

        [Fact]
        public void allow_get_trips_When_LoggedUser_Is_Friend()
        {
            var tripService = new TestTripService();
            var user = new User();
            LOGGED_USER = new User();
            user.AddFriend(FRIEND_USER);
            user.AddFriend(LOGGED_USER);
            user.AddTrip(GALICIA);
            user.AddTrip(BARCELONA);


            var trips = tripService.GetTripsByUser(user);
            trips.Should().NotBeEmpty();
        }

        [Fact]
        public void not_allow_get_trips_when_user_is_not_friend()
        {
            var tripService = new TestTripService();
            var user = new User();
            var friend = new User();
            user.AddFriend(friend);

            var trips = tripService.GetTripsByUser(user);
            trips.Should().BeEmpty();
        }

        [Fact]
        public void ShouldReturnTripsWhenLoggedUserIsAFriend()
        {
            var tripService = new TestTripService();
            var user = new User();
            var friend = new User();
            user.AddFriend(friend);
            LOGGED_USER = friend;

            var trips = tripService.GetTripsByUser(user);
            trips.Should().NotBeEmpty();
        }

        [Fact]
        public void AddFriend_should_be_work_as_expected()
        {
            var user = new User();
            var friend = new User();
            user.AddFriend(friend);

            user.IsFriend(friend).Should().BeTrue();

            //user.GetFriends().Should().Contain(friend);
        }
    }

    public class UserTest
    {

    }

    public class TestTripService : TripService
    {
        public override User GetLoggedUser() => TripServiceTest.LOGGED_USER;

        public override List<Trip> GetTripByUser(User user)
        {
            return new List<Trip> { new Trip() };
        }
    }
}
