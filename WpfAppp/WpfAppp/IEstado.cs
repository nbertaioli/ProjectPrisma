using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WpfAppp
{
    interface IEstado
    {
        [Get("/estados")]
        Task<List<Estado>> GetEstadoAsync();
    }
}
