using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using taurus.Core.Entities;

namespace taurus.Core.Interfaces
{
    public interface IUser : ISearchable<User>
    {
        bool hasAccess(string userName, string password);

        int validateUser(string userName, string pwd);

        bool validateUserName(string userName);

        IList<taurus.Core.Entities.Action> actionsByUser(int userId);

        IList<User> getAllUsers();
    }
}