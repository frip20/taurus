using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace taurus.Core.Services
{
    public sealed class MessageService
    {
        public const string INVALID_USERNAME = "No existe un usuario con nombre: {0}";
        public const string INVALID_USERPASSWORD = "Password invalido para el usuario: {0}";
        public const string USER_NOTFOUND = "El usuario: {0} no fue encontrado";

        public const string INVALID_USERROLE = "El usuario: {0} tiene un rol invalido";

        public const string UNDEFINED_OBJECT = "{0} - El registro no este definido";

    }
}
