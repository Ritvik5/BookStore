﻿using CommonLayer.Model;
using RepoLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepoLayer.Interface
{
    public interface IAdminUserRepo
    {
        UserTable Register(UserRegisModel registerModel);
        string GenerateJwtToken(string email, int userAdminId, string role);
        LoginResultModel Login(UserLoginModel userLoginModel);
        string ForgotPassword(ForgotPassModel forgotPassModel);
        bool ResetPassword(ResetPassModel resetPassModel, string email);
    }
}