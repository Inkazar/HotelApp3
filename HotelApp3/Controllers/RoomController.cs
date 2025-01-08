using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace HotelApp3.Controllers
{
    public class RoomController
    {
        private readonly RoomFunctions _functions;

        public RoomController(RoomFunctions functions)
        {
            _functions = functions;
        }

        public void AddRoom() => _functions.AddRoom();

        public void ViewAllRooms() => _functions.ViewAllRooms();

        public void UpdateRoom() => _functions.UpdateRoom();

        public void DeleteRoom() => _functions.DeleteRoom();

        public void AddExtraBed() => _functions.AddExtraBed();

        public void RemoveExtraBed() => _functions.RemoveExtraBed();
    }
}
