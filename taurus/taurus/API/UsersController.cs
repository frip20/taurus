using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Net.Http;
using System.Net;
using taurus.Core.Entities;
using taurus.Core.Interfaces;
using taurus.Core.Exceptions;
using taurus.Core.Web;
using taurus.Core.Services;
using taurus.Core.API;
using taurus.Core.Constants;

namespace taurus.API
{
    public class UsersController : ApiController
    {
        private readonly IUser _user;
        private readonly ICastleProvider _provider;

        public UsersController(IUser user, ICastleProvider provider)
        {
            _user = user;
            _provider = provider;
        }

        public HttpResponseMessage Get()
        {
            try
            {
                IList<User> users = _user.getAllUsers();
                return new TaurusResponseMessage(users);
            }
            catch (Exception ex)
            {
                return new TaurusResponseMessage(true, ex.Message);
            }
        }

        public HttpResponseMessage Post(UserRequest request)
        {
            try
            {
                string pwd = "";
                User us = null;
                if(request.User.Id>0) 
                    us = _user.searchObjectById(request.User.Id);

                switch (request.Action)
                {
                    case APIActions.ADD:
                        if (!_user.validateUserName(request.User.userName))
                            throw new InvalidUserException(string.Format(MessageService.USERNAME_EXISTS, request.User.userName));

                        pwd = EncryptService.Instance.Encrypt(request.User.Password);
                        us = request.User;
                        us.Password = pwd;
                        us.lastAccessDate = DateTime.Today;
                        _provider.Save(us);
                        break;
                    case APIActions.UPDATE:
                        us.userRol = request.User.userRol;
                        if (us.Password != request.User.Password) {
                            pwd = EncryptService.Instance.Encrypt(request.User.Password);
                            us.Password = pwd;
                        }
                        us.lastAccessDate = DateTime.Today;
                        _provider.Update(us);
                        break;
                    case APIActions.SEARCHBYID:
                        break;
                    case APIActions.LOCK:
                        us.Enable = false;
                        _provider.Update(us);
                        break;
                    case APIActions.UNLOCK:
                        us.Enable = true;
                        _provider.Update(us);
                        break;
                    default:
                        break;
                }
                return new TaurusResponseMessage(us);
            }
            catch (Exception ex)
            {
                return new TaurusResponseMessage(true, ex.Message);
            }
        }

        
    }
}