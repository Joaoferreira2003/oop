using System;
using Utilities;
using BusinessObjects;

/// <summary>
/// Classe que contém as regras de negócio para gerir produtos.
/// </summary>
namespace Exceptions
{
    public class ProductExceptionRules : Exception // Definindo a classe ProductExceptionRules
    {
        public ProductExceptionRules(int errorCode) : base($"Erro de permissão com código: {errorCode}")
        {
        }
    }

    public enum Permissions
    {
        Dono,
        Gerente
    }

    public static class ProductRules
    {
        /// <summary>
        /// Verifica se o utilizador tem permissões suficientes.
        /// </summary>
        /// <param name="userPerms">Permissões do utilizador.</param>
        /// <param name="requiredPerms">Permissões necessárias.</param>
        /// <returns>True se o utilizador tiver permissões suficientes; False caso contrário.</returns>
        public static bool CheckPermissions(Permissions userPerms, Permissions requiredPerms) => userPerms >= requiredPerms;

        /// <summary>
        /// Lança uma exceção se o utilizador não tiver permissões necessárias.
        /// </summary>
        /// <param name="userPerms">Permissões do utilizador.</param>
        /// <param name="requiredPerms">Permissões necessárias.</param>
        /// <exception cref="ProductExceptionRules">Se o utilizador não tiver permissões suficientes.</exception>
        public static void RequirePermission(Permissions userPerms, Permissions requiredPerms)
        {
            if (!CheckPermissions(userPerms, requiredPerms))
                throw new ProductExceptionRules(100);
        }

        /// <summary>
        /// Cria um produto caso o utilizador tenha permissões.
        /// </summary>
        /// <param name="perms">Permissões do utilizador.</param>
        /// <param name="name">Nome do produto.</param>
        /// <param name="category">Categoria do produto.</param>
        /// <param name="price">Preço do produto.</param>
        /// <param name="stock">Quantidade no stock.</param>
        /// <returns>Instância do produto criado.</returns>
        public static Product TryCreateProduct(Permissions perms, string name, string category, double price, int stock)
        {
            RequirePermission(perms, Permissions.Gerente);
            return new Product(name, category, price, stock);
        }

        /// <summary>
        /// Adiciona um produto ao sistema caso permitido.
        /// </summary>
        /// <param name="perms">Permissões do utilizador.</param>
        /// <param name="product">Produto a ser adicionado.</param>
        /// <returns>True se o produto for adicionado com sucesso.</returns>
        public static bool TryAddProduct(Permissions perms, Product product)
        {
            RequirePermission(perms, Permissions.Gerente);
            AdicionarStock(product.Nome, product.Stock);
            Logger.Log($"Produto {product.Nome} adicionado.");
            return true;
        }

        /// <summary>
        /// Visualiza os produtos disponíveis no sistema.
        /// </summary>
        /// <param name="perms">Permissões do utilizador.</param>
        /// <param name="productId">ID do produto (opcional).</param>
        /// <returns>Lista de produtos ou detalhes de um produto específico.</returns>
        public static string TryViewProducts(Permissions perms, Guid? productId = null)
        {
            return perms >= Permissions.Dono
                ? productId != null ? ShowProductById(productId.Value) : ListarProdutos()
                : throw new ProductExceptionRules(100);
        }

        /// <summary>
        /// Mostra os detalhes de um produto específico pelo ID.
        /// </summary>
        /// <param name="productId">ID do produto.</param>
        /// <returns>Detalhes do produto.</returns>
        private static string ShowProductById(Guid productId)
        {
            // Implementação fictícia para retornar detalhes do produto pelo ID
            return $"Detalhes do produto com ID: {productId}";
        }

        /// <summary>
        /// Adiciona a quantidade de stock ao produto.
        /// </summary>
        /// <param name="nome">Nome do produto.</param>
        /// <param name="stock">Quantidade a ser adicionada.</param>
        private static void AdicionarStock(string nome, int stock)
        {
            // Implementação fictícia para adicionar stock ao produto
        }

        /// <summary>
        /// Lista todos os produtos disponíveis.
        /// </summary>
        /// <returns>Lista de produtos.</returns>
        private static string ListarProdutos()
        {
            // Implementação fictícia para listar todos os produtos
            return "Lista de todos os produtos disponíveis.";
        }
    }
}
