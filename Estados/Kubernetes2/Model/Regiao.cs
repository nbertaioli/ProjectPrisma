using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cidades
{
    public class Regiao
    {
        [JsonProperty("id")]
        public int RegiaoId { get; set; }

        [JsonProperty("sigla")]
        public string RegiaoSigla { get; set; }

        [JsonProperty("nome")]
        public string RegiaoNome { get; set; }
    }
}
