using MIST143_Traveler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MIST143_Traveler.MViewModel
{
    public class CtoMyFavorites
    {

        public CtoMyFavorites()
        {
            _CtoMyFavorites = new Myfavorite();
        }
        private Myfavorite _CtoMyFavorites;
        public Myfavorite myfavorite
        {
            get { return _CtoMyFavorites; }
            set { _CtoMyFavorites = value; }
        }

        public int TravelProductId
        {
            get { return _CtoMyFavorites.TravelProductId; }
            set { _CtoMyFavorites.TravelProductId = value; }
        }
        public int MembersId
        {
            get { return _CtoMyFavorites.MembersId; }
            set { _CtoMyFavorites.MembersId = value; }
        }
        public int MyfavoritesId
        {
            get { return _CtoMyFavorites.MyfavoritesId; }
            set { _CtoMyFavorites.MyfavoritesId = value; }
        }
    }
}
