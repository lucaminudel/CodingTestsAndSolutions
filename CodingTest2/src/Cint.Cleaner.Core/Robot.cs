using System;

namespace Cint.Cleaner.Core
{
    public class Robot
    {
        private readonly IOfficeSpace _office;
        private Point _currentPosition;

        public Robot(int startingPositionX, int startingPositionY)
            : this (startingPositionX, startingPositionY, new OfficeSpace())
        {
        }

        public Robot(int startingPositionX, int startingPositionY, IOfficeSpace office)
        {
            _office = office;
            _currentPosition = new Point(startingPositionX, startingPositionY);
            _office.SetPlaceCleaned(_currentPosition);
        }

        public int CurrentPositionX
        {
            get { return _currentPosition.X; }
        }


        public int CurrentPositionY
        {
            get { return _currentPosition.Y; }
        }

        public long CleanedPlacesCount
        {
            get { return _office.CleanedPlacesCount; }
        }


        public void MoveForward(MoveForwardCommand moveCommand)
        {

            Point moveDirection = (Point)moveCommand.Direction * Math.Sign(moveCommand.Steps);
            int distance = Math.Abs(moveCommand.Steps);

            for (int cleanOperation = 1; cleanOperation <= distance; ++cleanOperation)
            {
                _currentPosition = _currentPosition + moveDirection;
                _office.SetPlaceCleaned(_currentPosition);
            }            
        }
    }
}