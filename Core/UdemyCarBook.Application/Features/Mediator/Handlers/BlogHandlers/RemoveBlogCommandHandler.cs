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
    public class RemoveBlogCommandHandler : IRequestHandler<RemoveBlogCommand>
    {
        private readonly IRepository<Blog> _repository;

        public RemoveBlogCommandHandler(IRepository<Blog> repository)
        {
            _repository = repository;
        }
        public async Task Handle(RemoveBlogCommand request, CancellationToken cancellationToken)
        {
            // Blog'un var olup olmadığını kontrol ediyoruz
            var blog = await _repository.GetByIdAsync(request.Id);
            if (blog == null)
            {
                throw new ArgumentException($"BlogID {request.Id} bulunamadı.");
            }
            await _repository.RemoveAsync(blog);
        }
    }
}
