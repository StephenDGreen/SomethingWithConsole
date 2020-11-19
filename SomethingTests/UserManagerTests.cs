using Something.Security;
using System.IdentityModel.Tokens.Jwt;
using Xunit;


namespace SomethingTests
{
    public class UserManagerTests
    {


        [Fact]
        public void UserManager_GetUserPrinciple_ReturnsUserPrinciple()
        {
            var userManager = new SomethingUserManager();

            var actual = userManager.GetUserPrinciple();

            Assert.IsType<System.Security.Claims.ClaimsPrincipal>(actual);
        }

        [Fact]
        public void UserManager_GetUserToken_ReturnsJwtToken()
        {
            var userManager = new SomethingUserManager();
            var jwtHandler = new JwtSecurityTokenHandler();

            string actual = userManager.GetUserToken();

            Assert.True(jwtHandler.CanReadToken(actual));
        }
    }
}
