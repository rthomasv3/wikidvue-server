using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WikidVueServer.DataAccess.Data;

namespace WikidVueServer.DataAccess.Abstractions;

public interface IUserRepository
{
    Expression<Func<UserData, UserData>> DefaultUserDataMapper { get; }
    Task<UserData> GetUser(string email, Expression<Func<UserData, UserData>> mapper = null);
}
