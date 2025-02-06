using System;
using System.Collections.Generic;

namespace Exceptions
{
    public class ProductException : Exception
    {
        public int ErrorCode { get; private set; }
        private static readonly Dictionary<int, string> ErrorMessages = new Dictionary<int, string>
        {
            { 100, "Produto inválido" },
            { 101, "Stock insuficiente" },
            { 102, "Preço inválido" },
            { 103, "Produto não encontrado" },
            { 104, "Operação não permitida para este usuário" }
        };

        public ProductException(int errorCode)
            : base(ErrorMessages.ContainsKey(errorCode) ? ErrorMessages[errorCode] : "Erro desconhecido")
        {
            ErrorCode = errorCode;
        }
    }
}