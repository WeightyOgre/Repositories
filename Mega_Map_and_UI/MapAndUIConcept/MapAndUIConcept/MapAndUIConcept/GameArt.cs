using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace MapAndUIConcept
{
    class GameArt
    {
        //background image
        Texture2D backGroundTexture;
        public Texture2D BackGroundTexture
        {
            get { return backGroundTexture; }
            set { backGroundTexture = value; }
        }

        //tile image
        Texture2D tileTexture;
        public Texture2D TileTexture
        {
            get { return tileTexture; }
            set { tileTexture = value; }
        }

        //UI Icons
        Texture2D personIcon;
        public Texture2D PersonIcon
        {
            get { return personIcon; }
            set { personIcon = value; }
        }

        //right arrow icon texture
        Texture2D rightArrowIcon;
        public Texture2D RightArrowIcon
        {
            get { return rightArrowIcon; }
            set { rightArrowIcon = value; }
        }

        //army icon texture
        Texture2D armyIconTexture;
        public Texture2D ArmyIconTexture
        {
            get { return armyIconTexture; }
            set { armyIconTexture = value; }
        }

        //left arrow icon texture
        Texture2D leftArrowIcon;
        public Texture2D LeftArrowIcon
        {
            get { return leftArrowIcon; }
            set { leftArrowIcon = value; }
        }

        //research icon texture
        Texture2D researchIconTexture;
        public Texture2D ResearchIconTexture
        {
            get { return researchIconTexture; }
            set { researchIconTexture = value; }
        }

        //down arrow icon texture
        Texture2D downArrowIcon;
        public Texture2D DownArrowIcon
        {
            get { return downArrowIcon; }
            set { downArrowIcon = value; }
        }

        //building texture
        Texture2D buildingTexture;
        public Texture2D BuildingTexture
        {
            get { return buildingTexture; }
            set { buildingTexture = value; }
        }

        Texture2D rockIconTexture;
        public Texture2D RockIconTexture
        {
            get { return rockIconTexture; }
            set { rockIconTexture = value; }
        }

        Texture2D personWithRockIconTexture;
        public Texture2D PersonWithRockIconTexture
        {
            get { return personWithRockIconTexture; }
            set { personWithRockIconTexture = value; }
        }

        ContentManager content;

        public GameArt(ContentManager content)
        {
            this.content = content;
            //load the games background image
            BackGroundTexture = content.Load<Texture2D>("background1920x1080");
            
            //load the tile image for mapping out main game area
            TileTexture = content.Load<Texture2D>("brick40x40");

            PersonIcon = content.Load<Texture2D>("PersonIcon40x40");

            BuildingTexture = content.Load<Texture2D>("BPH");

            RightArrowIcon = content.Load<Texture2D>("RightArrowIcon40x40");

            ArmyIconTexture = content.Load<Texture2D>("ArmyIcon40x40");

            LeftArrowIcon = content.Load<Texture2D>("LeftArrowIcon40x40");

            ResearchIconTexture = content.Load<Texture2D>("ResearchIcon40x40");

            DownArrowIcon = content.Load<Texture2D>("DownArrowIcon40x40");

            RockIconTexture = content.Load<Texture2D>("RockIcon40x40");

            PersonWithRockIconTexture = content.Load<Texture2D>("PersonWithRockIcon40x40");

        }
    }
}
