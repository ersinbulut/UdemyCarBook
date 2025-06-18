using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UdemyCarBook.Application.Features.Mediator.Commands.BlogCommands;
using UdemyCarBook.Application.Interfaces;
using UdemyCarBook.Domain.Entities;

namespace UdemyCarBook.Application.Features.Mediator.Handlers.BlogHandlers
{
    public class CreateBlogCommandHandler : IRequestHandler<CreateBlogCommand>
    {
        private readonly IRepository<Blog> _repository;
        private readonly IRepository<Category> _categoryRepository;
        private readonly IRepository<Author> _authorRepository;

        public CreateBlogCommandHandler(IRepository<Blog> repository, IRepository<Category> categoryRepository, IRepository<Author> authorRepository)
        {
            _repository = repository;
            _categoryRepository = categoryRepository;
            _authorRepository = authorRepository;
        }
        public async Task Handle(CreateBlogCommand request, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetByIdAsync(request.CategoryID);
            if (category == null)
            {
                throw new ArgumentException($"CategoryID {request.CategoryID} bulunamadı. Lütfen geçerli bir kategori ID'si girin.");
            }

            var author = await _authorRepository.GetByIdAsync(request.AuthorID);
            if (author == null)
            {
                throw new ArgumentException($"AuthorID {request.AuthorID} bulunamadı. Lütfen geçerli bir yazar ID'si girin.");
            }

            await _repository.CreateAsync(new Blog
            {
                AuthorID= request.AuthorID,
                CategoryID= request.CategoryID,
                CoverImageUrl= request.CoverImageUrl,
                CreatedDate= request.CreatedDate,
                Title = request.Title,
                Description = request.Description,
            });
        }
    }
}
