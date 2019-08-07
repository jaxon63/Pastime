using System;
using System.Collections.Generic;
using System.Text;

namespace Pastime.ViewModels
{
    public class OtherProfilesViewModel
    {
        private string name;
        private string desc;
        private string memberSince;
        private string location;
        private string favSports;


        public OtherProfilesViewModel(string name, string desc, string memberSince, string location, string favSports)
        {
            this.name = name;
            this.desc = desc;
            this.memberSince = memberSince;
            this.location = location;
            this.favSports = favSports;
        }

        public string Name => name;

        public string Desc => desc;

        public string MemberSince => memberSince;

        public string Location => location;

        public string FavSports => favSports;

    }

    
}
