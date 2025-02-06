using BusinessObjects;
using System;


namespace BussinessObjects
{
    /// <summary>
    /// Classe que representa um produto simples com informações básicas.
    /// </summary>
    public class SimpleProduct
    {
        #region Atributos

        private Guid id;
        private string name;
        private string category;

        #endregion

        #region Métodos

        #region Construtores

        /// <summary>
        /// Construtor padrão.
        /// </summary>
        public SimpleProduct()
        {

        }

        /// <summary>
        /// Construtor que inicializa um SimpleProduct com nome e categoria.
        /// </summary>
        /// <param name="name">Nome do produto.</param>
        /// <param name="category">Categoria do produto.</param>
        public SimpleProduct(string name, string category)
        {
            Name = name;
            Category = category;
        }

        #endregion

        #region Propriedades

        /// <summary>
        /// ID único do produto.
        /// </summary>
        public Guid Id
        {
            get { return id; }
            set { id = value; }
        }

        /// <summary>
        /// Nome do produto.
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// Categoria do produto.
        /// </summary>
        public string Category
        {
            get { return category; }
            set { category = value; }
        }

        #endregion

        #region Outros Métodos

        #region Criar

        /// <summary>
        /// Cria uma instância de SimpleProduct a partir de um objeto Product.
        /// </summary>
        /// <param name="product">Objeto Product usado para criar SimpleProduct.</param>
        /// <returns>Instância de SimpleProduct.</returns>
        /// <exception cref="ProductException">Lançada se o produto fornecido for inválido.</exception>
        public static SimpleProduct CreateSimpleProduct(Product product)
        {
            if (product == null)
            {
                throw new InvalidOperationException("Produto inválido"); // Produto inválido
            }

            SimpleProduct simpleProduct = new SimpleProduct(product.Nome, product.Category)
            {
                Id = product.Id
            };

            return simpleProduct;
        }

        #endregion

        #endregion

        #endregion
    }
}
