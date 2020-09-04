using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cidades
{
    public interface IMunicipio
    {
        [Get("/{siglaEstado}/Municipios")]
        Task<List<Municipio>> GetMunicipioAsync(string siglaEstado);
    }
}
