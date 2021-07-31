using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Bufunfa.Models
{
    public class Conta
    {
        [ScaffoldColumn(false)]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int NumeroConta { get; set; }
        [Required]
        public string Titular { get; set; }
        [Required]
        public double Saldo { get; set; }
    }

    public class ContaValor
    {
        public int conta { get; set; }
        public int valor { get; set; }
    }

    public class Transferir
    {
        public int contaDestino { get; set; }
        public int contaOrigem { get; set; }
        public int valor { get; set; }
    }
}
