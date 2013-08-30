using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MapAndUIConcept
{
    class Block
    {
        //a Block
        Vector2 tileTexturePosition;
        public Vector2 TileTexturePosition
        {
            get { return tileTexturePosition; }
            set { tileTexturePosition = value; }
        }

        public Block(Vector2 tileTexturePosition)
        {
            
            this.tileTexturePosition = tileTexturePosition;
        }

    }
}
