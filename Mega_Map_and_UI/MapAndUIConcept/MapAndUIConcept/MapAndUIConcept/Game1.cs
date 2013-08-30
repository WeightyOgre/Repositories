using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace MapAndUIConcept
{

    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        const int screenResolutionWidth = 1920;
        const int screenResolutionHeight = 1080;

        GameArt gameArt;

        //map variables
        Block[,] MapArea;

        int mapAreaBlocksAcross;
        int mapAreaBlocksDown;

        int mapAreaBound0;
        int mapAreaBound1;

        int mapAreaBlockWidth;
        int mapAreaBlockHeight;

        Vector2 mapAreaStartingPosition;

        //main game area variables
        MainGameArea[,] gameArea;

        int sectorsAcross = 3;
        int sectorsDown = 3;

        //ui area variables
        UserInterface UI;
        TextDisplay uiText;
        TextDisplay debugText;
        TextDisplay armyText;

        Resources gameResources;

        Color aColor;
        Color anotherColor;

        MouseState mouseState;

        float elapsedTime;
        float minWaitTime;

        

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            //set up the viewport to match screen resolution and set to full screen
            graphics.PreferredBackBufferWidth = screenResolutionWidth;
            graphics.PreferredBackBufferHeight = screenResolutionHeight;
            //graphics.IsFullScreen = true;
            graphics.ApplyChanges();

            this.IsMouseVisible = true;

            gameArt = new GameArt(this.Content);
            InitializeGameArea();
            InitilaizeMapArea();
            InitializeUIArea();

            aColor = Color.White;
            anotherColor = Color.White;

            gameResources = new Resources();

            uiText = new TextDisplay(this.Content, "test", new Vector2(85 + (40 * 4), 300 + (40 * 6)));
            debugText = new TextDisplay(this.Content, "test", new Vector2(0, 0));
            armyText = new TextDisplay(this.Content, "0", new Vector2(85 + (40 * 6), 300 + (40 * 9)));
            //set time to zero
            elapsedTime = 0;

            minWaitTime = 30000;

            

            gameArea[0, 0].AddBuilding();
            gameArea[2, 2].AddBuilding();
            gameArea[0, 0].People = 10;
            
            base.Initialize();
        }

        private void InitializeUIArea()
        {
            UI = new UserInterface(gameArt.TileTexture.Width, gameArt.TileTexture.Height);
        }

        private void InitializeGameArea()
        {
            gameArea = new MainGameArea[sectorsAcross, sectorsDown];

            for (int i = 0; i <= sectorsAcross - 1; i++)
            {
                for (int j = 0; j <= sectorsDown - 1; j++)
                {
                    gameArea[i, j] = new MainGameArea(gameArt.TileTexture.Width, gameArt.TileTexture.Height);
                }
            }
            gameArea[0, 0].Active = true;
        }

        private void InitilaizeMapArea()
        {
            //setup map area
            mapAreaStartingPosition = new Vector2(100, 100);
            mapAreaBlocksAcross = 3;
            mapAreaBlocksDown = 3;

            mapAreaBlockWidth = gameArt.TileTexture.Width;
            mapAreaBlockHeight = gameArt.TileTexture.Height;

            MapArea = new Block[mapAreaBlocksAcross, mapAreaBlocksDown];

            mapAreaBound0 = MapArea.GetUpperBound(0);
            mapAreaBound1 = MapArea.GetUpperBound(1);

            for (int i = 0; i <= mapAreaBound0; i++)
            {
                for (int j = 0; j <= mapAreaBound1; j++)
                {

                    Vector2 position = new Vector2(mapAreaStartingPosition.X + i * mapAreaBlockWidth, mapAreaStartingPosition.Y + j * mapAreaBlockHeight);
                    MapArea[i, j] = new Block(position);

                }
            }
        }

        public void updateMapSelection()
        {
            mouseState = Mouse.GetState();

            Rectangle mouseRectangle = new Rectangle(mouseState.X, mouseState.Y, 1, 1);

            Rectangle mapAreaRectangle = new Rectangle((int)mapAreaStartingPosition.X, (int)mapAreaStartingPosition.Y, 40 * 3, 40 * 3);

            if (mouseRectangle.Intersects(mapAreaRectangle))
            {
                //left mouse button click
                if (mouseState.LeftButton == ButtonState.Pressed && mouseState.RightButton == ButtonState.Released)
                {
                    //second is the ui icons
                    for (int i = 0; i <= mapAreaBound0; i++)
                    {
                        for (int j = 0; j <= mapAreaBound1; j++)
                        {
                            Rectangle mapRectangle = new Rectangle((int)MapArea[i, j].TileTexturePosition.X, (int)MapArea[i, j].TileTexturePosition.Y, 40, 40);

                            if (mouseRectangle.Intersects(mapRectangle))
                            {
                                gameArea[i, j].Active = true;
                                moveArmy();
                            }
                            else
                            {
                                gameArea[i, j].Active = false;
                            }
                        }
                    }
                }
            }
        }

        private void updateColor()
        {
            if (gameArea[0, 0].Active == true)
            {
                aColor = Color.Red;
            }
            if (gameArea[1, 0].Active == true)
            {
                aColor = Color.Blue;
            }
            if (gameArea[2, 0].Active == true)
            {
                aColor = Color.Green;
            }
            if (gameArea[0, 1].Active == true)
            {
                aColor = Color.Purple;
            }
            if (gameArea[0, 2].Active == true)
            {
                aColor = Color.Yellow;
            }
            if (gameArea[1, 1].Active == true)
            {
                aColor = Color.Pink;
            }
            if (gameArea[1, 2].Active == true)
            {
                aColor = Color.BlanchedAlmond;
            }
            if (gameArea[2, 1].Active == true)
            {
                aColor = Color.White;
            }
            if (gameArea[2, 2].Active == true)
            {
                aColor = Color.Black;
            }
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        public bool minimumWaitTime(GameTime gameTime)
        {
            elapsedTime += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (elapsedTime >= minWaitTime)
            {
                elapsedTime = 0;
                return true;
            }
            else
            {
                return false;
            }

        }

        private void updateResources(GameTime gameTime)
        {
            if (minimumWaitTime(gameTime))
            {
                for (int i = 0; i <= sectorsAcross - 1; i++)
                {
                    for (int j = 0; j <= sectorsDown - 1; j++)
                    {
                        if (gameArea[i, j].Buildings.Count >= 1)
                        {

                            gameArea[i, j].People += 1;

                        }
                    }
                }
            }
        }

        private void updateUI()
        {
            mouseState = Mouse.GetState();
            //check to see the state of the ui
            if (UI.isMain())
            {
                if (mouseState.LeftButton == ButtonState.Pressed && mouseState.RightButton == ButtonState.Released)
                {
                    //depending on the state
                    //check for collision(click) on and act on in different ways.
                    Rectangle mouseRectangle = new Rectangle(mouseState.X, mouseState.Y, 1, 1);
                    Rectangle militaryRectangle = new Rectangle((int)UI.ArmyIconPosition.X, (int)UI.ArmyIconPosition.Y, gameArt.ArmyIconTexture.Width, gameArt.ArmyIconTexture.Height);

                    if (mouseRectangle.Intersects(militaryRectangle))
                    {
                        UI.setToMilitary();
                    }
                }
            }
            if (UI.isMilitary())
            {
                if (mouseState.LeftButton == ButtonState.Pressed && mouseState.RightButton == ButtonState.Released)
                {
                    //depending on the state
                    //check for collision(click) on and act on in different ways.
                    Rectangle mouseRectangle = new Rectangle(mouseState.X, mouseState.Y, 1, 1);
                    Rectangle personRectangle = new Rectangle((int)UI.PersonIcon.X, (int)UI.PersonIcon.Y, gameArt.PersonIcon.Width, gameArt.PersonIcon.Height);

                    if (mouseRectangle.Intersects(personRectangle))
                    {
                        UI.setToMain();
                    }

                    Rectangle rockThrowerRectangle = new Rectangle((int)UI.RockIconPosition.X, (int)UI.RockIconPosition.Y, gameArt.RockIconTexture.Width, gameArt.RockIconTexture.Height);

                    if (mouseRectangle.Intersects(rockThrowerRectangle))
                    {
                        //add from population to army
                        addArmyToSector();
                    }

                }
                else if (mouseState.LeftButton == ButtonState.Released && mouseState.RightButton == ButtonState.Pressed)
                {
                    Rectangle mouseRectangle = new Rectangle(mouseState.X, mouseState.Y, 1, 1);
                    Rectangle rockThrowerRectangle = new Rectangle((int)UI.RockIconPosition.X, (int)UI.RockIconPosition.Y, gameArt.RockIconTexture.Width, gameArt.RockIconTexture.Height);
                    if (mouseRectangle.Intersects(rockThrowerRectangle))
                    {
                        //remove from population to army
                        removeArmyFromSector();
                    }
                }
            }

        }

        private void removeArmyFromSector()
        {
            for (int i = 0; i <= sectorsAcross - 1; i++)
            {
                for (int j = 0; j <= sectorsDown - 1; j++)
                {
                    if (gameArea[i, j].Active == true)
                    {
                        if (gameArea[i, j].ArmyAtThisSector >= 1)
                        {
                            gameArea[i, j].ArmyAtThisSector -= 1;
                            gameArea[i, j].People += 1;
                        }
                    }
                }
            }
        }

        private void addArmyToSector()
        {
            for (int i = 0; i <= sectorsAcross - 1; i++)
            {
                for (int j = 0; j <= sectorsDown - 1; j++)
                {
                    if (gameArea[i, j].Active == true)
                    {
                        if (gameArea[i, j].People >= 1)
                        {
                            gameArea[i, j].ArmyAtThisSector += 1;
                            gameArea[i, j].People -= 1;
                        }
                    }
                }
            }
        }

        private void updateText(GameTime gameTime)
        {
            debugText.stringValue = Convert.ToString(gameTime.TotalGameTime);
            for (int i = 0; i <= sectorsAcross - 1; i++)
            {
                for (int j = 0; j <= sectorsDown - 1; j++)
                {
                    if (gameArea[i, j].Active == true)
                    {
                        uiText.stringValue = Convert.ToString(gameArea[i, j].People);
                        if (UI.isMilitary())
                        {
                            armyText.stringValue = Convert.ToString(gameArea[i, j].ArmyAtThisSector);
                        }
                    }
                }
            }
        }

        private void updateArmyAtSector()
        {
            for (int i = 0; i <= sectorsAcross - 1; i++)
            {
                for (int j = 0; j <= sectorsDown - 1; j++)
                {
                    gameArea[i, j].updateArmyUnit();
                }
            }
        }

        private void updateArmyUnitMovement()
        {
            for (int i = 0; i <= sectorsAcross - 1; i++)
            {
                for (int j = 0; j <= sectorsDown - 1; j++)
                {
                    gameArea[i, j].updateArmyUnitMovement();
                }
            }
        }

        private void updateMainGameAreaUI()
        {
            mouseState = Mouse.GetState();

                //move an army
                Rectangle mouseRectangle = new Rectangle(mouseState.X, mouseState.Y, 1, 1);

                for (int i = 0; i <= sectorsAcross - 1; i++)
                {
                    for (int j = 0; j <= sectorsDown - 1; j++)
                    {
                        if (gameArea[i, j].ArmyAtThisSector > 0)
                        {
                            Rectangle mainGameAreaRectangle = new Rectangle((int)gameArea[i, j].GameAreaStartingPosition.X, (int)gameArea[i, j].GameAreaStartingPosition.Y, gameArt.TileTexture.Width * gameArea[i, j].GameAreaBlocksAcross, gameArt.TileTexture.Height * gameArea[i, j].GameAreaBlocksDown);

                            if (mouseRectangle.Intersects(mainGameAreaRectangle))
                            {
                                if (mouseState.LeftButton == ButtonState.Pressed && mouseState.RightButton == ButtonState.Released)
                                {
                                    
                                    gameArea[i, j].MoveArmy = true;
                                    
                                    //get ready to move the army, if next click is on the mini map, copy army from this highlighted sector to the selected sector
                                }
                            }
                        }
                    }
                }
            
        }

        private void moveArmy()
        {
            for (int i = 0; i <= sectorsAcross - 1; i++)
            {
                for (int j = 0; j <= sectorsDown - 1; j++)
                {
                    if (gameArea[i, j].MoveArmy == true)
                    {
                        for (int k = 0; k <= sectorsAcross - 1; k++)
                        {
                            for (int l = 0; l <= sectorsDown - 1; l++)
                            {
                                if (gameArea[k, l].Active == true)
                                {
                                    //move army from move army to active sector
                                    gameArea[k, l].ArmyAtThisSector = gameArea[i, j].ArmyAtThisSector;
                                    gameArea[i, j].ArmyAtThisSector = 0;
                                    gameArea[i, j].MoveArmy = false;
                                }
                            }
                        }
                    }
                }
            }
        }

        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (Keyboard.GetState().IsKeyDown(Keys.Escape) == true)
            {
                this.Exit();
            }

            updateMapSelection();
            updateColor();

            updateResources(gameTime);

            updateUI();
            updateText(gameTime);

            updateArmyAtSector();

            updateArmyUnitMovement();

            updateMainGameAreaUI();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            DrawBackground();
            DrawGameArea();
            DrawMapArea();
            DrawUIArea();
            DrawBuilding();
            DrawArmyUnits();
            
            spriteBatch.End();

            SpriteBatch fontBatch = new SpriteBatch(GraphicsDevice);
            uiText.DrawFont(fontBatch);
            debugText.DrawFont(fontBatch);
            if (UI.isMilitary())
            {
                armyText.stringPositionValue = new Vector2(85 + (40 * 6), 300 + (40 * 9));
                armyText.DrawFont(fontBatch);
            }
            if (UI.isMain())
            {
                armyText.stringPositionValue = new Vector2(UI.UIAreaStartingPosition.X + (6 * gameArt.TileTexture.Width) + 10, UI.UIAreaStartingPosition.Y + (6*gameArt.TileTexture.Height));
                armyText.DrawFont(fontBatch);
            }

            base.Draw(gameTime);
        }

        public void DrawArmyUnits()
        {
            for (int i = 0; i <= sectorsAcross - 1; i++)
            {
                for (int j = 0; j <= sectorsDown - 1; j++)
                {
                    if (gameArea[i, j].Active == true)
                    {
                        for (int k = 0; k <= gameArea[i, j].ArmyUnits.Count - 1; k++)
                        {
                            spriteBatch.Draw(gameArt.PersonWithRockIconTexture, gameArea[i, j].ArmyUnits[k].UnitPosition, Color.White);
                        }
                    }
                }
            }
        }

        public void DrawBuilding()
        {
            for (int i = 0; i <= sectorsAcross - 1; i++)
            {
                for (int j = 0; j <= sectorsDown - 1; j++)
                {
                    if (gameArea[i, j].Active == true)
                    {
                        for (int k = gameArea[i, j].Buildings.Count - 1; k >= 0; k--)
                        {
                            spriteBatch.Draw(gameArt.BuildingTexture, gameArea[i, j].Buildings[k].BuildingTexturePosition, Color.White);
                        }
                    }
                }
            }  
        }

        public void DrawBackground()
        {
            spriteBatch.Draw(gameArt.BackGroundTexture, new Vector2(0, 0), anotherColor);
        }

        private void DrawUIArea()
        {
            for (int k = 0; k <= UI.UIAreaBound0; k++)
            {
                for (int l = 0; l <= UI.UIAreaBound1; l++)
                {
                    spriteBatch.Draw(gameArt.TileTexture, UI.UIArea[k,l].TileTexturePosition, Color.White);
                }
            }
            //Draw Icons
            spriteBatch.Draw(gameArt.PersonIcon, UI.PersonIcon, Color.White);
            spriteBatch.Draw(gameArt.RightArrowIcon, UI.RightArrowIconPosition, Color.White);
            spriteBatch.Draw(gameArt.ArmyIconTexture, UI.ArmyIconPosition, Color.White);
            spriteBatch.Draw(gameArt.LeftArrowIcon, UI.LeftArrowIconPosition, Color.White);
            spriteBatch.Draw(gameArt.ResearchIconTexture, UI.ResearchIconPosition, Color.White);
            spriteBatch.Draw(gameArt.DownArrowIcon, UI.DownArrowIconPosition, Color.White);
            spriteBatch.Draw(gameArt.RockIconTexture, UI.RockIconPosition, Color.White);
            spriteBatch.Draw(gameArt.PersonWithRockIconTexture, UI.PersonWIthROckIconPosition, Color.White);
        }

        private void DrawGameArea()
        {
            for (int i = 0; i <= sectorsAcross-1; i++)
            {
                for (int j = 0; j <= sectorsDown-1; j++)
                {
                    if (gameArea[i, j].Active == true)
                    {
                        for (int k = 0; k <= gameArea[i, j].GameAreaBound0; k++)
                        {
                            for (int l = 0; l <= gameArea[i, j].GameAreaBound1; l++)
                            {
                                spriteBatch.Draw(gameArt.TileTexture, gameArea[i, j].GameArea[k, l].TileTexturePosition, aColor);
                                
                            }
                        }
                    }
                }
            } 
        }

        private void DrawMapArea()
        {
            for (int i = 0; i <= mapAreaBound0; i++)
            {
                for (int j = 0; j <= mapAreaBound1; j++)
                {
                    spriteBatch.Draw(gameArt.TileTexture, MapArea[i, j].TileTexturePosition, Color.White);
                }
            } 
        }

    }
}
