using Newtonsoft.Json;

namespace Cidades
{
    public class Estado
    {
        [JsonProperty("id")]
        public int EstadoId { get; set; }

        [JsonProperty("sigla")]
        public string EstadoSigla { get; set; }

        [JsonProperty("nome")]
        public string EstadoNome { get; set; }

        public Regiao Regiao { get; set; }
    }
}
