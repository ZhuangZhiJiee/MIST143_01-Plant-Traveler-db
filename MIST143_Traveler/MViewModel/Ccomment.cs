using MIST143_Traveler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MIST143_Traveler.MViewModel
{
    public class Ccomment
    {
        public Ccomment()
        {
            _commment = new Comment();
        }
        private Comment _commment;

        public Comment comment
        {
            get { return _commment; }
            set { _commment = value; }
        }
        public int CommentId 
        {
            get { return _commment.CommentId; }
            set { _commment.CommentId = value; }
        }
        public int MembersId 
        {
            get { return _commment.MembersId; }
            set { _commment.MembersId = value; }
        }
        public int TravelProductId 
        {
            get { return _commment.TravelProductId; }
            set { _commment.TravelProductId = value; }
        }
        public string CommentText
        {
            get { return _commment.CommentText; }
            set { _commment.CommentText = value; }
        }
        public int Star 
        {
            get { return _commment.Star; }
            set { _commment.Star = value; }
        }

       
        public string CommentDate
        {
            get { return _commment.CommentDate; }
            set { _commment.CommentDate = value; }
        }
        public bool? CommentStatus
        {
            get { return _commment.CommentStatus; }
            set { _commment.CommentStatus = value; }
        }


        public int? OrderId
        {
            get { return _commment.OrderId; }
            set { _commment.OrderId = value; }
        }
       
    }
}
       

