using System;
using System.Net;
using System.Net.Mail;
using CoreDatabase.Managers;
using System.Threading.Tasks;
using KernelDLL.Network.Response;

namespace KernelDLL.Authentication
{
    public class UserManager : IUserManager
    {
        private readonly IUserDbManager _userDbManager;

        public UserManager()
        {
            _userDbManager = new UserDbManager();
            //_userDbManager = new FakeUserDbManager();
        }

        public async Task<RegisterResult> RegisterUserAsync(string username, string email, string password)
        {
            var user = await _userDbManager.GetUserByUsernameAsync(username);
            if (user != null)
            {
                return new RegisterResult(EnumRegisterResponse.UserExists);
            }

            user = await _userDbManager.GetUserByEmailAsync(email);
            if (user != null)
            {
                return new RegisterResult(EnumRegisterResponse.EmailExists);
            }

            var activationCode = Guid.NewGuid();
            var sucess = await _userDbManager.RegisterAsync(username, email, password, activationCode);

            if (!sucess)
            {
                return new RegisterResult(EnumRegisterResponse.UndefinedError);
            }

            //Verification Email
            VerificationEmail(email, activationCode.ToString());
            user = await _userDbManager.GetUserByUsernameAsync(username);
            return new RegisterResult(EnumRegisterResponse.Success, user.Id);
        }

        // TODO: Finish, fix and test. It's necessary?? It's possible??
        private void VerificationEmail(string email, string activationCode)
        {
            var fromEmail = new MailAddress("wotweb.test@gmail.com", "Activation Account - AKKA");
            var toEmail = new MailAddress(email);

            var fromEmailPassword = "wotweb1976";
            string subject = "Activation Account !";

            string body = "<br/> Please, introduce the following code to activate your account:" +
                          "<br/>'" + activationCode + "'";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
            };

            using (var message = new MailMessage(fromEmail, toEmail)
                { Subject = subject, Body = body, IsBodyHtml = true })
            {
                try
                {
                    smtp.Send(message);
                }
                catch (Exception e)
                {
                    var ex = e;
                    throw;
                }
            }
        }

        public async Task<LoginResult> LoginUserAsync(string usernameOrEmail, string password)
        {
            var success = await _userDbManager.LoginAsync(usernameOrEmail, password);
            if (success)
            {
                var loggedUser = await _userDbManager.GetUserByUsernameAsync(usernameOrEmail) ??
                           await _userDbManager.GetUserByEmailAsync(usernameOrEmail);
                return new LoginResult(loggedUser.Id);
            }

            var user = await _userDbManager.GetUserByUsernameAsync(usernameOrEmail) ??
                   await _userDbManager.GetUserByEmailAsync(usernameOrEmail);

            if (user == null)
            {
                return new LoginResult(EnumLoginResponse.WrongUserOrEmail);
            }

            if (user.Status == 0)
            {
                return new LoginResult(EnumLoginResponse.NotVerified);
            }

            if (user.Status == 2)
            {
                return new LoginResult(EnumLoginResponse.UserBlocked);
            }

            return new LoginResult(EnumLoginResponse.WrongPassword);
        }

        public async Task<LoginResultLegacy> LoginUserLegacyAsync(string usernameOrEmail, string password)
        {
            var success = await _userDbManager.LoginAsync(usernameOrEmail, password);
            if (success)
            {
                var user = await _userDbManager.GetUserByUsernameAsync(usernameOrEmail) ?? await _userDbManager.GetUserByEmailAsync(usernameOrEmail);

                if (user == null)
                {
                    return new LoginResultLegacy("User not found");
                }

                //if (!user.IsActive)
                //{
                //    return new LoginResultLegacy("User not actived");
                //}

                //if (user.IsBlocked)
                //{
                //    return new LoginResultLegacy("User blocked");
                //}

                return new LoginResultLegacy(user.Id);
            }

            return new LoginResultLegacy("Login failed");
        }

        public async Task<AuthenticationResult> AuthenticateUserAsync(int userId, string authenticationCode)
        {
            if (!Guid.TryParse(authenticationCode, out Guid guid)) return new AuthenticationResult(EnumAuthenticationResponse.InvalidCode);
            var success = await _userDbManager.CheckAuthenticationAsync(userId, guid);
            if (success)
            {
                await _userDbManager.AuthenticateAsync(userId);
                return new AuthenticationResult(EnumAuthenticationResponse.Success);
            }

            return new AuthenticationResult(EnumAuthenticationResponse.InvalidCode);
        }
    }
}
