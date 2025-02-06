using System;

namespace BusinessObjects
{
    /// <summary>
    /// Representa um produto no sistema.
    /// </summary>
    public class Product
    {
        public string Nome { get; set; }
        public string Category { get; set; }
        public double Preco { get; set; }
        public int Stock { get; set; }
        public Guid Id { get; internal set; }

        public Product(string nome, string category, double preco, int stock)
        {
            Nome = nome;
            Category = category;
            Preco = preco;
            Stock = stock;
        }
    }
}