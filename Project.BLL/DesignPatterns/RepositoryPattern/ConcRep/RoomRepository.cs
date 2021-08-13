using Project.BLL.DesignPatterns.RepositoryPattern.BaseRep;
using Project.BLL.DesignPatterns.SingletonPattern;
using Project.DAL.Context;
using Project.ENTITIES.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.BLL.DesignPatterns.RepositoryPattern.ConcRep
{
    public class RoomRepository : BaseRepository<Room>
    {
        MyContext _db;
        public RoomRepository()
        {
            _db = DBTool.DBInstance;
        }

        public List<Room> GetAvailableRooms()
        {
            return _db.Set<Room>().Where(x => x.RoomAvailable > 0).ToList();
        }

        public Room GetRoomById(int id)
        {
            return _db.Set<Room>().Where(x => x.ID == id).FirstOrDefault();
        }

    }
}
