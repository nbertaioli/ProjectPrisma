using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WpfAppp.Model
{
    class Mesorregiao
    {
        [JsonProperty("MesorregiaoNome")]
        public string MesorregiaoNome { get; set; }

        public Uf Uf { get; set; }
    }
}
