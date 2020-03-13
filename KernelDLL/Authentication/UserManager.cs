using System;
using System.Net;
using System.Net.Mail;
using CoreDatabase.Managers;
using System.Threading.Tasks;

namespace KernelDLL.Authentication
{
    public class UserManager : IUserManager
    {
        private readonly IUserDbManager _userDbManager;

        public UserManager()
        {
            _userDbManager = new UserDbManager();
        }

        public async Task<RegisterResult> RegisterUserAsync(RegisterUser registerUser)
        {
            var user = await _userDbManager.GetUserByEmailAsync(registerUser.Email);
            if (user != null)
            {
                return new RegisterResult("Email already exists");
            }

            user = await _userDbManager.GetUserByUsernameAsync(registerUser.Username);
            if (user != null)
            {
                return new RegisterResult("Username already exists");
            }

            var activationCode = Guid.NewGuid();
            var sucess = await _userDbManager.RegisterAsync(registerUser.Username, registerUser.Email, registerUser.Password, activationCode);

            if (!sucess)
            {
                return new RegisterResult("Error during registration");
            }

            //Verification Email
            VerificationEmail(registerUser.Email, activationCode.ToString());
            return new RegisterResult();
        }

        // TODO: Finish, fix and test. It's necessary?? It's possible??
        private void VerificationEmail(string email, string activationCode)
        {
            var url = string.Format("/Account/ActivationAccount/{0}", activationCode);
            //var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, url);
            var link = "Test address";

            var fromEmail = new MailAddress("wotweb.test@gmail.com", "Activation Account - AKKA");
            var toEmail = new MailAddress(email);

            var fromEmailPassword = "wotweb1976";
            string subject = "Activation Account !";

            string body = "<br/> Please click on the following link in order to activate your account" +
                          "<br/><a href='" + link + "'> Activation Account ! </a>";

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
                var user = await _userDbManager.GetUserByUsernameAsync(usernameOrEmail) ?? await _userDbManager.GetUserByEmailAsync(usernameOrEmail);

                if (user == null)
                {
                    return new LoginResult("User not found");
                }

                if (!user.IsActive)
                {
                    return new LoginResult("User not actived");
                }

                if (user.IsBlocked)
                {
                    return new LoginResult("User blocked");
                }

                return new LoginResult(user.Id);
            }

            return new LoginResult("Login failed");
        }
    }
}
