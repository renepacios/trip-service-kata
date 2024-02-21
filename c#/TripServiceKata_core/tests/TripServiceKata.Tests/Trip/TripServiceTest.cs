namespace TripServiceKata.Tests.Trip
{
    using Exception;
    using FluentAssertions;
    using Moq;
    using TripServiceKata.Trip;
    using User;

    public class TripServiceTest
    {
        public static User LOGGED_USER = null;
        public static User FRIEND_USER = new User();
        public static User ANOTHER_USER = new User();

        public static Trip GALICIA = new Trip();
        public static Trip BARCELONA = new Trip();

        private TripService _prodTripService;
        //private TestTripService _testTripService= new TestTripService();;
        private Mock<IUserSession> _userSessionMock;
        private IUserSession _userSession;
        private Mock<ITripDAO> _tripDaoMock;
        private ITripDAO _tripDao;

        public TripServiceTest()
        {
            _userSessionMock = new Mock<IUserSession>();
            _userSession = _userSessionMock.Object;
          
            _tripDaoMock = new Mock<ITripDAO>();
            _tripDao = _tripDaoMock.Object;
            
            _prodTripService = new TripService(_userSession, _tripDao);


        }

        [Fact]
        public void not_allow_get_trips_WhenUserIsNotLoggedIn()
        {
            LOGGED_USER = null;

            _userSessionMock.Setup(x => x.GetLoggedUser()).Returns(LOGGED_USER);

            _prodTripService
              .Invoking(s => s.GetTripsByUser(ANOTHER_USER))
                .Should()
                .Throw<UserNotLoggedInException>();



        }

        [Fact]
        public void allow_get_trips_When_LoggedUser_Is_Friend()
        {
            var tripService = _prodTripService;
            LOGGED_USER = new User();

            _userSessionMock
                .Setup(x => x.GetLoggedUser())
                .Returns(LOGGED_USER);

            _tripDaoMock
                .Setup(x => x.GetTripByUser(It.IsAny<User>()))
                .Returns(new System.Collections.Generic.List<Trip> { GALICIA }); 
            
            //user.AddFriend(FRIEND_USER);
            //user.AddFriend(LOGGED_USER);
            //user.AddTrip(GALICIA);
            //user.AddTrip(BARCELONA);

            var user = Builder.User
                 .WithFriends(FRIEND_USER, LOGGED_USER)
                 .WithTrips(GALICIA, BARCELONA)
                 .Build();


            var trips = tripService.GetTripsByUser(user);
            trips.Should().NotBeEmpty();
        }

        [Fact]
        public void not_allow_get_trips_when_user_is_not_friend()
        {
            var tripService = _prodTripService;

            var user = ANOTHER_USER;
            LOGGED_USER = new User();
            _userSessionMock
                .Setup(x => x.GetLoggedUser())
                .Returns(LOGGED_USER);

            user.AddFriend(FRIEND_USER);
            user.AddTrip(GALICIA);
            user.AddTrip(BARCELONA);

            var trips = tripService.GetTripsByUser(user);
            trips.Should().BeEmpty();
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


        [Fact]
        public void Add_another_friend_should_be_work_as_expected()
        {
            var user = Builder.User
                .WithFriends(ANOTHER_USER)
                .Build();
            user.IsFriend(FRIEND_USER).Should().BeFalse();

            //user.GetFriends().Should().Contain(friend);
        }
    }

    public class UserTest
    {

    }

    //public class TestTripService : TripService
    //{
    //    public TestTripService()
    //    {
    //    }

    //    public override User GetLoggedUser() => TripServiceTest.LOGGED_USER;

    //    public override List<Trip> GetTripByUser(User user)
    //    {
    //        return new List<Trip> { new Trip() };
    //    }
    //}
}
