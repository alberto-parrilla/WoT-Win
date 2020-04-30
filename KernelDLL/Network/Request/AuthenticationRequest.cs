using System;
using System.Threading.Tasks;
using KernelDLL.Authentication;
using KernelDLL.Network.Response;

namespace KernelDLL.Network.Request
{
    [Serializable]
    public class AuthenticationRequest : RequestMessageBase
    {
        public AuthenticationRequest(int userId, string authenticationCode)
        {
            UserId = userId;
            AuthenticationCode = authenticationCode;
        }

        public int UserId { get; }
        public string AuthenticationCode { get; }

        public override async Task<IResponse> ProcessRequestAsync()
        {
            var userManager = new UserManager();
            var result = await userManager.AuthenticateUserAsync(UserId, AuthenticationCode);
            return new AuthenticationResponse(result.Status);
        }
    }
}
