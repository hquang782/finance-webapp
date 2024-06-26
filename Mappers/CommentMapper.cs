using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Comment;
using api.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace api.Mappers
{
    public static class CommentMapper
    {

        public static CommentDto ToCommentDto(this Comment commentModel)
        {
            return new CommentDto
            {
                Id = commentModel.Id,
                Title = commentModel.Title,
                Content = commentModel.Content,
                CreatedOn = commentModel.CreatedOn,
                StockId = commentModel.StockId,
            };
        }
        public static Comment ToCommentFromCreateDTO(this CreateCommentRequestDto createCommentRequestDto,int stockId)
        {
            return new Comment
            {
                Title = createCommentRequestDto.Title,
                Content = createCommentRequestDto.Content,
                StockId = stockId,
            };
        }
         public static Comment ToCommentFromUpdateDTO(this UpdateCommentRequestDto updateCommentRequestDto)
        {
            return new Comment
            {
                Title = updateCommentRequestDto.Title,
                Content = updateCommentRequestDto.Content,
            };
        }
    }
}