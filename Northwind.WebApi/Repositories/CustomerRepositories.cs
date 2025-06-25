using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Caching.Memory;
using Northwind.DataContext.MySql;
using Northwind.EntityModels.Mysql;

namespace Northwind.WebApi.Repositories;

public class CustomerRepositories :ICustomerRepositories
{
    // IMemoryCache、MemoryCacheEntryOptions是.NetCore内置的内存缓存机制的核心组件
    // IMemoryCache：基于内存的轻量级缓存服务接口
    private readonly IMemoryCache _memoryCache;

    // MemoryCacheEntryOptions：精细化控制缓存项的行为，通过属性配置缓存策略
    private readonly MemoryCacheEntryOptions _cacheEntryOptions = new()
    {
        // SlidingExpiration：滑动过期，若缓存项在30小时内未被访问则自动失效
        SlidingExpiration = TimeSpan.FromHours(30)
    };

    private NorthwindContext _db;

    public CustomerRepositories(NorthwindContext db, IMemoryCache memoryCache)
    {
        _db = db;
        _memoryCache = memoryCache;
    }
    
    
    public async Task<Customer?> CreateAsync(Customer c)
    {
        // 转换为大写字符串
        if (c.CustomerId != null)
        {
            c.CustomerId = c.CustomerId.ToUpper();
            // EntityEntry：跟踪实体状态变更
            EntityEntry<Customer> added = await _db.Customers.AddAsync(c);
            int affected = await _db.SaveChangesAsync();
            if (affected == 1)
            {
                _memoryCache.Set(c.CustomerId, c, _cacheEntryOptions);
                // 成功时返回客户对象
                return c;
            }
        }

        return null;
    }

    public Task<Customer[]> RetrieveAllAsync()
    {
        return _db.Customers.ToArrayAsync();
    }

    public Task<Customer?> RetrieveAsync(string id)
    {
        id = id.ToUpper();
        
        // 先尝试缓存中获取需要的数据，存在则直接返回
        if (_memoryCache.TryGetValue(id, out Customer? formCatch))
            return Task.FromResult(formCatch);
        
        // 找到第一个满足CustomerId == id条件的客户对象，否则返回null（引用类型为null、值类型为0）
        Customer? formDb = _db.Customers
            .FirstOrDefault(c => c.CustomerId == id);
        
        // 返回数据或者null
        if (formDb is null) return Task.FromResult(formDb);
        
        // 设置缓存
        _memoryCache.Set(formDb.CustomerId!, formDb, _cacheEntryOptions);
        
        // 返回值
        return Task.FromResult(formDb)!;
    }
    
    public async Task<Customer?> UpdateAsync(Customer c)
    {
        if (c.CustomerId != null)
        {
            c.CustomerId = c.CustomerId.ToUpper();
            _db.Customers.Update(c);
            if (await _db.SaveChangesAsync() == 1)
            {
                _memoryCache.Set(c.CustomerId, c, _cacheEntryOptions);
                return c;
            }
        }

        return null;
    }

    public async Task<bool?> DeleteAsync(string id)
    {
        id = id.ToUpper();
        Customer? c = await _db.Customers.FindAsync(id);
        if (c is null) return null;
        
        _db.Customers.Remove(c);
        if (await _db.SaveChangesAsync() == 1)
        {
            _memoryCache.Remove(c.CustomerId!);
            return true;
        }

        return null;
    }
}