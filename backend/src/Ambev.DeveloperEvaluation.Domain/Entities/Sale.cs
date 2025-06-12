using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Validation;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    /// <summary>
    /// Represents a sale transaction in the system.
    /// This entity follows domain-driven design principles and includes business rules validation.
    /// </summary>
    public class Sale : BaseEntity
    {
        /// <summary>
        /// Gets or sets the sale date.
        /// </summary>
        public DateTime SaleDate { get; set; }

        /// <summary>
        /// Gets or sets the customer information (name, email, phone) as a value object.
        /// </summary>
        public Customer Customer { get; set; }

        /// <summary>
        /// Gets or sets the branch or location where the sale was made.
        /// </summary>
        public string Branch { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the sale's status.
        /// </summary>
        public SaleStatus Status { get; set; } = SaleStatus.Pending;

        /// <summary>
        /// Gets the total sale amount after applying any discounts.
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// Gets the list of items in the sale.
        /// Each sale can have multiple products.
        /// </summary>
        public List<SaleItem> Items { get; set; } = [];

        /// <summary>
        /// Initializes a new instance of the Sale class.
        /// </summary>
        public Sale(Customer customer)
        {
            if (customer == null)
                throw new ArgumentNullException(nameof(customer));

            Customer = customer;
            SaleDate = DateTime.UtcNow;
        }

        /// <summary>
        /// Validates the sale entity according to business rules and domain validation.
        /// </summary>
        /// <returns>A <see cref="ValidationResultDetail"/> containing validation results.</returns>
        public ValidationResultDetail Validate()
        {
            var validator = new SaleValidator();
            var result = validator.Validate(this);

            return new ValidationResultDetail
            {
                IsValid = result.IsValid,
                Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
            };
        }

        /// <summary>
        /// Calculates the total amount of the sale, applying discounts and summing up item totals.
        /// </summary>
        public void CalculateTotalAmount()
        {
            TotalAmount = 0;

            foreach (var item in Items)
            {
                item.ApplyDiscount(); // Applies discount based on quantity or other business rules
                TotalAmount += item.TotalAmount;
            }
        }

        /// <summary>
        /// Cancels the sale by setting the status to cancelled.
        /// </summary>
        public void Cancel()
        {
            if (Status == SaleStatus.Completed)
                throw new InvalidOperationException("Cannot cancel a completed sale.");

            Status = SaleStatus.Canceled;
        }

        /// <summary>
        /// Marks the sale as completed.
        /// </summary>
        public void Complete()
        {
            if (Status != SaleStatus.Pending && Status != SaleStatus.InProgress)
                throw new InvalidOperationException("Sale must be pending or in progress to be completed.");

            Status = SaleStatus.Completed;
        }

        /// <summary>
        /// Starts the sale's payment process, setting the status to In Progress.
        /// </summary>
        public void StartPaymentProcess()
        {
            if (Status != SaleStatus.Pending)
                throw new InvalidOperationException("Sale must be pending to start the payment process.");

            Status = SaleStatus.InProgress;
        }
    }
}
