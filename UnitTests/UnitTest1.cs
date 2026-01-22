using SchoolTermTracker.Models;

namespace UnitTests
{
    public class UnitTest1
    {
        [Fact]
        public async void SucessfulLogin()
        {
            bool result = await User.CheckUser("admin", "password", true);

            Assert.True(result);
        }

        [Fact]
        public async void FailedLogin()
        {
            bool result = await User.CheckUser("notUser", "wrongPassword", true);

            Assert.False(result);
        }
    }
}