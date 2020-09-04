using Newtonsoft.Json;

namespace WpfAppp
{
    class Estado
    {
        [JsonProperty("estadoSigla")]
        public string Sigla { get; set; }
    }
}
