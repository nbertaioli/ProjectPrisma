using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppp
{
    interface IMunicipio
    {
        [Get("/Municipios?siglaEstado={UF}")]
        Task<List<Municipio>> GetMunicipioAsync(string UF);
    }
}
