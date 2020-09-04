using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Refit;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Cidades.Controller
{
    public class MunicipioController : ApiController
    {
        const string urlApi = "https://servicodados.ibge.gov.br/api/v1/localidades/estados/";

        [HttpGet("{UF}")]
        public async Task<List<Municipio>> GetMunicipiosAsync(string siglaEstado)
        {
            var procMunicipio = RestService.For<IMunicipio>(urlApi);
            JsonSerializer jsonData = new JsonSerializer();

            List<Municipio> listMunicipio = new List<Municipio>();
            Console.WriteLine("Iniciando busca de dados do estado sigla " + siglaEstado);
            var Json = await procMunicipio.GetMunicipioAsync(siglaEstado);
            foreach (var item in Json)
            {
                Municipio municipio = new Municipio
                {
                    MunicipioId = item.MunicipioId,
                    MunicipioNome = item.MunicipioNome
                };

                Microrregiao microrregiao = new Microrregiao
                {
                    MicroregiaoId = item.Microrregiao.MicroregiaoId,
                    MicroregiaoNome = item.Microrregiao.MicroregiaoNome
                };

                Mesorregiao mesorregiao = new Mesorregiao
                {
                    MesorregiaoId = item.Microrregiao.Mesorregiao.MesorregiaoId,
                    MesorregiaoNome = item.Microrregiao.Mesorregiao.MesorregiaoNome
                };

                Estado estado = new Estado
                {
                    EstadoId = item.Microrregiao.Mesorregiao.UF.EstadoId,
                    EstadoNome = item.Microrregiao.Mesorregiao.UF.EstadoNome,
                    EstadoSigla = item.Microrregiao.Mesorregiao.UF.EstadoSigla
                };

                Regiao regiao = new Regiao
                {
                    RegiaoId = item.Microrregiao.Mesorregiao.UF.Regiao.RegiaoId,
                    RegiaoNome = item.Microrregiao.Mesorregiao.UF.Regiao.RegiaoNome,
                    RegiaoSigla = item.Microrregiao.Mesorregiao.UF.Regiao.RegiaoSigla
                };

                municipio.Microrregiao = microrregiao;
                municipio.Microrregiao.Mesorregiao = mesorregiao;
                municipio.Microrregiao.Mesorregiao.UF = estado;
                municipio.Microrregiao.Mesorregiao.UF.Regiao = regiao;

                listMunicipio.Add(municipio);
            }
            return listMunicipio;
        }
    }
}
