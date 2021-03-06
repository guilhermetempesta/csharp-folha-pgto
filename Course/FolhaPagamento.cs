using System;
using System.Globalization;

namespace Course {
    class FolhaPagamento {
        string RazaoSocial { get; set; }
        string CNPJ { get; set; }
        int Mes { get; set; }
        int Ano { get; set; }
        int Codigo { get; set; }
        string NomeFuncionario { get; set; }
        string Cargo { get; set; } 
        double SalarioBase { get; set; }
        double DescontoFaltas { get; set; }
        double FgtsMensal { get; set; }
        double DescontoInss { get; set; }
        double BaseIrrf { get; set; }
        double FaixaIrrf { get; set; }
        double DescontoIrrf { get; set; }
        
        public FolhaPagamento(string razaoSocial, string cnpj, int mes, int ano, int codigo, string nome, string cargo, double salarioBase) {
            this.RazaoSocial = razaoSocial;
            this.CNPJ = cnpj;
            this.Mes = mes;
            this.Ano = ano;
            this.Codigo = codigo;
            this.NomeFuncionario = nome;
            this.Cargo = cargo;
            this.SalarioBase = salarioBase;
            this.CalculoINSS();
            this.CalculoIRRF();
            this.CalculoFGTS();
        }

        private string MesAno() {
            if (this.Mes == 1) {
                return "Janeiro de " + this.Ano.ToString();
            } else if (this.Mes == 2) {
                return "Fevereiro de " + this.Ano.ToString();
            } else if (this.Mes == 3) {
                return "Março de " + this.Ano.ToString();
            } else if (this.Mes == 4) {
                return "Abril de " + this.Ano.ToString();
            } else if (this.Mes == 5) {
                return "Maio de " + this.Ano.ToString();
            } else if (this.Mes == 6) {
                return "Junho de " + this.Ano.ToString();
            } else if (this.Mes == 7) {
                return "Julho de " + this.Ano.ToString();
            } else if (this.Mes == 8) {
                return "Agosto de " + this.Ano.ToString();
            } else if (this.Mes == 9) {
                return "Setembro de " + this.Ano.ToString();
            } else if (this.Mes == 10) {
                return "Outubro de " + this.Ano.ToString();
            } else if (this.Mes == 11) {
                return "Novembro de " + this.Ano.ToString();
            } else {
                return "Dezembro de " + this.Ano.ToString();
            }
        }

        private void CalculoFGTS() {
            this.FgtsMensal = this.SalarioBase * 0.08;
        }

        private void CalculoINSS() {
            // até 1.100,00 ...............   7,5 %
            // de  1.100,01 até 2.203,48...     9 %
            // de  2.203,49 até 3.305,22...    12 %
            // de  3.305,23 até 6.433,57...    14 %

            if (this.SalarioBase < 1100) {
                throw new Exception("Salário inválido!"); 
            }
            else if (this.SalarioBase == 1100) {
                double f1 = Math.Round(this.SalarioBase * 0.075, 2);
                this.DescontoInss = f1;
            } else if (this.SalarioBase > 1100 && this.SalarioBase <= 2203.48) {
                double f1 = Math.Round(1100 * 0.075, 2);
                double f2 = Math.Round((this.SalarioBase - 1100) * 0.09, 2);
                this.DescontoInss = (f1 + f2);       
            } else if (this.SalarioBase > 2203.48 && this.SalarioBase <= 3305.22) {
                double f1 = Math.Round(1100 * 0.075, 2);
                double f2 = Math.Round((2203.48 - 1100) * 0.09, 2);
                double f3 = Math.Round((this.SalarioBase - 2203.48) * 0.12, 2);
                this.DescontoInss = (f1 + f2 + f3);
            } else if (this.SalarioBase > 3305.22 && this.SalarioBase <= 6433.57){
                double f1 = Math.Round(1100 * 0.075, 2);
                double f2 = Math.Round((2203.48 - 1100) * 0.09, 2);
                double f3 = Math.Round((3305.22 - 2203.48) * 0.12, 2);
                double f4 = Math.Round((this.SalarioBase - 3305.22) * 0.14, 2);
                this.DescontoInss = (f1 + f2 + f3 + f4);
            } else {
                double f1 = Math.Round(1100 * 0.075, 2);
                double f2 = Math.Round((2203.48 - 1100) * 0.09, 2);
                double f3 = Math.Round((3305.22 - 2203.48) * 0.12, 2);
                double f4 = Math.Round((6433.57 - 3305.22) * 0.14, 2);
                this.DescontoInss = (f1 + f2 + f3 + f4);
            }
        }

