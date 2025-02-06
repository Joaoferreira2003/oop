using BusinessObjects;
using Data;
using Exceptions;
using System.Collections.Generic;

namespace Business
{
    public class ProductService
    {
        private readonly IProductRepository repository;

        public ProductService(IProductRepository repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// Retorna todos os produtos.
        /// </summary>
        public IEnumerable<Product> ListProducts() => repository.GetAll();

        /// <summary>
        /// Efetua a venda de um produto, verificando existência e stock.
        /// </summary>
        /// <exception cref="ProductException">
        /// Lança exceção se o produto não existir ou se o stock for insuficiente.
        /// </exception>
        public void SellProduct(string nome, int quantidade, string credencial, string codigo)
        {
            var product = repository.GetByName(nome);
            if (product == null)
                throw new ProductException(103); // Produto não encontrado

            if (product.Stock < quantidade)
                throw new ProductException(101); // Stock insuficiente

            // Reduz o stock
            product.Stock -= quantidade;
            repository.Update(product);

            // Em um cenário mais completo, a venda (log ou registro) seria realizada aqui.
        }

        /// <summary>
        /// Adiciona stock ao produto especificado.
        /// </summary>
        /// <exception cref="ProductException">Lança exceção se o produto não existir.</exception>
        public void AddStock(string nome, int quantidade)
        {
            var product = repository.GetByName(nome);
            if (product == null)
                throw new ProductException(103); // Produto não encontrado

            product.Stock += quantidade;
            repository.Update(product);
        }
    }
}