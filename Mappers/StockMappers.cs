using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using api.Dtos.Stock;
namespace api.Mappers
{
    public static class StockMappers
    {
         public static StockDto ToStockDto(this Stock stockModel)
        {
            return new StockDto
            {
                Id = stockModel.Id,
                Symbol = stockModel.Symbol,
                CompanyName = stockModel.CompanyName,
                Purchase = stockModel.Purchase,
                LastDiv = stockModel.LastDiv,
                Industry = stockModel.Industry,
                MarketCap = stockModel.MarketCap,
                Comments = stockModel.Comments.Select(x => x.ToCommentDto()).ToList(),
            };
        }
        public static Stock ToStockFromCreateDTO(this CreateStockRequestDto createStockRequestDto){
            return new Stock{
                Symbol = createStockRequestDto.Symbol,
                CompanyName = createStockRequestDto.CompanyName,    
                Purchase = createStockRequestDto.Purchase,
                LastDiv =   createStockRequestDto.LastDiv,
                Industry = createStockRequestDto.Industry,
                MarketCap = createStockRequestDto.MarketCap,
            };
        }
    }
}