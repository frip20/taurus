using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using taurus.Core.Entities;

namespace taurus.Core.Interfaces
{
    public interface ICastleProvider
    {
        bool Save<T>(CastleProvider<T> entity);
        bool Update<T>(CastleProvider<T> entity);
        bool Delete<T>(CastleProvider<T> entity);

        int userInAction { get; }
    }
}
