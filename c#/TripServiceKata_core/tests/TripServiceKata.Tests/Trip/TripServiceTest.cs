namespace TripServiceKata.Tests.Trip
{
    using Exception;
    using FluentAssertions;
    using TripServiceKata.Trip;
    using User;

    public class TripServiceTest
    {


        [Fact]
        public void ShouldThrowUserNotLoggedInExceptionWhenUserIsNotLoggedIn()
        {
            var tripService = new TestTripService();
            var user = new User();
            tripService
                .Invoking(s => s.GetTripsByUser(user))
                .Should()
                .Throw<UserNotLoggedInException>();

        }

        [Fact]
        public void ShouldNotReturnTripsWhenLoggedUserIsNotAFriend()
        {
            var tripService = new TestTripService();
            var user = new User();
            var friend = new User();
            friend.AddFriend(user);
            TestTripService.LOGGED_USER = friend;
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
            TestTripService.LOGGED_USER = friend;

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
        public static User LOGGED_USER = null;

        public override User GetLoggedUser() => LOGGED_USER;

        public override List<Trip> GetTripByUser(User user)
        {
            return new List<Trip> { new Trip() };
        }
    }
}
