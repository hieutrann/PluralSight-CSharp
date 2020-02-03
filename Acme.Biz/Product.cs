using Acme.Common;
using static Acme.Common.LoggingService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acme.Biz
{
    /// <summary>
    /// Manages products carried in inventory
    /// </summary>
    public class Product
    {
        public const double InchesPerMeter = 39.37;
        public readonly decimal MinimumPrice;
        #region Constructors
        public Product()
        {
            Console.WriteLine("Product instance created");
            this.MinimumPrice = .96m;
            this.Category = "Tools";
        }
        public Product(int productId,
                        string productName,
                        string description) : this()
        {
            this.ProductId = productId;
            this.ProductName = productName;
            this.Description = description;
            if (ProductName.StartsWith("Bulk")) 
            {
                this.MinimumPrice = 9.99m;
            }
             
            Console.WriteLine("This is the product " + ProductName);
        }
        #endregion

        #region Properties
        //public DateTime? AvailabilityDate { get; set; }
        private DateTime? availabilityDate;

        public DateTime? AvailabilityDate
        {
            get { return availabilityDate; }
            set { availabilityDate = value; }
        }


        public decimal Cost { get; set; }

        public string Description { get; set; }

        public int ProductId { get; set; }

        private string productName;
        public string ProductName
        {
            get {
                var formattedValue = productName?.Trim();
                return formattedValue;
            }
            set
            {
                if (value.Length < 3)
                {
                    ValidationMessage = "Product Name must be at least 3 characters";
                }
                else if (value.Length > 20)
                {
                    ValidationMessage = "Product Name cannot be more than 20 characters";

                }
                else
                {
                    productName = value;

                }
            }
        }

        private Vendor productVendor;
        public Vendor ProductVendor
        {
            get {
                if (productVendor == null)
                {
                    productVendor = new Vendor();
                }
                return productVendor;
            }
            set { productVendor = value; }
        }

        public string ValidationMessage { get; private set; }
        public string Category { get; set; }
        public int SequenceNumber { get; set; } = 1;
        public string ProductCode => $"{this.Category}-{this.SequenceNumber:0000}";
        #endregion

        /// <summary>
        /// Calculates the suggested retail price
        /// </summary>
        /// <param name="markupPercent">Percent used to mark up the cost.</param>
        /// <returns></returns>
        public decimal CalculateSuggestedPrice(decimal markupPercent) =>
             this.Cost + (this.Cost * markupPercent / 100);

        public override string ToString() => this.ProductName + " (" + this.ProductId + ")";
        

      
        public string SayHello()
        {
            //var vendor = new Vendor();
            //vendor.SendWelcomeEmail("Message from Product");
            return "Hello " + ProductName + " (" + ProductId + "): " + Description + " " + ProductVendor.CompanyName + " Available on: " + AvailabilityDate?.ToShortDateString();
        }
    }
}
