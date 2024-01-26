using Causality.Mud.Common.Services;
using StackExchange.Redis;

namespace Causality.Mud.Core.Services;

public class RedisStorageService<T>:IStorageService<T>
    where T: class, new()
{
    private readonly IDatabase _database;
    
    public RedisStorageService(IDatabase database)
    {
        _database = database;
    }
}