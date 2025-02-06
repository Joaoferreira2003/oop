using System;
using System.Collections.Generic;

namespace Exceptions
{
    /// <summary>
    /// Classe que representa uma exceção personalizada para erros relacionados com as regras de produtos.
    /// </summary>
    public class ProductRulesException : Exception
    {
        /// <summary>
        /// Código de erro associado à exceção.
        /// </summary>
        private int errorCode;

        /// <summary>
        /// Dicionário que contém mensagens de erro associadas a códigos específicos.
        /// </summary>
        private static readonly Dictionary<int, string> ProductsErrorMessage = new Dictionary<int, string>()
        {
            { 100, "Não tem permissões para realizar esta ação" },
            { 101, "Produto inválido" },
            { 102, "Quantidade insuficiente de stock" },
            { 103, "Categoria do produto inválida" },
            { 104, "Preço fora do intervalo permitido" }
        };

        /// <summary>
        /// Construtor que inicializa a exceção com um código de erro.
        /// </summary>
        /// <param name="error">Código de erro associado.</param>
        public ProductRulesException(int error) : base(ProductsErrorMessage.ContainsKey(error) ? ProductsErrorMessage[error] : "Erro desconhecido")
        {
            ErrorCode = error;

            // O uso de Console.WriteLine foi removido para centralizar o tratamento de logs.
        }

        /// <summary>
        /// Propriedade para obter ou definir o código de erro associado à exceção.
        /// </summary>
        public int ErrorCode
        {
            get { return errorCode; }
            set { errorCode = value; }
        }
    }
}
