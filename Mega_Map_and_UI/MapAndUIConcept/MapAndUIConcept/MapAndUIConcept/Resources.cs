using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MapAndUIConcept
{
    class Resources
    {
        //all the stats and resources for the game are going to be tracked here

        int people;
        public int People
        {
            get { return people; }
            set { people = value; }
        }

        public Resources()
        {
            people = 2;
        }
    }
}
