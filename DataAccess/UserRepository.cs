using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using Microsoft.EntityFrameworkCore;
using WikidVueServer.DataAccess.Abstractions;
using WikidVueServer.DataAccess.Data;

namespace WikidVueServer.DataAccess;

public sealed class UserRepository : IUserRepository
{
    #region Fields

    private readonly UserDbContext _context;

    #endregion

    #region Constructor

    public UserRepository(UserDbContext context)
    {
        _context = Guard.Against.Null(context);
    }

    #endregion

    #region Properties

    public Expression<Func<UserData, UserData>> DefaultUserDataMapper
    {
        get
        {
            return x => new UserData()
            {
                Created = x.Created,
                Email = x.Email,
                Id = x.Id,
                LastLogin = x.LastLogin,
                Password = x.Password,
                Sessions = x.Sessions,
                Username = x.Username
            };
        }
    }

    #endregion

    #region Public Methods

    public async Task<UserData> GetUser(string email, Expression<Func<UserData, UserData>> mapper = null)
    {
        Guard.Against.NullOrWhiteSpace(email);

        return await _context.Users
            .Where(x => x.Email == email.Trim())
            .Select(mapper ?? DefaultUserDataMapper)
            .FirstOrDefaultAsync();
    }

    #endregion
}