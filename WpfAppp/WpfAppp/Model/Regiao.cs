using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WpfAppp.Model
{
    class Regiao
    {
        [JsonProperty("RegiaoSigla")]
        public string RegiaoSigla { get; set; }

        [JsonProperty("RegiaoNome")]
        public string RegiaoNome { get; set; }
    }
}
