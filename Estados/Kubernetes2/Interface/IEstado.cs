using Cidades;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kubernetes2.Interface
{
    public interface IEstado
    {
        [Get("/estados")]
        Task<List<Estado>> GetEstadosAsync();
    }
}
