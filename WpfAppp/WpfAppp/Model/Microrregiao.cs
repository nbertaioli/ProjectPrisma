using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace WpfAppp.Model
{
    class Microrregiao
    {
        [JsonProperty("MicroregiaoNome")]
        public string MicroregiaoNome { get; set; }

        public Mesorregiao Mesorregiao { get; set; }
    }
}
