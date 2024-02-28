using FluentAssertions;

using TripServiceKata.Trip;
namespace TripServiceKata.Tests.Trip
{
    using Exception;
    using Moq;
    using TripServiceKata.User;
    using Trip = TripServiceKata.Trip.Trip;


    public class TripServiceTest
    {
        private TripService _tripService;
        private readonly TripServiceToTest _tripSeriveToTest;

        public static User LOGGED_USER;
        public static User GUEST_USER = new User();
        public static User ANOTHER_USER = new User();
        public static User FRIEND_USER = new User();
        public static Trip TO_BRAZIL = new Trip();
        public static Trip TO_LONDON = new Trip();
        private static Mock<IUserSession> _userSessionMock;
        private static IUserSession _userSession;
        private static Mock<ITripDAO> _tripDaoMock;
        private static ITripDAO _tripDao;

        public TripServiceTest()
        {

            _tripSeriveToTest = new TripServiceToTest();

            _userSessionMock = new Mock<IUserSession>();

            _userSession = _userSessionMock.Object;

            _tripDaoMock = new Mock<ITripDAO>();
         _tripDao = _tripDaoMock.Object;

            _tripService = new TripService(_userSession, _tripDao);
        }

        [Fact]
        public void Should_throwException_WhenUser_IsNot_LoggedIn()
        {
            var tripService = _tripService; //ToTest;

            var user = new User();
            LOGGED_USER = null;
            _userSessionMock
                .Setup(x => x.GetLoggedUser())
                .Returns(LOGGED_USER);


            Action act = () => tripService.GetTripsByUser(user);

            act.Should().Throw<UserNotLoggedInException>();
        }


        [Fact]
        public void Should_returnEmptyList_WhenUser_IsNot_Friend()
        {
            var tripService = _tripService;

            var usera=new UserBuilder()
                .WithFriends(FRIEND_USER)
                .WithTrips(TO_BRAZIL,TO_LONDON)
                .Build();

            var user = GUEST_USER;
            user.AddTrip(TO_BRAZIL);
            user.AddTrip(TO_LONDON);

            LOGGED_USER = new User();
            _userSessionMock
                .Setup(x => x.GetLoggedUser())
                .Returns(LOGGED_USER);

            user.AddFriend(ANOTHER_USER);

            var trips = tripService.GetTripsByUser(user);

            trips.Should().BeEmpty();
        }


        [Fact]
        public void Should_returnTrips_WhenUser_Is_Friend()
        {
            var tripService = _tripService;

            var user = GUEST_USER;
            user.AddTrip(TO_BRAZIL);
            user.AddTrip(TO_LONDON);
           

            LOGGED_USER = new User(){
                nombre="pepe"
                    };

            user.AddFriend(LOGGED_USER);

            _userSessionMock
                .Setup(x => x.GetLoggedUser())
                .Returns(LOGGED_USER);

            _tripDaoMock
                .Setup(s => s.GetTripsBy(It.IsAny<User>()))
                .Returns(user.Trips());


            var trips = tripService.GetTripsByUser(user);

            trips.Should().NotBeEmpty();
        }


        [Fact]
        public void AddUserFriend_works_as_expected()
        {
            var user = new User();
            var friend = new User();
            user.AddFriend(friend);
            user.GetFriends().Should().Contain(friend);
        }

        [Fact]
        public void IsFriendOf_works_as_expected()
        {
            var user = new User();
            var friend = new User();
            user.AddFriend(friend);
            user.IsFriendOf(friend)
                .Should()
                .BeTrue();
        }

        [Fact]
        public void IsFrindOf_with_another_user_works_as_expected()
        {

            var user = GUEST_USER;
            var friend = FRIEND_USER;
            user.AddFriend(friend);

            user.IsFriendOf(ANOTHER_USER)
                .Should()
                .BeFalse();
        }
    }




    public class TripServiceToTest : TripService
    {
        protected override User GetLoggedUser()
        {
            return TripServiceTest.LOGGED_USER;
        }

        protected override List<Trip> FindTripsByUser(User user)
        {
            return user.Trips();
        }
    }
}
