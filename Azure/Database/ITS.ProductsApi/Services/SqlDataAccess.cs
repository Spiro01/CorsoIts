using Dapper;
using ITS.ProductsApi.Models;
using Microsoft.Data.SqlClient;

namespace ITS.ProductsApi.Services;

public class SqlDataAccess : IDataAccess
{
    private readonly string _connectionString;

    public SqlDataAccess(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("db") ?? throw new NullReferenceException("Connection string missing");
    }
    public async Task<IEnumerable<Products>> GetProductsAsync()
    {
        const string query = """ 
        SELECT[Id]
            ,[Name] 
            ,[Description]
            ,[Price]
        FROM[dbo].[Products]  
        """;
        using var connection = new SqlConnection(_connectionString);

        return await connection.QueryAsync<Products>(query);

    }

    public async Task<Products> GetProductAsync(int id)
    {
        const string query = """ 
        SELECT[Id]
            ,[Name] 
            ,[Description]
            ,[Price]
        FROM[dbo].[Products]  
        WHERE Id = @Id
        """;
        using var connection = new SqlConnection(_connectionString);

        return await connection.QueryFirstOrDefaultAsync<Products>(query, new { id });
    }

    public async Task InsertProductAsync(Products product)
    {
        const string query = """ 
            INSERT INTO [dbo].[Products]
                       ([Name]
                       ,[Description]
                       ,[Price])
                 VALUES
                       (@Name
                       ,@Description,
                       @Price )
            """;
        using var connection = new SqlConnection(_connectionString);
        await connection.ExecuteAsync(query, product);
    }
}