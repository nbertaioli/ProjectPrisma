using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using Kubernetes2.Interface;
using Cidades;
using Newtonsoft.Json;
using Refit;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using Kubernetes2.App_Start;
using System.Data.SqlClient;
using System;
using Microsoft.SqlServer.Server;
using Microsoft.Extensions.Logging;

namespace Kubernetes2.Controllers
{
    public class EstadoController : ApiController
    {
        const string urlApi = "https://servicodados.ibge.gov.br/api/v1/localidades/";

        [HttpGet("/estados")]
        public async Task<List<Estado>> GetEstadoAsync()
        {
            var procEstado = RestService.For<IEstado>(urlApi);
            JsonSerializer jsonData = new JsonSerializer();

            List<Estado> listEstado = new List<Estado>();
            Console.WriteLine("Realizando busca de estados.");
            var Json = await procEstado.GetEstadosAsync();
            Console.WriteLine("Busca efetuada com sucesso, realizando geração de lista.");
            foreach (var est in Json)
            {
                Estado estado = new Estado();
                estado.EstadoId = est.EstadoId;
                estado.EstadoNome = est.EstadoNome;
                estado.EstadoSigla = est.EstadoSigla;

                Regiao regiao = new Regiao();
                regiao.RegiaoId = est.Regiao.RegiaoId;
                regiao.RegiaoNome = est.Regiao.RegiaoNome;
                regiao.RegiaoSigla = est.Regiao.RegiaoSigla;

                estado.Regiao = regiao;

                listEstado.Add(estado);
            }
            Console.WriteLine("Finalizando geração da lista. Iniciando Gravação em banco de dados.");
            GravaRequisicao();

            return listEstado;
        }

        public void GravaRequisicao()
        {
            Connection connection = new Connection();
            SqlCommand sqlCom = new SqlCommand();

            sqlCom.CommandText = "INSERT INTO Requisicao (Data) VALUES (@data)";
            sqlCom.Parameters.AddWithValue("@data", DateTime.Now.ToString());

            sqlCom.Connection = connection.Disconnect();
            sqlCom.Connection = connection.Connect();

            sqlCom.ExecuteNonQuery();
            Console.WriteLine("Gravação da requisição efetuada com sucesso.");

        }
    }
}
