using WebBanHang.Models;

namespace WebBanHang.Repository
{
    public class LoaiSpRepository : ILoaiSpRepository
    {
        private readonly QlbanValiContext db;
        public LoaiSpRepository(QlbanValiContext db)
        {
            this.db = db;
        }

        public TLoaiSp Add(TLoaiSp loaiSp)
        {
            db.TLoaiSps.Add(loaiSp);
            db.SaveChanges();
            return loaiSp;
        }

        public TLoaiSp Delete(string maLoaiSp)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TLoaiSp> GetAllLoaiSp()
        {
            return db.TLoaiSps;
        }

        public TLoaiSp GetLoaiSp(string maLoaiSp)
        {
            return db.TLoaiSps.Find(maLoaiSp);
        }

        public TLoaiSp Update(TLoaiSp loaiSp)
        {
            db.Update(loaiSp);
            db.SaveChanges();
            return loaiSp;
        }
    }
}
