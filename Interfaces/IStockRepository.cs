using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using api.Dtos.Stock;
using api.Helpers;
namespace api.Interfaces
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllAsync(QueryObject queryObject);
        Task<Stock?> GetByIdAsync(int id);
        Task<Stock> CreateAsync(Stock stock);
        Task<Stock?> UpdateAsync(int Id,UpdateStockRequestDto updateStockRequestDto);
        Task<Stock?> DeleteAsync(int Id);
        Task<bool> StockExists(int Id);
    }
}