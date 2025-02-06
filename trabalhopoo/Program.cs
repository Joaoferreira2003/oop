using System;
using System.Collections.Generic;
using Business;
using Data;
using Exceptions;
using Permissions = Rules.Permissions;

namespace trabalhopoo
{
    class Program
    {
        private static ProductService productService;
        private static List<string> vendas = new List<string>();

        static void Main(string[] args, ProductService productService)
        {
            productService = new ProductService(new InMemoryProductRepository());
            ExibirMenu();
        }

        private static void ExibirMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Menu de Comércio Eletrônico ===");
                Console.WriteLine("1. Vender Produto");
                Console.WriteLine("2. Mostrar Stock");
                Console.WriteLine("3. Adicionar Stock");
                Console.WriteLine("4. Visualizar Vendas");
                Console.WriteLine("5. Sair");
                Console.Write("Escolha uma opção: ");

                switch (Console.ReadLine())
                {
                    case "1": VenderProduto(); break;
                    case "2": MostrarStock(); break;
                    case "3": AdicionarStock(); break;
                    case "4": VisualizarVendas(); break;
                    case "5": Console.WriteLine("Saindo..."); return;
                    default: Console.WriteLine("Opção inválida."); break;
                }
                Console.WriteLine("Pressione qualquer tecla para continuar...");
                Console.ReadKey();
            }
        }

        private static void VenderProduto()
        {
            Console.Clear();
            Console.Write("Digite o nome do produto vendido: ");
            string nome = Console.ReadLine();

            Console.Write("Digite a quantidade vendida: ");
            if (!int.TryParse(Console.ReadLine(), out int quantidade))
            {
                Console.WriteLine("Quantidade inválida.");
                return;
            }

            if (!Permissions.Login(out string credencial, out string codigo))
            {
                Console.WriteLine("Login falhou. Venda não registrada.");
                return;
            }

            try
            {
                productService.SellProduct(nome, quantidade, credencial, codigo);
                vendas.Add($"{nome} - {quantidade} unidades - Vendido por: {credencial.ToUpper()} (Código: {codigo})");
                Console.WriteLine("Venda registrada com sucesso!");
            }
            catch (ProductException ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
            }
        }

        private static void MostrarStock()
        {
            Console.Clear();
            Console.WriteLine("Stock disponível:");
            foreach (var p in productService.ListProducts())
                Console.WriteLine($"{p.Nome} - Preço: {p.Preco:C} - Stock: {p.Stock}");
        }

        private static void AdicionarStock()
        {
            Console.Clear();
            if (!Permissions.Login(out string credencial, out string codigo))
            {
                Console.WriteLine("Login falhou.");
                return;
            }
            if (!Permissions.VerifyPermission(credencial, "gerente", "dono"))
            {
                Console.WriteLine("Acesso negado: Permissões insuficientes.");
                return;
            }

            Console.Write("Digite o nome do produto: ");
            string nome = Console.ReadLine();
            Console.Write("Digite a quantidade a adicionar: ");
            if (!int.TryParse(Console.ReadLine(), out int quantidade))
            {
                Console.WriteLine("Quantidade inválida.");
                return;
            }

            productService.AddStock(nome, quantidade);
            Console.WriteLine("Stock atualizado com sucesso!");
        }

        private static void VisualizarVendas()
        {
            Console.Clear();
            if (!Permissions.Login(out string credencial, out string codigo))
            {
                Console.WriteLine("Login falhou.");
                return;
            }
            if (!Permissions.VerifyPermission(credencial, "gerente", "dono"))
            {
                Console.WriteLine("Acesso negado: Permissões insuficientes.");
                return;
            }

            Console.WriteLine("Vendas realizadas:");
            if (vendas.Count == 0)
                Console.WriteLine("Nenhuma venda registrada.");
            else
                vendas.ForEach(Console.WriteLine);
        }
    }
}