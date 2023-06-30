using System;
using Ardalis.GuardClauses;
using WikidVueServer.DataAccess.Abstractions;
using WikidVueServer.Services.Abstractions;

namespace WikidVueServer.Services;

public sealed class AuthService :IAuthService
{
    #region Fields

    private static readonly TimeSpan _accessTokenLifetime = TimeSpan.FromMinutes(15);
    private static readonly TimeSpan _refreshTokenLifetime = TimeSpan.FromHours(1);
    

    private readonly IUserRepository _userRepository;

    #endregion

    #region Constructor

    public AuthService(IUserRepository userRepository)
    {
        _userRepository = Guard.Against.Null(userRepository);
    }

    #endregion

    #region Public Methods

    #endregion
}