using Dapper;
using MauiPets.Core.Application.Interfaces.Repositories;
using MauiPets.Core.Application.ViewModels;
using MauiPetsApp.Core.Application.Interfaces.DapperContext;

namespace MauiPetsApp.Infrastructure.Repositories
{
    public class PetPhotoRepository : IPetPhotoRepository
    {
        private readonly IDapperContext _db;

        public PetPhotoRepository(IDapperContext db)
        {
            _db = db;
        }

        public async Task AddPhotoAsync(int petId, string filePath)
        {
            string sql = "INSERT INTO PetPhoto (PetId, PhotoPath, DateAdded) VALUES (@PetId, @Path, @DateAdded)";
            using var connection = _db.CreateConnection();
            await connection.ExecuteAsync(sql, new { PetId = petId, Path = filePath, DateAdded = DateTime.Now });
        }

        public async Task<List<PetPhoto>> GetPhotosAsync(int petId)
        {
            string sql = "SELECT * FROM PetPhoto WHERE PetId = @PetId ORDER BY DateAdded DESC";
            using var connection = _db.CreateConnection();
            var result = await connection.QueryAsync<PetPhoto>(sql, new { PetId = petId });
            return result.ToList();
        }
        public async Task DeletePhotoAsync(int photoId)
        {
            string sql = "DELETE FROM PetPhoto WHERE Id = @Id";
            using var connection = _db.CreateConnection();

            await connection.ExecuteAsync(sql, new { Id = photoId });
        }
    }
}
