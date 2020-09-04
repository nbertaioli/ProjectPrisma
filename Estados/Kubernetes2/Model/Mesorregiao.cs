using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cidades
{
    public class Mesorregiao
    {
        [JsonProperty("id")]
        public int MesorregiaoId { get; set; }

        [JsonProperty("nome")]
        public string MesorregiaoNome { get; set; }

        public Estado UF { get; set; }
    }
}