        private void CalculoIRRF() {
            // até 1.903,98 ...............  0.0 % ...   0.00
            // de  1.903,99 até 2.826,65...  7.5 % ... 142.80
            // de  2.826,65 até 3.751,05... 15.0 % ... 354.80 
            // de  3.751,06 até 4.664,68... 22.5 % ... 636.13
            // acima de 4.664,68........... 27.5 % ... 869.36

            this.BaseIrrf = this.SalarioBase - this.DescontoInss;
            double deducaoIRRF;

            if (this.BaseIrrf <= 1903.98) {
                this.FaixaIrrf = 0.00;
                deducaoIRRF = 0.00;
            } else if (this.BaseIrrf > 1903.98 && this.BaseIrrf <= 2826.65) {
                this.FaixaIrrf = 0.075;
                deducaoIRRF = 142.80;
            } else if (this.BaseIrrf > 2826.65 && this.BaseIrrf <= 3751.05) {
                this.FaixaIrrf = 0.15;
                deducaoIRRF = 354.80;
            } else if (this.BaseIrrf > 3751.05 && this.BaseIrrf <= 4664.68) {
                this.FaixaIrrf = 0.225;
                deducaoIRRF = 636.13; 
            } else {
                this.FaixaIrrf = 0.275;
                deducaoIRRF = 869.36;
            }
            this.DescontoIrrf = (this.BaseIrrf * this.FaixaIrrf) - deducaoIRRF;
        }
        
        public void CalculoFaltas(int faltas) {
            double salarioDia = this.SalarioBase / 30;
            this.DescontoFaltas = faltas * salarioDia;
        }

        private double TotalDescontos() {
            return this.DescontoFaltas + this.DescontoInss + this.DescontoIrrf;
        }

        private double ValorLiquido() {
            return (this.SalarioBase - this.TotalDescontos());
        }

        public override string ToString() { 
            return
                "---------------------------------------------------------------------------------------------" + "\r\n"
                + "Empresa: " + RazaoSocial + "\r\n"
                + "CNPJ: " + CNPJ + "                                                        Folha Mensal " + "\r\n"
                + "                                                                          " + MesAno() + "\r\n"
                + "---------------------------------------------------------------------------------------------" + "\r\n"
                + "Código Nome do Funcionário" + "\r\n"
                + "---------------------------------------------------------------------------------------------" + "\r\n"
                + Codigo.ToString().PadLeft(6, '0') + " " + NomeFuncionario + "\r\n"
                + "Cargo: " + Cargo + "\r\n"
                + "---------------------------------------------------------------------------------------------" + "\r\n"
                + "Código | Descrição                                           | Vencimentos   | Descontos     " + "\r\n"
                + "---------------------------------------------------------------------------------------------" + "\r\n"
                + "     1 | Horas Normais                                       |  " + SalarioBase.ToString("F2", CultureInfo.InvariantCulture) + "      |              " + "\r\n"
                + "   998 | I.N.S.S                                             |               |   " + DescontoInss.ToString("F2", CultureInfo.InvariantCulture) + "\r\n"
                + "   999 | Imposto de Renda                                    |               |   " + DescontoIrrf.ToString("F2", CultureInfo.InvariantCulture) + "\r\n"
                + "   000 | Faltas                                              |               |   " + DescontoFaltas.ToString("F2", CultureInfo.InvariantCulture) + "\r\n"
                + "---------------------------------------------------------------------------------------------" + "\r\n"
                + "                                                             | Total Venc.   | Total Desc.   " + "\r\n"
                + "                                                             |  " + SalarioBase.ToString("F2", CultureInfo.InvariantCulture) + "      |   " + DescontoIrrf.ToString("F2", CultureInfo.InvariantCulture) + "\r\n"
                + "---------------------------------------------------------------------------------------------" + "\r\n"
                + "                                                             | Valor Liq. -> |   " + ValorLiquido().ToString("F2", CultureInfo.InvariantCulture) + "\r\n"
                + "---------------------------------------------------------------------------------------------" + "\r\n"
                + " Salário Base      Base INSS      Base FGTS      FGTS mês      Base IRRF    Faixa IRRF" + "\r\n"
                + "  " + SalarioBase.ToString("F2", CultureInfo.InvariantCulture) 
                + "           " + SalarioBase.ToString("F2", CultureInfo.InvariantCulture) 
                + "        " + SalarioBase.ToString("F2", CultureInfo.InvariantCulture) 
                + "        " + FgtsMensal.ToString("F2", CultureInfo.InvariantCulture) 
                + "        " + BaseIrrf.ToString("F2", CultureInfo.InvariantCulture) 
                + "        " + FaixaIrrf.ToString("F3", CultureInfo.InvariantCulture) + "\r\n"
                + "---------------------------------------------------------------------------------------------"
                ;
        }
    }
}
