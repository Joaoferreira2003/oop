using System;

namespace BusinessObjects
{
    /// <summary>
    /// Classe abstrata que representa um item genérico no sistema de comércio eletrônico.
    /// </summary>
    public abstract class Item
    {
        public abstract void DisplayDetails(); // Método abstrato para ser implementado nas subclasses

        #region Atributos

        private Guid id;
        private string name;
        private string category;
        private int quantity;
        private double price;

        #endregion

        #region Métodos

        #region Construtores

        /// <summary>
        /// Construtor padrão.
        /// </summary>
        public Item()
        {
        }

        /// <summary>
        /// Construtor que inicializa um item com os atributos especificados.
        /// </summary>
        /// <param name="name">Nome do item.</param>
        /// <param name="category">Categoria do item.</param>
        /// <param name="quantity">Quantidade disponível.</param>
        /// <param name="price">Preço por unidade.</param>
        public Item(string name, string category, int quantity, double price)
        {
            Id = Guid.NewGuid();
            Name = name;
            Category = category;
            Quantity = quantity;
            Price = price;
        }

        #endregion

        #region Propriedades

        /// <summary>
        /// Obtém ou define o ID único do item.
        /// </summary>
        public Guid Id
        {
            get { return id; }
            set { id = value; }
        }

        /// <summary>
        /// Obtém ou define o nome do item.
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// Obtém ou define a categoria do item.
        /// </summary>
        public string Category
        {
            get { return category; }
            set { category = value; }
        }

        /// <summary>
        /// Obtém ou define a quantidade disponível do item.
        /// </summary>
        public int Quantity
        {
            get { return quantity; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("A quantidade não pode ser negativa.");
                quantity = value;
            }
        }

        /// <summary>
        /// Obtém ou define o preço por unidade do item.
        /// </summary>
        public double Price
        {
            get { return price; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("O preço não pode ser negativo.");
                price = value;
            }
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Retorna uma representação textual do item.
        /// </summary>
        /// <returns>String com as informações do item.</returns>
        public override string ToString()
        {
            return $"[ID: {Id}] {Name}\nCategoria: {Category}\nPreço: R$ {Price:F2}\nQuantidade: {Quantity}";
        }

        #endregion

        #endregion
    }
}
