using MIST143_Traveler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MIST143_Traveler.MViewModel
{
    public class Cproductlist
    {
        public Cproductlist()
        {
            _travelProduct = new TravelProduct();
        }
        private TravelProduct _travelProduct;


        public TravelProduct travelProduct
        {
            get {return _travelProduct; }
            set { _travelProduct=value; }
        }

        public int TravelProductId
        {
            get { return _travelProduct.TravelProductId; }
            set { _travelProduct.TravelProductId = value; }
        }
        public string TravelProductName 
        {
            get { return _travelProduct.TravelProductName; }
            set { _travelProduct.TravelProductName = value; }
        }
        public decimal Price
        {
            get { return _travelProduct.Price; }
            set { _travelProduct.Price = value; }
        }
        public int TravelProductTypeId
        {
            //get { return _travelProduct.TravelProductTypeId; }
            set { _travelProduct.TravelProductTypeId = value; }
        }
        public int Stocks
        {
            get { return _travelProduct.Stocks; }
            set { _travelProduct.Stocks = value; }
        }
        public string Description
        {
            get { return _travelProduct.Description; }
            set { _travelProduct.Description = value; }
        }
        public int CountryId
        {
            get { return _travelProduct.CountryId; }
            set { _travelProduct.CountryId = value; }
        }
        public int Cost
        {
            get { return _travelProduct.Cost; }
            set { _travelProduct.Cost = value; }
        }
        public string EventIntroduction
        {
            get { return _travelProduct.EventIntroduction; }
            set { _travelProduct.EventIntroduction = value; }
        }
        public string PreparationDescription
        {
            get { return _travelProduct.PreparationDescription; }
            set { _travelProduct.PreparationDescription = value; }
        }
        public string MapUrl
        {
            get { return _travelProduct.MapUrl; }
            set { _travelProduct.MapUrl = value; }
        }
        public string ProductStatus
        {
            get { return _travelProduct.ProductStatus; }
            set { _travelProduct.ProductStatus = value; }
        }
        public DateTime goqq(string st)
        {
            var time = DateTime.Parse(st);
            return time;
        }

        public string productpicture { get; set; }
        public List<string> TravelPictureText { get; set; }



    }

}
