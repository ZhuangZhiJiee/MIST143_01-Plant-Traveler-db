using Microsoft.AspNetCore.Http;
using MIST143_Traveler.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MIST143_Traveler.MViewModel
{
    public class CMemberView
    {
        public CMemberView()
        {
            _member = new Member();
        }

        private Member _member;

        public Member member
        {
            get { return _member; }
            set { _member = value; }
        }

        public int MembersId
        {
            get { return _member.MembersId; }
            set { _member.MembersId = value; }
        }
        public string MemberName
        {
            get { return _member.MemberName; }
            set { _member.MemberName = value; }
        }
        [Required(ErrorMessage ="密碼未填寫")]
        public string Password
        {
            get { return _member.Password; }
            set { _member.Password = value; }
        }
        public string Phone
        {
            get { return _member.Phone; }
            set { _member.Phone = value; }
        }
        public string Address
        {
            get { return _member.Address; }
            set { _member.Address = value; }
        }
        public string Email
        {
            get { return _member.Email; }
            set { _member.Email = value; }
        }
        public string Gender
        {
            get { return _member.Gender; }
            set { _member.Gender = value; }
        }
        public int CityId
        {
            get { return _member.CityId; }
            set { _member.CityId = value; }
        }
        public string BirthDay
        {
            get { return _member.BirthDay; }
            set { _member.BirthDay = value; }
        }
        public string PhotoPath
        {
            get { return _member.PhotoPath; }
            set { _member.PhotoPath = value; }
        }
        public int MemberStatusId
        {
            get { return _member.MemberStatusId; }
            set { _member.MemberStatusId = value; }
        }
      
        public string FImagePath
        {
            get { return member.PhotoPath; }
            set { member.PhotoPath = value; }
        }
        public IFormFile photo { get; set; }
        public string Cityname { get; set; }
    }
}
