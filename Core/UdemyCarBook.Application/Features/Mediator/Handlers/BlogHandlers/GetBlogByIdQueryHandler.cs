using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyCarBook.Application.Features.Mediator.Queries.BlogQueries;
using UdemyCarBook.Application.Features.Mediator.Results.FeatureResults;
using UdemyCarBook.Application.Features.Mediator.Results.BlogResults;
using UdemyCarBook.Application.Interfaces;
using UdemyCarBook.Domain.Entities;

namespace UdemyCarBook.Application.Features.Mediator.Handlers.BlogHandlers
{
    public class GetBlogByIdQueryHandler : IRequestHandler<GetBlogByIdQuery, GetBlogByIdQueryResult>
    {
        private readonly IRepository<Blog> _repository;
        public GetBlogByIdQueryHandler(IRepository<Blog> repository)
        {
            _repository = repository;
        }

        public async Task<GetBlogByIdQueryResult> Handle(GetBlogByIdQuery request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (request.Id <= 0)
                throw new ArgumentException("ID must be greater than zero.");

            var blog = await _repository.GetByIdAsync(request.Id);

            if (blog == null)
                throw new Exception($"Blog with ID {request.Id} not found.");

            return new GetBlogByIdQueryResult
            {
                BlogID = blog.BlogID,
                AuthorID = blog.AuthorID,
                CategoryID = blog.CategoryID,
                CoverImageUrl = blog.CoverImageUrl,
                CreatedDate = blog.CreatedDate,
                Description = blog.Description,
                Title = blog.Title
            };
        }

    }
}
