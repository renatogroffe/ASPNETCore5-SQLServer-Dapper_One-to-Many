using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Dapper;
using Slapper;
using APIRegioes.Models;

namespace APIRegioes.Data
{
    public class RegioesRepository
    {
        public readonly IConfiguration _configuration;

        public RegioesRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IEnumerable<Regiao>> GetAll()
        {
            var conexao = new SqlConnection(
                _configuration.GetConnectionString("BaseDadosGeograficos"));

            var dados = await conexao.QueryAsync<dynamic>(
                "SELECT R.IdRegiao, " +
                        "R.NomeRegiao, " +
                        "E.SiglaEstado AS Estados_SiglaEstado, " +
                        "E.NomeEstado AS Estados_NomeEstado, " +
                        "E.NomeCapital AS Estados_NomeCapital " +
                "FROM dbo.Regioes R " +
                "INNER JOIN dbo.Estados E " +
                    "ON E.IdRegiao = R.IdRegiao " +
                "ORDER BY R.NomeRegiao, E.NomeEstado");

            AutoMapper.Configuration.AddIdentifier(
                typeof(Regiao), "IdRegiao");
            AutoMapper.Configuration.AddIdentifier(
                typeof(Estado), "SiglaEstado");

            var regioes = (AutoMapper.MapDynamic<Regiao>(dados)
                as IEnumerable<Regiao>).ToList();

            return regioes;
        }
    }
}