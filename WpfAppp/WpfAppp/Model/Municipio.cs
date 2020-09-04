using Newtonsoft.Json;
using WpfAppp.Model;

namespace WpfAppp
{
    class Municipio
    {
        [JsonProperty("MunicipioId")]
        public int MunicipioId { get; set; }

        [JsonProperty("MunicipioNome")]
        public string MunicipioNome { get; set; }

        public Microrregiao Microrregiao { get; set; }
    }
}
