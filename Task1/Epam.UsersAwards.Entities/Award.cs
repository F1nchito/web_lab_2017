using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.UsersAwards.Entities
{
    public class Award
    {
        //public Award(int id, string title, string description)
        //{
        //    ID = id;
        //    Title = title;
        //    Description = description;
        //}

        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public PictureData Image { get; set; }
    }
}
