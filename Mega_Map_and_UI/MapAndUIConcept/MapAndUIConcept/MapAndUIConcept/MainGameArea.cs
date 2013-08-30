using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MapAndUIConcept
{
    class MainGameArea
    {
        Block[,] gameArea;
        public Block[,] GameArea
        {
            get { return gameArea; }
            set { gameArea = value; }
        }
        
        int gameAreaBlocksAcross;
        public int GameAreaBlocksAcross
        {
            get { return gameAreaBlocksAcross; }
            set { gameAreaBlocksAcross = value; }
        }

        int gameAreaBlocksDown;
        public int GameAreaBlocksDown
        {
            get { return gameAreaBlocksDown; }
            set { gameAreaBlocksDown = value; }
               
        }

        int gameAreaBound0;
        public int GameAreaBound0
        {
            get { return gameAreaBound0; }
            set { gameAreaBound0 = value; }
        }
        int gameAreaBound1;
        public int GameAreaBound1
        {
            get { return gameAreaBound1; }
            set { gameAreaBound1 = value; }
        }

        int gameAreaBlockWidth;
        int gameAreaBlockHeight;

        Vector2 gameAreastartingPosition;
        public Vector2 GameAreaStartingPosition
        {
            get { return gameAreastartingPosition; }
            set { gameAreastartingPosition = value; }
        }

        bool active;
        public bool Active
        {
            get { return active; }
            set { active = value; }
        }

        List<Building> buildings;
        public List<Building> Buildings
        {
            get { return buildings; }
            set { buildings = value; }
        }

        int armyAtThisSector;
        public int ArmyAtThisSector
        {
            get { return armyAtThisSector; }
            set { armyAtThisSector = value; }
        }

        int people;
        public int People
        {
            get { return people; }
            set { people = value; }
        }

        List<ArmyUnit> armyUnits;
        public List<ArmyUnit> ArmyUnits
        {
            get { return armyUnits; }
            set { armyUnits = value; }
        }

        bool moveArmy;
        public bool MoveArmy
        {
            get { return moveArmy; }
            set { moveArmy = value; }
        }

        Random randomNumber;

        public MainGameArea(int gameAreaBlockWidth, int gameAreaBlockHeight)
        {
            //setup main game area
            gameAreastartingPosition = new Vector2(600, 100);
            gameAreaBlocksAcross = 30;
            gameAreaBlocksDown = 20;

            this.gameAreaBlockWidth = gameAreaBlockWidth;
            this.gameAreaBlockHeight = gameAreaBlockHeight;

            gameArea = new Block[gameAreaBlocksAcross, gameAreaBlocksDown];

            gameAreaBound0 = gameArea.GetUpperBound(0);
            gameAreaBound1 = gameArea.GetUpperBound(1);

            for (int i = 0; i <= gameAreaBound0; i++)
            {
                for (int j = 0; j <= gameAreaBound1; j++)
                {

                    Vector2 position = new Vector2(gameAreastartingPosition.X + i * gameAreaBlockWidth, gameAreastartingPosition.Y + j * gameAreaBlockHeight);
                    gameArea[i, j] = new Block(position);

                }
            }
            active = false;

            buildings = new List<Building>();

            armyUnits = new List<ArmyUnit>();

            randomNumber = new Random();

            armyAtThisSector = 0;
            people = 0;

            moveArmy = false;

        }

        public void AddBuilding()
        {
            //add a building
            Building aBuilding = new Building(new Vector2(gameAreastartingPosition.X + (40 * 5), gameAreastartingPosition.Y + (40 * 4)));
            buildings.Add(aBuilding);
        }

        public void addArmyUnit()
        {
                //add army unit
                ArmyUnit aArmyUnit = new ArmyUnit(new Vector2(
                                                            randomNumberGenerator((int)gameAreastartingPosition.X,
                                                            ((int)gameAreastartingPosition.X + (40 * gameAreaBlocksAcross)-40)),
                                                            randomNumberGenerator((int)gameAreastartingPosition.Y,
                                                            (int)gameAreastartingPosition.Y + (40 * gameAreaBlocksDown)-40)));
                armyUnits.Add(aArmyUnit);
        }

        public int randomNumberGenerator(int minNumber, int maxNumber)
        {
            return randomNumber.Next(minNumber, maxNumber);
        }

        public void updateArmyUnit()
        {
            //keep track of units to add/remove
            //if list is empty, add the units.

            if (armyUnits.Count < armyAtThisSector)
            {
                addArmyUnit();
            }
            if (armyUnits.Count > armyAtThisSector)
            {
               armyUnits.RemoveAt( armyUnits.IndexOf(armyUnits.Last()));
            }

        }

        public void updateArmyUnitMovement()
        {
            for (int i = 0; i < armyUnits.Count; i++)
            {
                if (!armyUnits[i].IsDestinationSet)
                {
                    armyUnits[i].setDestination(randomNumberGenerator((int)gameAreastartingPosition.X, (int)gameAreastartingPosition.X + ((gameAreaBlocksAcross -1) * gameAreaBlockWidth)), randomNumberGenerator((int)gameAreastartingPosition.Y, (int)gameAreastartingPosition.Y +((gameAreaBlocksDown-1) * gameAreaBlockHeight)));
                    armyUnits[i].IsDestinationSet = true;
                }
                if (Vector2.Distance(armyUnits[i].UnitPosition, armyUnits[i].Destination) < 40.0)
                {
                    armyUnits[i].IsDestinationSet = false;
                }
                if (armyUnits[i].IsDestinationSet)
                {
                    armyUnits[i].UpdateRotation();
                    armyUnits[i].UpdateMovement();
                }
                
            }
        }


    }
}
