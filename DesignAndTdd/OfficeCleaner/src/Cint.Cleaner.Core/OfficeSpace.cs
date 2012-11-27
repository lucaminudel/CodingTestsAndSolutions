using System;
using System.Collections.Generic;

namespace Cint.Cleaner.Core
{
    public class OfficeSpace : IOfficeSpace
    {
        private Dictionary<Point, object> _cleanedPlaces = new Dictionary<Point, object>();

        public long CleanedPlacesCount
        {
            get
            {
                return _cleanedPlaces.Count;
            }
        }

        public void SetPlaceCleaned(Point placeCleaned)
        {
            _cleanedPlaces[placeCleaned] = null;                
        }
    }
}
