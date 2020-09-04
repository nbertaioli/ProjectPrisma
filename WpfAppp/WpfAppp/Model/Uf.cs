using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WpfAppp.Model
{
    class Uf
    {
        [JsonProperty("estadoSigla")]
        public string Sigla { get; set; }
        public Regiao Regiao { get; set; }
    }
}
