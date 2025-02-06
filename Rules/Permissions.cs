using System;
using System.Collections.Generic;

namespace Rules
{
    public static class Permissions
    {
        private static readonly Dictionary<string, string> usuarios = new Dictionary<string, string>
        {
            { "funcionario", "F123" },
            { "gerente", "G456" },
            { "dono", "D789" }
        };

        public static bool Login(out string credencial, out string codigo)
        {
            Console.Clear();
            Console.Write("Digite sua credencial: ");
            credencial = Console.ReadLine().ToLower();

            if (usuarios.ContainsKey(credencial))
            {
                Console.Write("Digite seu código de acesso: ");
                codigo = Console.ReadLine();
                if (usuarios[credencial] == codigo)
                {
                    return true;
                }
            }
            codigo = "";
            return false;
        }

        /// <summary>
        /// Verifica se o usuário logado tem uma das permissões necessárias.
        /// </summary>
        public static bool VerifyPermission(string user, params string[] allowedRoles)
        {
            foreach (var role in allowedRoles)
            {
                if (string.Equals(user, role, StringComparison.OrdinalIgnoreCase))
                    return true;
            }
            return false;
        }
    }
}