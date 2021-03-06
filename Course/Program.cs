using System;
using System.Globalization;
using System.Collections.Generic;

namespace Course {
    class Program {
        static void Main(string[] args) {
            Console.WriteLine("Dados da Empresa");
            Console.WriteLine("");
            Console.Write("Razão Social: ");
            string razao = Console.ReadLine();
            Console.Write("CNPJ: ");
            string cnpj = Console.ReadLine();
            Empresa e = new Empresa(razao, cnpj);
            Console.WriteLine("");

            Console.Write("Quantos funcionário serão registrados? ");
            int n = int.Parse(Console.ReadLine());

            List<Funcionario> list = new List<Funcionario>();

            for (int i = 1; i <= n; i++) {
                Console.WriteLine("Funcionário #" + i + ":");
                Console.Write("Código: ");
                int codigo = int.Parse(Console.ReadLine());
                Console.Write("Nome: ");
                string nome = Console.ReadLine();
                Console.Write("Cargo: ");
                string cargo = Console.ReadLine();
                Console.Write("Salario Base: ");
                double salarioBase = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);
                list.Add(new Funcionario(codigo, nome, cargo, salarioBase));
                Console.WriteLine();
            }

            Console.WriteLine();
            Console.WriteLine("Emitir folha de pagamento dos funcionários:");
            Console.WriteLine();
            Console.Write("Mês de referência: ");
            int mesRef = int.Parse(Console.ReadLine());
            Console.Write("Ano de referência: ");
            int anoRef = int.Parse(Console.ReadLine());
            Console.WriteLine();

            Console.WriteLine(); foreach (Funcionario obj in list) {
                Console.Write($"Quantidade de faltas do funcionario {obj.Nome}: ");
                int faltas = int.Parse(Console.ReadLine());
                FolhaPagamento folha = new FolhaPagamento(e.RazaoSocial, e.Cnpj, mesRef, anoRef, obj.Codigo, obj.Nome, obj.Cargo, obj.SalarioBase);
                if (faltas>0) {
                    folha.CalculoFaltas(faltas);
                }

                Console.WriteLine();
                Console.WriteLine(folha);
            }
        }
    }
}
