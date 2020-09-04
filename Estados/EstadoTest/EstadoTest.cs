using Cidades;
using Cidades.Controller;
using Kubernetes2;
using Kubernetes2.Controllers;
using Kubernetes2.Interface;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using Refit;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using Assert = NUnit.Framework.Assert;

namespace EstadoTest
{
    public class Tests
    {
        private readonly EstadoController _controllerEstado;
        private readonly MunicipioController _controllerMunicipio;

        public Tests()
        {
            _controllerEstado = new EstadoController();
            _controllerMunicipio = new MunicipioController();
        }

        [Fact]
        public void HealthCheckGetAsync_ShouldValuesEstado()
        {
            int expected = 27;

            var response = _controllerEstado.GetEstadoAsync();

            Assert.Equals(response.Result.Count(), expected);
        }

        [Fact]
        public void HealthCheckGetAsync_ShouldValuesMunicipio()
        {
            int expected = 399;

            var response = _controllerMunicipio.GetMunicipiosAsync("PR");

            Assert.Equals(response.Result.Count(), expected);
        }
    }
}