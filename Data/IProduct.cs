using BusinessObjects;
using System.Collections.Generic;
using System.Linq;

namespace Data
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAll();
        Product GetByName(string nome);
        void Update(Product product);
        void Add(Product product);
    }

    public class InMemoryProductRepository : IProductRepository
    {
        private List<Product> products = new List<Product>();

        public IEnumerable<Product> GetAll()
        {
            return products;
        }

        public Product GetByName(string nome)
        {
            return products.FirstOrDefault(p => p.Nome == nome);
        }

        public void Update(Product product)
        {
            var existingProduct = GetByName(product.Nome);
            if (existingProduct != null)
            {
                existingProduct.Preco = product.Preco;
                existingProduct.Stock = product.Stock;
            }
        }

        public void Add(Product product)
        {
            products.Add(product);
        }
    }
}