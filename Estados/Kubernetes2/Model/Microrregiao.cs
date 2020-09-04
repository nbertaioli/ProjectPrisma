using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cidades
{
    public class Microrregiao
    {
        [JsonProperty("id")]
        public int MicroregiaoId { get; set; }

        [JsonProperty("nome")]
        public string MicroregiaoNome { get; set; }

        public Mesorregiao Mesorregiao { get; set; }
    }
}
