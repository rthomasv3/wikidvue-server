using System;
using WikidVueServer.Services.Abstractions;

namespace WikidVueServer.Services;

public sealed class AuthService :IAuthService
{
    #region Fields

    private static readonly TimeSpan _accessTokenLifetime = TimeSpan.FromMinutes(15);
    private static readonly TimeSpan _refreshTokenLifetime = TimeSpan.FromHours(1);
    
    #endregion

    #region Constructor

    public AuthService()
    {
        
    }

    #endregion

    #region Public Methods

    #endregion
}