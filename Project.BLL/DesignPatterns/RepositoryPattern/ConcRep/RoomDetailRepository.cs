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
    public class RoomDetailRepository : BaseRepository<RoomDetail>
    {
        MyContext _db;
        public RoomDetailRepository()
        {
            _db = DBTool.DBInstance;
        }

        public List<RoomDetail> GetRoomById(int i)
        {
             
            List<RoomDetail> rd = _db.Set<RoomDetail>().Where(x => x.RoomID == i).ToList();

            return rd;
        }

        public List<RoomDetail> GetRoomsForMaintenance(int i)
        {

            List<RoomDetail> rd = _db.Set<RoomDetail>().Where(x => x.RoomID == i && x.isMaintenance == false && x.Situation == true).ToList();

            return rd;
        }

        public List<RoomDetail> GetRoomsForUnMaintenance(int i)
        {

            List<RoomDetail> rd = _db.Set<RoomDetail>().Where(x => x.RoomID == i && x.isMaintenance == true).ToList();

            return rd;
        }

        // GetMaintenanceRoomsfromStaff
        public List<RoomDetail> GetMaintenanceRoomsfromStaff()
        {

            List<RoomDetail> rd = _db.Set<RoomDetail>().Where(x => x.isMaintenance == true).ToList();

            return rd;
        }
    }
}
