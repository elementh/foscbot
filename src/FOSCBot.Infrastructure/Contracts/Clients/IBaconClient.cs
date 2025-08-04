﻿namespace FOSCBot.Infrastructure.Contracts.Clients;

public interface IBaconClient
{
    Task<string> Get(string type, int sentences, CancellationToken cancellationToken = default);
}