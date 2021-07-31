using Bufunfa.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Bufunfa.Repositories
{
    public interface IContaRepository
    {
        int Cadastrar(Conta conta);
        Conta Atualizar(Conta conta);
        void Delete(int conta);
        Conta Consulta(int conta);
        double Depositar(double valor, int conta);
        double Trasferir(double valor, int contaRecebedora, int contaOrigem);
        double Sacar(double valor, int conta);
        double Saldo(int conta);
    }

    public class ContaRepository : IContaRepository
    {
        private readonly DataContext _context;

        public ContaRepository(DataContext context)
        {
            _context = context;
        }

        public Conta Atualizar(Conta conta)
        {
            var _conta = _context.Conta.Find(conta.NumeroConta);
            _conta.Titular = conta.Titular;

            _context.Entry(_conta).State = EntityState.Modified;
            _context.SaveChanges();
            return _conta;
        }

        public int Cadastrar(Conta conta)
        {
            _context.Conta.Add(conta);
            _context.SaveChanges();
            return conta.NumeroConta;
        }

        public Conta Consulta(int conta)
        {
            return _context.Conta.Find(conta);
        }

        public void Delete(int conta)
        {
            var _conta = _context.Conta.Find(conta);
            _context.Conta.Remove(_conta);
            _context.SaveChanges();
        }

        public double Depositar(double valor, int conta)
        {
            Console.WriteLine($"{valor} {conta}");
            var _conta = _context.Conta.Find(conta);
            _conta.Saldo = _conta.Saldo + valor;
            _context.Entry(_conta).State = EntityState.Modified;
            _context.SaveChanges();
            return _conta.Saldo;
        }

        public double Sacar(double valor, int conta)
        {
            var _conta = _context.Conta.Find(conta);

            if (_conta.Saldo > valor)
            {
                _conta.Saldo = _conta.Saldo - valor;
                _context.Entry(_conta).State = EntityState.Modified;
                _context.SaveChanges();
                return _conta.Saldo;
            }
            else throw new SaldoInsuficienteException("Saldo Insuficiente.");
        }

        public double Saldo(int conta)
        {
            return _context.Conta.Find(conta).Saldo;
        }

        public double Trasferir(double valor, int contaRecebedora, int contaOrigem)
        {
            var _contaRec = _context.Conta.Find(contaRecebedora);
            var _contaOri = _context.Conta.Find(contaOrigem);

            if (_contaOri.Saldo > valor)
            {
                _contaRec.Saldo = _contaRec.Saldo + valor;
                _contaOri.Saldo = _contaOri.Saldo - valor;

                _context.Entry(_contaOri).State = EntityState.Modified;
                _context.Entry(_contaRec).State = EntityState.Modified;

                _context.SaveChanges();
                return _contaOri.Saldo;
            }
            else throw new SaldoInsuficienteException("Saldo Insuficiente.");
        }
    }
}
