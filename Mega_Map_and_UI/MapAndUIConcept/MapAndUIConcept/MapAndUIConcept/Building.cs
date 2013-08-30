using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MapAndUIConcept
{
    class Building
    {
        //Building position
        Vector2 buildingPosition;
        public Vector2 BuildingTexturePosition
        {
            get { return buildingPosition; }
            set { buildingPosition = value; }
        }

        public Building(Vector2 buildingPosition)
        {
            this.buildingPosition = buildingPosition;
        }

    }
}
