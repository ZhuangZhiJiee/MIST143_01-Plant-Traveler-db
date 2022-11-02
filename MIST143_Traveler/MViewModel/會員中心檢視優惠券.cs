using MIST143_Traveler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MIST143_Traveler.MViewModel
{
    public class 會員中心檢視優惠券
    {
        public List<我的優惠券> 優惠券列表;
        public 會員中心檢視優惠券()
        {
            _cp = new Coupon();
        }
        private Coupon _cp;
        public Coupon coupon 
        {
            get {return _cp; }
            set {_cp=value; }
        }

        public int CouponId 
        { 
          get { return _cp.CouponId; }
          set { _cp.CouponId =value; }
        }
        public string GiftKey
        {
            get { return _cp.GiftKey; }
            set { _cp.GiftKey = value; }
        }
        public string CouponName
        {
            get { return _cp.CouponName; }
            set { _cp.CouponName = value; }
        }
        public decimal Discount
        {
            get { return _cp.Discount; }
            set { _cp.Discount = value; }
        }
        public string ExDate
        {
            get { return _cp.ExDate; }
            set { _cp.ExDate = value; }
        }
        public string Condition
        {
            get { return _cp.Condition; }
            set { _cp.Condition = value; }
        }
        public bool? Useful
        {
            get { return _cp.Useful; }
            set { _cp.Useful = value; }
        }
        public int MembersId
        {
            get;
            set;
        }





    }

    
    public class 我的優惠券
    {
        public int CouponId { get; set; }
        public string CouponName { get; set; }
        public decimal Discount { get; set; }
        public string ExDate { get; set; }
        public string Condition { get; set; }
        public string GiftKey
        {
            get;
            set;
        }
        public int MembersId
        {
            get;
            set;
        }
    }
}
