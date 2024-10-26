using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext context;
        public ProductRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<Guid> AddAsync(Product product)
        {
            await context.Products.AddAsync(product);
            await context.SaveChangesAsync();
            return product.Id;
        }

        public async Task DeleteAsync(Guid id)
        {
            var product = await context.Products.FindAsync(id);
            context.Products.Remove(product);
            await context.SaveChangesAsync();
        }

        public async Task<Product> GetProductAsync(Guid id)
        {
            return await context.Products.FindAsync(id);
           
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await context.Products.ToListAsync();
        }

        public Task<Guid> UpdateAsync(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
