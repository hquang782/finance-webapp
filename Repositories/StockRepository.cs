using Microsoft.EntityFrameworkCore;
using api.Data;
using api.Interfaces;
using api.Models;
using api.Dtos.Stock;
using api.Helpers;

namespace api.Repositories
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDBContext _context;

        public StockRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<Stock>> GetAllAsync(QueryObject queryObject)
        {
            var stocks = _context.Stocks.Include(c=>c.Comments).AsQueryable();
            if(!string.IsNullOrWhiteSpace(queryObject.CompanyName)){
                stocks = stocks.Where(s=>s.CompanyName.Contains(queryObject.CompanyName));
            }
            if(!string.IsNullOrWhiteSpace(queryObject.Symbol)){
                stocks = stocks.Where(s=> s.Symbol.Contains(queryObject.Symbol));
            }
            if(!string.IsNullOrWhiteSpace(queryObject.SortBy)){
                if(queryObject.SortBy.Equals("Symbol",StringComparison.OrdinalIgnoreCase)){
                    stocks = queryObject.IsDecsending?stocks.OrderByDescending(s=>s.Symbol):stocks.OrderBy(s=>s.Symbol);
                }
            }

            var skipNumber = (queryObject.PageNumber-1)*queryObject.PageSize;

            return await stocks.Skip(skipNumber).Take(queryObject.PageSize).ToListAsync();
        }

        public async Task<Stock?> GetByIdAsync(int id)
        {
            return await _context.Stocks.Include(c=>c.Comments).FirstOrDefaultAsync(i => i.Id==id);
        }
        public async Task<Stock> CreateAsync(Stock stock)
        {
            await _context.Stocks.AddAsync(stock);
            await _context.SaveChangesAsync();
            return stock;
        }

        public async Task<Stock?> UpdateAsync(int Id, UpdateStockRequestDto updateStockRequestDto){
            var existingStock = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == Id); 
            if (existingStock == null){
               return null;
            }
            existingStock.Symbol = updateStockRequestDto.Symbol;
            existingStock.CompanyName = updateStockRequestDto.CompanyName;
            existingStock.Purchase = updateStockRequestDto.Purchase;
            existingStock.LastDiv = updateStockRequestDto.LastDiv;
            existingStock.Industry = updateStockRequestDto.Industry;
            existingStock.MarketCap = updateStockRequestDto.MarketCap;

            await _context.SaveChangesAsync();
            return existingStock;
        }
        public async Task<Stock?> DeleteAsync(int Id){
            var stockModel = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == Id);    
            if (stockModel == null){
                return null;    
            }
            _context.Stocks.Remove(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
        }

        public Task<bool> StockExists(int Id)
        {
           return _context.Stocks.AnyAsync(x => x.Id == Id);
        }
    }
}
