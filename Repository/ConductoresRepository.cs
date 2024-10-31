using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using APISERVI.Models;
using APISERVI.Repository.Application;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APISERVI.Repository
{
    public class ConductoresRepository : IConductoresRepository
    {
        private readonly string _connectionString;

        public ConductoresRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("Sqlite");
        }

        public async Task<int> AddAsync(Conductor entity)
        {
            var sql = "INSERT INTO Conductores (Nombre, Apellido, Cedula, FechaLicencia, TarifaPorServicio) VALUES (@Nombre, @Apellido, @Cedula, @FechaLicencia, @TarifaPorServicio)";
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                return await connection.ExecuteAsync(sql, entity);
            }
        }

        public async Task<int> DeleteAsync(int id)
        {
            var sql = "DELETE FROM Conductores WHERE IdConductor = @Id";
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                return await connection.ExecuteAsync(sql, new { Id = id });
            }
        }

        public async Task<IEnumerable<Conductor>> GetAllAsync()
        {
            var sql = "SELECT * FROM Conductores";
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                return await connection.QueryAsync<Conductor>(sql);
            }
        }

        public async Task<Conductor> GetByIdAsync(int id)
        {
            var sql = "SELECT * FROM Conductores WHERE IdConductor = @Id";
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                return await connection.QuerySingleOrDefaultAsync<Conductor>(sql, new { Id = id });
            }
        }

        public async Task<int> UpdateAsync(Conductor entity)
        {
            var sql = "UPDATE Conductores SET Nombre = @Nombre, Apellido = @Apellido, Cedula = @Cedula, FechaLicencia = @FechaLicencia, TarifaPorServicio = @TarifaPorServicio WHERE IdConductor = @IdConductor";
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                return await connection.ExecuteAsync(sql, entity);
            }
        }
    }
}
