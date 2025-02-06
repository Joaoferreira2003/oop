using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using Utilities;

/// <summary>
/// Classe responsável pela gestão de produtos no sistema.
/// </summary>
namespace Data
{
    public class Products
    {
        /// <summary>
        /// Lista de produtos atualmente no sistema.
        /// </summary>
        private static List<Product> productList = new List<Product>();

        /// <summary>
        /// Adiciona um produto à lista de produtos.
        /// </summary>
        /// <param name="product">O produto a ser adicionado.</param>
        /// <returns>True se o produto foi adicionado com sucesso; caso contrário, retorna false.</returns>
        public static bool AddProduct(Product product)
        {
            if (product == null)
            {
                Console.WriteLine("Erro: Produto inválido.");
                return false;
            }

            if (productList.Contains(product))
            {
                Console.WriteLine("Erro: Produto duplicado.");
                return false;
            }

            productList.Add(product);
            Logger.Log($"Produto {product.Nome} adicionado ao sistema.");
            return true;
        }

        /// <summary>
        /// Exibe uma lista de produtos, paginada.
        /// </summary>
        /// <param name="page">Número da página a ser exibida (padrão: 1).</param>
        /// <param name="pageSize">Número de produtos por página (padrão: 10).</param>
        /// <returns>Uma string contendo os produtos da página solicitada.</returns>
        public static string ShowProducts(int page = 1, int pageSize = 10)
        {
            int total = productList.Count;
            int skip = (page - 1) * pageSize;
            var products = productList.Skip(skip).Take(pageSize).ToList();

            if (!products.Any())
            {
                return "Nenhum produto encontrado.";
            }

            return string.Join(Environment.NewLine, products.Select(p => p.ToString())) +
                   $"\nPágina {page} de {Math.Ceiling((double)total / pageSize)}";
        }

        /// <summary>
        /// Exibe os detalhes de um produto específico com base no ID.
        /// </summary>
        /// <param name="productId">ID do produto a ser exibido.</param>
        /// <returns>Uma string com os detalhes do produto ou uma mensagem de erro.</returns>
        public static string ShowProductById(Guid productId)
        {
            foreach (var product in productList)
            {
                if (product.Id == productId)
                {
                    return product.ToString();
                }
            }
            return "Erro: Produto não encontrado.";
        }
    }
}
