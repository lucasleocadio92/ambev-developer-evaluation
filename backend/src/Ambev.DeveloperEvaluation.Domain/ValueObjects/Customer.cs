using System.Text.RegularExpressions;

namespace Ambev.DeveloperEvaluation.Domain.ValueObjects
{
    public class Customer
    {
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Phone { get; private set; }
        public string Document { get; private set; }

        public Customer(string name, string email, string phone, string document)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Customer name cannot be empty.");
            if (!IsValidEmail(email))
                throw new ArgumentException("Invalid email format.");
            if (!IsValidPhone(phone))
                throw new ArgumentException("Invalid phone number format.");

            if (!IsValidDocument(document))
                throw new ArgumentException("Invalid document format.");

            Name = name;
            Email = email;
            Phone = phone;
            Document = document;
        }

        private static bool IsValidEmail(string email) => email.Contains("@");

        private static bool IsValidPhone(string phone) => phone.StartsWith("+") && phone.Length >= 10;

        private static bool IsValidDocument(string document)
        {
            // CPF or CNPJ validation regex
            // CPF: 000.000.000-00
            // CNPJ: 00.000.000/0000-00

            var cpfPattern = @"^\d{3}\.\d{3}\.\d{3}-\d{2}$"; // Regex for CPF
            var cnpjPattern = @"^\d{2}\.\d{3}\.\d{3}/\d{4}-\d{2}$"; // Regex for CNPJ

            return Regex.IsMatch(document, cpfPattern) || Regex.IsMatch(document, cnpjPattern);
        }

        public override bool Equals(object? obj)
        {
            if (obj is null)
                return false;

            if (obj is Customer other)
            {
                return other.Name == Name && other.Email == Email && other.Phone == Phone;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Email, Phone);
        }
    }
}
