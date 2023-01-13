using System.Data.SqlClient;
using Dapper;
using ITS.Spironelli.Verifica.Web.Interfaces;
using ITS.Spironelli.Verifica.Web.Models;

namespace ITS.Spironelli.Verifica.Web.Repository;

public class PanelRepository : IPanelRepository
{
    private readonly string _connString;

    public PanelRepository(IConfiguration configuration)
    {
        _connString = configuration.GetConnectionString("db") ?? throw  new InvalidOperationException("No connection string found");
    }
    public async Task<Panel?> GetById(int id)
    {
        using var connection = new SqlConnection(_connString);

        const string query = """  
        SELECT [id]
              ,[deviceId]
              ,[description]  
           FROM [dbo].[Spironelli.Esame] 
           WHERE Id = @id 
        """;



        return await connection.QuerySingleOrDefaultAsync<Panel>(query, new { id });
    }
}