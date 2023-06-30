
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace WikidVueServer;

public sealed class AppSettings
{
    private SymmetricSecurityKey _signingKey = null;
    
    public string Host { get; init; }

    public string JwtKey { get; init; }

    public SymmetricSecurityKey SigningKey
    {
        get
        {
            if (_signingKey == null)
            {
                _signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtKey));
            }

            return _signingKey;
        }
    }

    public bool AllowRegistration { get; init; }

    public string HttpClientName { get; init; } = "AutoRetryClient";

    public string Database { get; init; }

    public string DatabaseConnection { get; init; }
}
