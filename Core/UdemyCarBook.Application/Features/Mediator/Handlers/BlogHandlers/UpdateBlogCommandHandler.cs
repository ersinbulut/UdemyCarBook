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
    public class UpdateBlogCommandHandler : IRequestHandler<UpdateBlogCommand>
    {
        private readonly IRepository<Blog> _repository;
        private readonly IRepository<Category> _categoryRepository;
        private readonly IRepository<Author> _authorRepository;

        public UpdateBlogCommandHandler(IRepository<Blog> repository, IRepository<Category> categoryRepository, IRepository<Author> authorRepository)
        {
            _repository = repository;
            _categoryRepository = categoryRepository;
            _authorRepository = authorRepository;
        }
        public async Task Handle(UpdateBlogCommand request, CancellationToken cancellationToken)
        {
            var blog = await _repository.GetByIdAsync(request.BlogID);
            if (blog == null)
            {
                throw new ArgumentException($"BlogID {request.BlogID} bulunamadı.");
            }

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

            blog.AuthorID = request.AuthorID;
            blog.CreatedDate= request.CreatedDate;
            blog.CategoryID = request.CategoryID;
            blog.CoverImageUrl = request.CoverImageUrl;
            blog.Title = request.Title;
            await _repository.UpdateAsync(blog);
        }
    }
}
