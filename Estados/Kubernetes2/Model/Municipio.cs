using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cidades
{
    public class Municipio
    {
        [JsonProperty("id")]
        public int MunicipioId { get; set; }

        [JsonProperty("nome")]
        public string MunicipioNome { get; set; }

        public Microrregiao Microrregiao { get; set; }
    }
}
