using System;

namespace Course {
    class Empresa {

        public string RazaoSocial { get; set; }
        public string Cnpj { get; set; }
        
        public Empresa(string razaoSocial, string cnpj) {
            RazaoSocial = razaoSocial;
            Cnpj = cnpj;
        }
        
        public override string ToString() {
            return 
                RazaoSocial
                + "\r\n"
                + "CNPJ: " 
                + Cnpj
                ;
        }
    }
}
