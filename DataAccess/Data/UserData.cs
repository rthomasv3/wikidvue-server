using System;
using System.Collections.Generic;

namespace WikidVueServer.DataAccess.Data;

public partial record UserData
{
    public long Id { get; init; }

    public string Username { get; init; }

    public string Password { get; init; }

    public DateTime Created { get; init; }

    public DateTime? LastLogin { get; init; }

    public string Email { get; init; }

    public virtual ICollection<UserSessionData> Sessions { get; init; } = new List<UserSessionData>();
}
