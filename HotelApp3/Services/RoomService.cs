using HotelApp3.Models;
using HotelApp3.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelApp3.Services
{
    public class RoomService
    {
        private readonly RoomRepository _repository;

        public RoomService(RoomRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Room> GetAllRooms()
        {
            return _repository.GetAllRooms().ToList();
        }

        public Room GetRoomById(int id)
        {
            return _repository.GetRoomById(id);
        }

        public void AddRoom(Room room)
        {
            _repository.AddRoom(room);
        }

        public void UpdateRoom(Room room)
        {
            _repository.UpdateRoom(room);
        }

        public void DeleteRoom(int id)
        {
            _repository.DeleteRoom(id);
        }
    }
}
