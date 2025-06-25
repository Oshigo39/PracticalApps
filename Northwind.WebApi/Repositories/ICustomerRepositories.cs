using Northwind.EntityModels.Mysql;

namespace Northwind.WebApi.Repositories;

/// <summary>
/// Customer表的CRUD方法的接口
/// </summary>
public interface ICustomerRepositories
{
    // 异步创建数据
    Task<Customer?> CreateAsync(Customer c);
    // 异步检索所有数据
    Task<Customer[]> RetrieveAllAsync();
    // 根据ID异步检索数据
    Task<Customer?> RetrieveAsync(string id);
    // 异步更新数据
    Task<Customer?> UpdateAsync(Customer c);
    // 异步删除数据
    Task<bool?> DeleteAsync(string id);
}