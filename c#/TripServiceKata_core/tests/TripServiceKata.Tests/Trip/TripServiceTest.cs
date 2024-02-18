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
    }


    public class TestTripService : TripService
    {
        public static User LOGGED_USER = null;

        public override User GetLoggedUser()=> LOGGED_USER;
    }
}
