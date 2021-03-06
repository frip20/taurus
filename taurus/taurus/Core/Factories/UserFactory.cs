﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using taurus.Core.Interfaces;
using taurus.Core.Entities;
using taurus.Core.Services;
using taurus.Core.Exceptions;
using NHibernate.Criterion;
using taurus.Core.Constants;

namespace taurus.Core.Factories
{
    public class UserFactory : IUser
    {
        public bool hasAccess(string userName, string password)
        {
            return true;
        }


        public int validateUser(string userName, string pwd)
        {
            string encryptPwd = EncryptService.Instance.Encrypt(pwd);

            if (userName.Trim() != "") {
                List<User> users = User.FindAllByProperty("userName", userName).ToList<User>();
                if (users == null || users.Count <= 0)
                    throw new InvalidUserException(string.Format(MessageService.INVALID_USERNAME, userName));
                else {
                    foreach (User user in users)
                    {
                        if (user.Password == encryptPwd){
                            return user.Id;
                        }
                    }
                    throw new InvalidUserException(string.Format(MessageService.INVALID_USERPASSWORD, userName));
                }
            }
            return 0;
        }

        public User searchObjectById(int id)
        {
            User _user = User.Find(id);
            if (_user == null)
                throw new UserRolException(MessageService.USER_NOTFOUND);
            else
                return _user;
        }

        public IList<taurus.Core.Entities.Action> actionsByUser(int userId)
        {
            User _user = searchObjectById(userId);
            if (_user.userRol != null)
            {
                return _user.userRol.Actions;
            }
            else
            {
                throw new UserRolException(MessageService.INVALID_USERROLE);
            }
        }

        public IList<User> getAllUsers() {
            return User.FindAll();
        }

        public bool validateUserName(string userName)
        {
            List<User> users = User.FindAllByProperty("userName", userName).ToList<User>();
            return (users.Count <= 0);
        }
        
        public void registerLogin(User user)
        {
            try
            {
                if (user != null)
                {
                    user.lastAccessDate = DateTime.Now;
                    user.SaveAndFlush();
                    AuditService.Instance.registerEvent(EventType.USER_LOGGING, user.Id, string.Format(MessageService.USER_LOGGING, user.userName));
                }
                else
                {
                    throw new InvalidUserException(string.Format(MessageService.UNDEFINED_OBJECT, "userName"));
                }
            }
            catch (InvalidUserException ex) {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new InvalidUserException(string.Format(MessageService.USER_NOTFOUND, "registerLogin"), ex);
            }
        }

    }
}