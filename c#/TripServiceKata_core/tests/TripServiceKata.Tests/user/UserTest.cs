using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TripServiceKata.Tests.user
{
    using FluentAssertions;
    using User;

    public class UserTest
    {
        [Fact]
        public void add_user_friend_work_as_expected()
        {
            var user = new User();
            var friend = new User();
            user.AddFriend(friend);
            user.GetFriends().Should().Contain(friend);
        }

        [Fact]
        public void check_user_friends_work_as_expected()
        {
            var user = new User();
            var friend = new User();

            user.AddFriend(friend);

            user.IsFriendOf(friend)
                .Should()
                .BeTrue();
        }

        [Fact]
        public void check_no_user_friends_work_as_expected()
        {
            var user = new User();
            var friend = new User();
            var anotherUser = new User();
            user.AddFriend(friend);

            user.IsFriendOf(anotherUser)
                .Should()
                .BeFalse();
        }
    }
}
