using MIST143_Traveler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MIST143_Traveler.MViewModel
{
    public class COrderCancel
    {
        public COrderCancel()
        {
            _co = new OrderCancel();
        }
        private OrderCancel _co;

        public OrderCancel ordercancel
        {
            get { return _co; }
            set { _co = value; }
        }
      
        public string Titel
        {
            get { return _co.Titel; }
            set { _co.Titel = value; }
        }
        public string CancaelContent
        {
            get { return _co.CancaelContent; }
            set { _co.CancaelContent = value; }
        }

        public int OrderStatusID
        {
            get; set;
        }
        public int OrderId
        {
            get { return _co.OrderId; }
            set { _co.OrderId = value; }
        }
    }
}
