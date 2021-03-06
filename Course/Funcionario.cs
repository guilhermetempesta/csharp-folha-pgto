using System.Globalization;

namespace Course {
    class Funcionario {

        public int Codigo { get; set; }
        public string Nome { get; set; }
        public string Cargo { get; set; }
        public double SalarioBase { get; private set; }

        public Funcionario(int codigo, string nome, string cargo, double salarioBase) {
            this.Codigo = codigo;
            this.Nome = nome;
            this.Cargo = cargo;
            this.SalarioBase = salarioBase;
        }
        
        public override string ToString() {
            return Codigo
                + ", "
                + Nome
                + ", "
                + Cargo
                + ", "
                + SalarioBase.ToString("F2", CultureInfo.InvariantCulture);
        }
    }
}
