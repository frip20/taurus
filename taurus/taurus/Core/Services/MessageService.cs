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
        public const string USERNAME_EXISTS = "El nombre de usuario: {0} ya existe";
        public const string USER_BLOCKED = "El usuario {0} esta bloqueado";

        public const string INVALID_USERROLE = "El usuario: {0} tiene un rol invalido";

        public const string UNDEFINED_OBJECT = "{0} - El registro no este definido";


        public const string CASTLE_DELETE_ERROR = "{0} : Error al eliminar el registro";
        public const string CASTLE_SAVE_ERROR = "{0} : Error al guardar el registro";
        public const string CASTLE_UPDATE_ERROR = "{0} : Error al actualizar el registro";
        public const string CASTLE_SEARCH_ERROR = "{0} : Error al buscar los registros";

    }
}
