using BusinessLayer.Abstract;
using BussinessLayer.Utilities.Hashing;
using EntityLayer.Concrete;
using EntityLayer.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{

    public class AuthManager : IAuthService
    {
        IAdminService _adminService;
        IWriterService _writerService;

        public AuthManager(IAdminService adminService)
        {
            _adminService = adminService;
        }

        public AuthManager(IWriterService writerService)
        {
            _writerService = writerService;
        }

        public AuthManager(IAdminService adminService, IWriterService writerService)
        {
            _adminService = adminService;
            _writerService = writerService;

        }

        public bool AdminLogin(AdminLoginDto adminDto)
        {
            using (var crypto = new System.Security.Cryptography.HMACSHA512())
            {
                var userNameHash = crypto.ComputeHash(Encoding.UTF8.GetBytes(adminDto.AdminUserName));
                var user = _adminService.GetList();
                foreach (var item in user)
                {
                    if (HashingHelper.AdminVerifyPasswordHash(adminDto.AdminUserName, adminDto.AdminPassword, item.AdminUserName, item.AdminPasswordHash, item.AdminPasswordSalt))
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        public void AdminRegister(string adminName, string adminMail, string password, int roleId)
        {
            byte[] userNameHash, passwordHash, passwordSalt;
            HashingHelper.AdminCreatePasswordHash(adminMail, password, out userNameHash, out passwordHash, out passwordSalt);
            var admin = new Admin
            {
                AdminName = adminName,
                AdminUserName = userNameHash,
                AdminPasswordHash = passwordHash,
                AdminPasswordSalt = passwordSalt,
                RoleId = roleId
            };
            _adminService.AdminAddBL(admin);
        }

        //------------------------- WRITER -----------------------------\\

        public bool WriterLogin(WriterLoginDto writerLogInDto)
        {
            using (var crypto = new System.Security.Cryptography.HMACSHA512())
            {
                //var mailHash = crypto.ComputeHash(Encoding.UTF8.GetBytes(writerLogInDto.WriterMail));
                var writer = _writerService.GetList();
                foreach (var item in writer)
                {
                    if (HashingHelper.WriterVerifyPasswordHash(writerLogInDto.WriterPassword,
                        item.WriterPasswordHash, item.WriterPasswordSalt))
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        public void WriterRegister(string name, string surname, string ımage, string about, string writerMail, string writerPassword, string title,int roleId)
        {

            byte[] passwordHash, passwordSalt;

            HashingHelper.WriterCreatePasswordHash(writerPassword, out passwordHash, out passwordSalt);
            var writer = new Writer
            {
                WriterName = name,
                WriterSurname = surname,
                WriterImage = ımage,
                WriterAbout = about,
                WriterMail = writerMail,
                WriterPasswordHash = passwordHash,
                WriterPasswordSalt = passwordSalt,
                WriterTitle = title,             
                RoleId = roleId



            };
            _writerService.WriterAddBL(writer);
        }
        public void WriterEditPassword(int writerID,string writerPassword)
        {

            byte[] passwordHash, passwordSalt;

            HashingHelper.WriterCreatePasswordHash(writerPassword, out passwordHash, out passwordSalt);
            var writer = new Writer
            {
                WriterID = writerID,
                WriterPasswordHash = passwordHash,
                WriterPasswordSalt = passwordSalt,
              



            };
            _writerService.WriterUpdate(writer);
        }

  
    }
    }

