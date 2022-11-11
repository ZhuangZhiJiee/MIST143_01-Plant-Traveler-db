using System;
using System.Collections.Generic;

#nullable disable

namespace MIST143_Traveler.Models
{
    public partial class TravelProduct
    {
        public TravelProduct()
        {
            Comments = new HashSet<Comment>();
            Myfavorites = new HashSet<Myfavorite>();
            OrderDetails = new HashSet<OrderDetail>();
            ProductCoordinates = new HashSet<ProductCoordinate>();
            TravelPictures = new HashSet<TravelPicture>();
            TravelProductDetails = new HashSet<TravelProductDetail>();
        }

        public int TravelProductId { get; set; }
        public string TravelProductName { get; set; }
        public decimal Price { get; set; }
        public int TravelProductTypeId { get; set; }
        public int Quantity { get; set; }
        public int Stocks { get; set; }
        public string Description { get; set; }
        public int CountryId { get; set; }
        public int Cost { get; set; }
        public string EventIntroduction { get; set; }
        public string PreparationDescription { get; set; }
        public string MapUrl { get; set; }
        public string ProductStatus { get; set; }
        public string AnotherDepartureDate { get; set; }

        public virtual Country Country { get; set; }
        public virtual TravelProductType TravelProductType { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Myfavorite> Myfavorites { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<ProductCoordinate> ProductCoordinates { get; set; }
        public virtual ICollection<TravelPicture> TravelPictures { get; set; }
        public virtual ICollection<TravelProductDetail> TravelProductDetails { get; set; }

        
    }
}
