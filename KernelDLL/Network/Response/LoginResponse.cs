﻿using System;

namespace KernelDLL.Network.Response
{
    public enum EnumLoginResponse
    {
        Success,
        NotVerified,
        WrongUserOrEmail,
        WrongPassword,
        UserBlocked
    }

    [Serializable]
    public class LoginResponse : ResponseMessageBase
    {
        public LoginResponse(EnumLoginResponse status, int? userId)
        {
            Status = status;
            UserId = userId;
        }

        public EnumLoginResponse Status { get; }
        public int? UserId { get; }

        public override EnumResponseType ResponseType => EnumResponseType.Login;
    }
}
