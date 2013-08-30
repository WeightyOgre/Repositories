using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MapAndUIConcept
{
    class UserInterface
    {
        Block[,] uiArea;
        public Block[,] UIArea
        {
            get { return uiArea; }
            set { uiArea = value; }
        }

        int uiAreaBlocksAcross;
        int uiAreaBlocksDown;

        int uiAreaBound0;
        public int UIAreaBound0
        {
            get { return uiAreaBound0; }
            set { uiAreaBound0 = value; }
        }
        int uiAreaBound1;
        public int UIAreaBound1
        {
            get { return uiAreaBound1; }
            set { uiAreaBound1 = value; }
        }

        int uiAreaBlockWidth;
        int uiAreaBlockHeight;

        Vector2 uiAreastartingPosition;
        public Vector2 UIAreaStartingPosition
        {
            get { return uiAreastartingPosition; }
            set { uiAreastartingPosition = value; }
        }

        //icons
        Vector2 personIcon;
        public Vector2 PersonIcon
        {
            get { return personIcon; }
            set { personIcon = value; }
        }

        Vector2 rightArrowIconPosition;
        public Vector2 RightArrowIconPosition
        {
            get { return rightArrowIconPosition; }
            set { rightArrowIconPosition = value; }
        }

        Vector2 armyIconPosition;
        public Vector2 ArmyIconPosition
        {
            get { return armyIconPosition; }
            set { armyIconPosition = value; }
        }

        Vector2 leftArrowIconPosition;
        public Vector2 LeftArrowIconPosition
        {
            get { return leftArrowIconPosition; }
            set { leftArrowIconPosition = value; }
        }

        Vector2 researchIconPosition;
        public Vector2 ResearchIconPosition
        {
            get { return researchIconPosition; }
            set { researchIconPosition = value; }
        }

        Vector2 downArrowIconPosition;
        public Vector2 DownArrowIconPosition
        {
            get { return downArrowIconPosition; }
            set { downArrowIconPosition = value; }
        }

        Vector2 rockIconPosition;
        public Vector2 RockIconPosition
        {
            get { return rockIconPosition; }
            set { rockIconPosition = value; }
        }

        Vector2 personWithRockIconPosition;
        public Vector2 PersonWIthROckIconPosition
        {
            get { return personWithRockIconPosition; }
            set { personWithRockIconPosition = value; }
        }

        uiState aUIState;
        public enum uiState
        {
            main,
            military
        }

        public UserInterface(int uiAreaBlockWidth, int uiAreaBlockHeight)
        {
            //setup UI area
            uiAreastartingPosition = new Vector2(80, 300);
            uiAreaBlocksAcross = 10;
            uiAreaBlocksDown = 15;

            this.uiAreaBlockWidth = uiAreaBlockWidth;
            this.uiAreaBlockHeight = uiAreaBlockHeight;

            uiArea = new Block[uiAreaBlocksAcross, uiAreaBlocksDown];

            uiAreaBound0 = uiArea.GetUpperBound(0);
            uiAreaBound1 = uiArea.GetUpperBound(1);

            for (int i = 0; i <= uiAreaBound0; i++)
            {
                for (int j = 0; j <= uiAreaBound1; j++)
                {

                    Vector2 position = new Vector2(uiAreastartingPosition.X + i * uiAreaBlockWidth, uiAreastartingPosition.Y + j * uiAreaBlockHeight);
                    uiArea[i, j] = new Block(position);

                }
            }
            personIcon = new Vector2(uiAreastartingPosition.X+(40*4), uiAreastartingPosition.Y+(40*5));
            rightArrowIconPosition = new Vector2(uiAreastartingPosition.X + (40 * 5), uiAreastartingPosition.Y + (40 * 5));
            armyIconPosition = new Vector2(uiAreastartingPosition.X + (40 * 6), uiAreastartingPosition.Y + (40 * 5));
            leftArrowIconPosition = new Vector2(uiAreastartingPosition.X + (40 * 3), uiAreastartingPosition.Y + (40 * 5));
            researchIconPosition = new Vector2(uiAreastartingPosition.X + (40 * 2), uiAreastartingPosition.Y + (40 * 5));
            downArrowIconPosition = new Vector2(uiAreastartingPosition.X + (40 * 400), uiAreastartingPosition.Y + (40 * 300));
            rockIconPosition = new Vector2(uiAreastartingPosition.X + (40 * 400), uiAreastartingPosition.Y + (40 * 300));
            personWithRockIconPosition = new Vector2(uiAreastartingPosition.X + (40 * 400), uiAreastartingPosition.Y + (40 * 300));

            aUIState = uiState.main;

       }

        public bool isMain()
        {
            if (aUIState == uiState.main)
            {
                return true;
            }
            return false;
        }

        public void setToMain()
        {
            personIcon = new Vector2(uiAreastartingPosition.X + (40 * 4), uiAreastartingPosition.Y + (40 * 5));
            rightArrowIconPosition = new Vector2(uiAreastartingPosition.X + (40 * 5), uiAreastartingPosition.Y + (40 * 5));
            armyIconPosition = new Vector2(uiAreastartingPosition.X + (40 * 6), uiAreastartingPosition.Y + (40 * 5));
            leftArrowIconPosition = new Vector2(uiAreastartingPosition.X + (40 * 3), uiAreastartingPosition.Y + (40 * 5));
            researchIconPosition = new Vector2(uiAreastartingPosition.X + (40 * 2), uiAreastartingPosition.Y + (40 * 5));
            downArrowIconPosition = new Vector2(uiAreastartingPosition.X + (40 * 400), uiAreastartingPosition.Y + (40 * 300));
            rockIconPosition = new Vector2(uiAreastartingPosition.X + (40 * 400), uiAreastartingPosition.Y + (40 * 300));
            personWithRockIconPosition = new Vector2(uiAreastartingPosition.X + (40 * 400), uiAreastartingPosition.Y + (40 * 300));
            aUIState = uiState.main;
        }

        public bool isMilitary()
        {
            if (aUIState == uiState.military)
            {
                return true;
            }
            return false;
        }

        public void setToMilitary()
        {
            personIcon = new Vector2(uiAreastartingPosition.X + (40 * 4), uiAreastartingPosition.Y + (40 * 5));
            rightArrowIconPosition = new Vector2(uiAreastartingPosition.X + (40 * 5), uiAreastartingPosition.Y + (40 * 8));
            armyIconPosition = new Vector2(uiAreastartingPosition.X + (40 * 600), uiAreastartingPosition.Y + (40 * 5));
            leftArrowIconPosition = new Vector2(uiAreastartingPosition.X + (40 * 300), uiAreastartingPosition.Y + (40 * 5));
            researchIconPosition = new Vector2(uiAreastartingPosition.X + (40 * 200), uiAreastartingPosition.Y + (40 * 5));
            downArrowIconPosition = new Vector2(uiAreastartingPosition.X + (40 * 4), uiAreastartingPosition.Y + (40 * 7));
            rockIconPosition = new Vector2(uiAreastartingPosition.X + (40 * 4), uiAreastartingPosition.Y + (40 * 8));
            personWithRockIconPosition = new Vector2(uiAreastartingPosition.X + (40 * 6), uiAreastartingPosition.Y + (40 * 8));
            aUIState = uiState.military;
        }

        public void updateUI()
        {

        }

    }
}
