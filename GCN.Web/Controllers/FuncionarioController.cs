using GCN.Aplicacao.GestaoDeFuncionario.Modelos;
using GCN.Aplicacao.GestaoDeFuncionarios;
using GCN.Dominio.ObjetosDeValor;
using GCN.Web.CustomExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GCN.Web.Controllers
{
    public class FuncionarioController : Controller
    {
        private readonly IServicoDeGestaoDeFuncionarios _servicoDeGestaoDeFuncionarios;
        public FuncionarioController(IServicoDeGestaoDeFuncionarios servicoDeGestaoDeFuncionarios)
        {
            this._servicoDeGestaoDeFuncionarios = servicoDeGestaoDeFuncionarios;
        }

        public ActionResult Index()
        {

            var usuario = User.Logado();

           
            var modelo = new ModeloDeCadastroDeFuncionario(){ Nome = "Cliente Novo", Email = "guilherme@gmail.com", Senha = "12312312",
                Documento = "02025032161", Telefone = "12312321", Celular = "12312312",
                PerfilDeFuncionario = PerfilDeFuncionario.AdministradorGeral };

            this._servicoDeGestaoDeFuncionarios.CadastrarNovoFuncionario(modelo);
            return View();
        }
    }
}