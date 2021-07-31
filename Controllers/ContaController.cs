using Bufunfa.Models;
using Bufunfa.Repositories;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Bufunfa.Controllers
{
    [ApiController]
    [Route("conta")]
    public class ContaController: ControllerBase
    {

        [HttpPost]
        [Route("cadastrar")]
        public IActionResult Cadastrar([FromBody] Conta conta, [FromServices] IContaRepository repository)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            try
            {
            return Ok(repository.Cadastrar(conta));
            }
            catch
            {
                return StatusCode(500, "Falha interna do servidor");
            }
        }

        [HttpPut]
        [Route("atualizar")]
        public IActionResult AtualizarConta([FromBody] Conta conta, [FromServices] IContaRepository repository)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            try
            {
                return Ok(repository.Atualizar(conta));
            }
            catch
            {
                return StatusCode(500, "Falha interna do servidor");
            }
        }

        [HttpDelete]
        [Route("delete")]
        public IActionResult DeletarConta([FromBody] int conta, [FromServices] IContaRepository repository)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            try
            {
                repository.Delete(conta);
                return Ok("Conta Apagada");
            }
            catch
            {
                return StatusCode(500, "Falha interna do servidor");
            }
        }

        [HttpPost]
        [Route("consultar")]
        public IActionResult ConsultarConta([FromBody] int conta, [FromServices] IContaRepository repository)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            try
            {
                  return Ok(repository.Consulta(conta));
            }
            catch
            {
                return StatusCode(500, "Falha interna do servidor");
            }
        }

        [HttpPost]
        [Route("saldo")]
        public IActionResult Saldo([FromBody] int numeroConta, [FromServices] IContaRepository repository)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                var saldo = repository.Saldo(numeroConta).ToString("C", CultureInfo.CurrentCulture);
                return Ok($"Saldo = {saldo}");
            }
            catch
            {
                return StatusCode(500, "Falha interna do servidor");
            }
        }

        [HttpPost]
        [Route("depositar")]
        public IActionResult Depositar([FromBody] ContaValor deposito, [FromServices] IContaRepository repository)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                var saldoAtual = repository.Depositar(deposito.valor, deposito.conta).ToString("C", CultureInfo.CurrentCulture);
                return Ok($"Novo saldo = {saldoAtual}");
            }
            catch
            {
                return StatusCode(500, "Falha interna do servidor");
            }
        }

        [HttpPost]
        [Route("trasferir")]
        public IActionResult Transferir([FromBody] Transferir transferir, [FromServices] IContaRepository repository)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                var saldo = repository.Trasferir(transferir.valor, transferir.contaDestino, transferir.contaOrigem).ToString("C", CultureInfo.CurrentCulture);
                return Ok($"Novo saldo = {saldo}");
            }
            catch (SaldoInsuficienteException e)
            {
                return BadRequest(e.Message);
            }
            catch
            {
                return StatusCode(500, "Falha interna do servidor");
            }
        }

        [HttpPost]
        [Route("sacar")]
        public IActionResult Sacar([FromBody] ContaValor saque, [FromServices] IContaRepository repository)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                var saldo = repository.Sacar(saque.valor, saque.conta).ToString("C", CultureInfo.CurrentCulture);
                return Ok($"Novo saldo = {saldo}");
            }
            catch (SaldoInsuficienteException e)
            {
                return BadRequest(e.Message);
            }
            catch
            {
                return StatusCode(500, "Falha interna do servidor");
            }
        }


    }
}
