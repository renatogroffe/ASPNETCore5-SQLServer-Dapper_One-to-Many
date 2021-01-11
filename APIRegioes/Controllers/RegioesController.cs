using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using APIRegioes.Data;
using APIRegioes.Models;

namespace APIRegioes.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegioesController : ControllerBase
    {
        private readonly ILogger<RegioesController> _logger;
        private readonly RegioesRepository _repository;

        public RegioesController(ILogger<RegioesController> logger,
            RegioesRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet]
        public async Task<IEnumerable<Regiao>> GetAll()
        {
            var dados = await _repository.GetAll();
            _logger.LogInformation(
                $"{nameof(GetAll)}: {dados.Count()} registro(s) encontrado(s)");
            return dados;
        }
    }
}