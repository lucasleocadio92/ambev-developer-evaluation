namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale
{
    /// <summary>
    /// Represents a customer involved in a sale.
    /// </summary>
    public class GetCustomerDto
    {
        /// <summary>
        /// Gets or sets the name of the customer.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the email address of the customer.
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the phone number of the customer.
        /// </summary>
        public string Phone { get; set; } = string.Empty;
    }
}
