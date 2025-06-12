using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    /// <summary>
    /// Handler for processing UpdateSaleCommand requests.
    /// </summary>
    public class UpdateSaleHandler : IRequestHandler<UpdateSaleCommand, UpdateSaleResult>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly ISaleService _saleService;

        /// <summary>
        /// Initializes a new instance of <see cref="UpdateSaleHandler"/>.
        /// </summary>
        /// <param name="saleRepository">The sale repository</param>
        /// <param name="mapper">The AutoMapper instance</param>
        /// <param name="mediator">The Mediator instance to send events</param>
        /// <param name="saleService">The sale service</param>
        public UpdateSaleHandler(ISaleRepository saleRepository, IMapper mapper, IMediator mediator, ISaleService saleService)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
            _mediator = mediator;
            _saleService = saleService;
        }

        /// <summary>
        /// Handles the UpdateSaleCommand request.
        /// </summary>
        /// <param name="command">The update sale command</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The updated sale result</returns>
        public async Task<UpdateSaleResult> Handle(UpdateSaleCommand command, CancellationToken cancellationToken)
        {
            var validator = new UpdateSaleCommandValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var sale = await _saleRepository.GetByIdAsync(command.Id, cancellationToken);
            if (sale == null)
                throw new KeyNotFoundException("Sale not found.");

            sale.Customer = command.Customer;
            sale.Branch = command.Branch;
            sale.Status = command.Status;
            sale.Items = _mapper.Map<List<SaleItem>>(command.Items);
            sale.CalculateTotalAmount();

            await _saleService.ValidateBeforeUpdateAsync(sale);

            await _saleRepository.UpdateAsync(sale, cancellationToken);

            var saleEvent = new SaleModifiedEvent(sale);
            await _mediator.Publish(saleEvent, cancellationToken);

            var result = _mapper.Map<UpdateSaleResult>(sale);
            return result;
        }
    }
}
