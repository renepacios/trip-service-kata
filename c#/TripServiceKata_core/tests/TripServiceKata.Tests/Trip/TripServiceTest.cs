namespace TripServiceKata.Tests.Trip
{
    using Exception;
    using FluentAssertions;
    using Moq;
    using TripServiceKata.Trip;
    using User;

    public class TripServiceTest
    {
        private readonly TripService _tripService;
        private readonly TripServiceToTest _tripServiceToTest;

        public static User LOGGED_USER;
        public static User GUEST_USER = new User();
        public static User ANOTHER_USER = new User();
        public static User REGISTERED_USER = new User();
        public static Trip TO_BRAZIL = new Trip();
        public static Trip TO_LONDON = new Trip();
        private static Mock<IUserSession> _userSessionMock;
        private static IUserSession _userSesion;
        private static Mock<ITripDAO> _tripDaoMock;
        private static ITripDAO _tripDao;

        public TripServiceTest()
        {
            //  _tripService = new TripService();
            _tripServiceToTest = new TripServiceToTest();

            //_userSesion =
            //    Mock.Of<IUserSession>(l => l.GetLoggedUser() == LOGGED_USER);


            _userSessionMock = new Mock<IUserSession>();
            _userSessionMock
                .Setup(x => x.GetLoggedUser())
                .Returns(LOGGED_USER);
            _userSesion = _userSessionMock.Object;

            _tripDaoMock = new Mock<ITripDAO>();
            _tripDao= _tripDaoMock.Object;

            _tripService = new TripService(_userSesion,_tripDao);

        }

        [Fact]
        public void No_Allow_Get_Trips_WhenUser_IsNot_LoggedIn()
        {
            var tripService = _tripService;//ToTest;

            LOGGED_USER = null;
          

            var user = GUEST_USER;

            Assert.Throws<UserNotLoggedInException>(() => tripService.GetTripsByUser(user));

            tripService
                .Invoking(x => x.GetTripsByUser(user))
                .Should()
                .Throw<UserNotLoggedInException>();

        }


        [Fact]
        public void Should_Return_Empty_List_WhenUser_IsNot_Friend()
        {
            var tripService = _tripService;

            LOGGED_USER = REGISTERED_USER;
            var user = GUEST_USER;
            user.AddTrip(TO_BRAZIL);
            user.AddTrip(TO_LONDON);


            var tripsByUser = tripService.GetTripsByUser(user);

            tripsByUser
                .Should()
                .NotBeNull()
                .And
                .BeEmpty();
        }

        [Fact]
        public void Should_Return_Friend_Trips_WhenUser_Is_Friend()
        {
            var tripService = _tripService;

            LOGGED_USER = ANOTHER_USER;

            var user = REGISTERED_USER;
            user.AddFriend(LOGGED_USER);
            user.AddTrip(TO_BRAZIL);
            user.AddTrip(TO_LONDON);

            _userSessionMock
                .Setup(x => x.GetLoggedUser())
                .Returns(LOGGED_USER);
            _tripDaoMock
                .Setup(x=>x.GetTripsBy(It.IsAny<User>()))
                .Returns(user.Trips());

            var tripsByUser = tripService.GetTripsByUser(user);

            tripsByUser
                .Should()
                .NotBeNull()
                .And
                .NotBeEmpty()
                .And
                .HaveCount(2);
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
