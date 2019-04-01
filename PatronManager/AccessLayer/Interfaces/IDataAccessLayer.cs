using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevExpressPlayAround.AccessLayer.Interfaces
{
  public interface IDataAccessLayer<T> where T : class, new()
  {
    #region Public methods

    void ConnectToDataBase(string connectionString);

    Task ConnectToDataBaseAsync(string connectionString);

    void Create();
    Task CreateAsync();

    void Delete(T obj);
    IEnumerable<T> Read();
    Task<IEnumerable<T>> ReadAsync();
    void Update();
    Task UpdateAsync();

    #endregion
  }
}