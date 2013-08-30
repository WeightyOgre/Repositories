using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace MapAndUIConcept
{
    class ArmyUnit
    {

        Vector2 unitPosition;
        public Vector2 UnitPosition
        {
            get { return unitPosition; }
            set { unitPosition = value; }
        }

        private float Speed = 1.0f;

        public float rotation;


        Vector2 destination;
        public Vector2 Destination
        {
            get { return destination; }
            set { destination = value; }
        }

        bool isDestinationSet;
        public bool IsDestinationSet
        {
            get { return isDestinationSet; }
            set { isDestinationSet = value; }
        }

        public ArmyUnit(Vector2 unitPosition)
        {
            this.unitPosition = unitPosition;
            
            
            rotation = 0f;
           
            
            isDestinationSet = false;
        }


        public void UpdateMovement()
        {
            //http://gamedev.stackexchange.com/questions/50793/moving-a-sprite-in-the-direction-its-facing-xna
            Vector2 direction = new Vector2((float)Math.Cos(rotation),
                                            (float)Math.Sin(rotation));

            direction.Normalize();
            unitPosition += direction * Speed;
            
        }

        public void setDestination(float targetX, float targetY)
        {
            destination.X = targetX;
            destination.Y = targetY;
        }

        public void UpdateRotation()
        {
            float deltaX = (destination.X) - (unitPosition.X);
            float deltaY = (destination.Y) - (unitPosition.Y);

            float angle = (float)Math.Atan2(deltaY, deltaX);
            //float offSet = (90.0f / 360) * MathHelper.Pi * 2;
            //angle += offSet;

            rotation = angle;
        }

    }
}
