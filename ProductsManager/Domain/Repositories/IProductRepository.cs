using Domain.Entities;

namespace Domain.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProductsAsync();
        Task<Product> GetProductAsync(Guid id);
        Task<Guid> AddAsync(Product product);
        Task DeleteAsync(Guid id);
        Task UpdateAsync(Product product);
    }
}
