using BusinessLayer.Interface;
using CommonLayer.Model;
using RepoLayer.Interface;
using RepoLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class AdminUserBusiness : IAdminUserBusiness
    {
        private readonly IAdminUserRepo adminUserRepo;
        public AdminUserBusiness(IAdminUserRepo adminUserRepo)
        {
            this.adminUserRepo = adminUserRepo;
        }

        public string ForgotPassword(ForgotPassModel forgotPassModel)
        {
            try
            {
                return adminUserRepo.ForgotPassword(forgotPassModel);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string GenerateJwtToken(string email, int userAdminId, string role)
        {
            try
            {
                return adminUserRepo.GenerateJwtToken(email, userAdminId, role);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public LoginResultModel Login(UserLoginModel userLoginModel)
        {
            try
            {
                return adminUserRepo.Login(userLoginModel);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public UserTable Register(UserRegisModel registerModel)
        {
            try
            {
                return adminUserRepo.Register(registerModel);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool ResetPassword(ResetPassModel resetPassModel, string email)
        {
            try
            {
                return adminUserRepo.ResetPassword(resetPassModel, email);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
