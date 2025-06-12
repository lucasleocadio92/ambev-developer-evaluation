using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale
{
    /// <summary>
    /// Handler for processing the <see cref="CancelSaleCommand"/>.
    /// </summary>
    /// <remarks>
    /// This handler is responsible for locating the sale by ID, validating its state,
    /// marking it as cancelled, and persisting the changes to the database.
    /// It returns a <see cref="CancelSaleResult"/> containing the updated sale details.
    /// </remarks>
    public class CancelSaleHandler : IRequestHandler<CancelSaleCommand, CancelSaleResult>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMediator _mediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="CancelSaleHandler"/> class.
        /// </summary>
        /// <param name="saleRepository">The repository to access sale records.</param>
        /// <param name="mapper">The AutoMapper instance</param>
        /// <param name="mediator">The Mediator instance to send events</param>
        public CancelSaleHandler(ISaleRepository saleRepository, IMediator mediator)
        {
            _saleRepository = saleRepository;
            _mediator = mediator;
        }

        /// <summary>
        /// Handles the cancel sale operation.
        /// </summary>
        /// <param name="command">The <see cref="CancelSaleCommand"/> containing the sale ID.</param>
        /// <param name="cancellationToken">A token to observe while waiting for the task to complete.</param>
        /// <returns>A <see cref="CancelSaleResult"/> containing the updated sale data.</returns>
        public async Task<CancelSaleResult> Handle(CancelSaleCommand command, CancellationToken cancellationToken)
        {
            var validator = new CancelSaleCommandValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var sale = await _saleRepository.GetByIdAsync(command.Id, cancellationToken);

            if (sale is null)
                throw new KeyNotFoundException($"Sale with ID {command.Id} not found");

            if (sale.Status == SaleStatus.Canceled)
                throw new DomainException("Sale is already cancelled.");

            sale.Cancel();

            await _saleRepository.UpdateAsync(sale, cancellationToken);

            var saleEvent = new SaleCancelledEvent(sale);
            await _mediator.Publish(saleEvent, cancellationToken);

            return new CancelSaleResult { Id = sale.Id, Status = sale.Status };
        }
    }
}
