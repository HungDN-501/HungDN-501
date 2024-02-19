using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebBanHang.Models;
using X.PagedList;

namespace WebBanHang.Areas.Admin.Controllers
{
    // Xác định controller thuộc về vùng(area) có tên là "admin"
    [Area("admin")]
    // Xác định route mặc định cho controller HomeAdminController trong vùng "admin". Khi một yêu cầu được gửi đến "/admin", nó sẽ được xử lý bởi controller này
    [Route("admin")]
    // Xác định một route khác cho controller HomeAdminController trong vùng "admin". Khi một yêu cầu được gửi đến "/admin/homeadmin", nó cũng sẽ được xử lý bởi controller này
    [Route("admin/homeadmin")]
    public class HomeAdminController : Controller
    {
        QlbanValiContext db = new QlbanValiContext();

        // Xác định rằng phương thức là mặc định
        [Route("")]
        // Xác định rằng phương thức Index cũng có thể xử lý yêu cầu tới route "/index" khi có yêu cầu
        [Route("index")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("danhmucsanpham")]
        public IActionResult DanhMucSanPham(int? page)
        {
            int pageSize = 12;
            int pageIndex = (page == null || page < 0 ? 1 : page.Value);

            //// Get list data
            //var lstSanPham = db.TDanhMucSps.ToList();

            // AsNoTracking() : chỉ định các đối tượng trả về từ truy vấn sẽ không được theo dõi bởi context để cải thiện hiệu suất
            var lstSanPham = db.TDanhMucSps.AsNoTracking().OrderBy(x => x.TenSp).ToList();

            // Phân trang bằng Pub "PagedList"
            PagedList<TDanhMucSp> lst = new PagedList<TDanhMucSp>(lstSanPham, pageIndex, pageSize);

            return View(lst);
        }

        // Lay thong tin Sp moi tao
        [Route("ThemSanPhamMoi")]
        [HttpGet]
        public IActionResult ThemSanPhamMoi()
        {
            ViewBag.MaChatLieu = new SelectList(db.TChatLieus.ToList(),"MaChatLieu","ChatLieu");
            ViewBag.MaHangSx = new SelectList(db.THangSxes.ToList(), "MaHangSx", "HangSx");
            ViewBag.MaNuocSx = new SelectList(db.TQuocGia.ToList(), "MaNuoc", "TenNuoc");
            ViewBag.MaLoai = new SelectList(db.TLoaiSps.ToList(), "MaLoai", "Loai");
            ViewBag.MaDt = new SelectList(db.TLoaiDts.ToList(), "MaDt", "TenLoai");
            return View();
        }

        // Luu Sp moi tao vao csdl
        [Route("ThemSanPhamMoi")]
        [HttpPost]
        // kiểm tra xem request gửi đến action đó có chứa thông tin xác thực từ một nguồn tin cậy hay không. Nếu thông tin xác thực không hợp lệ, request sẽ bị từ chối và action sẽ không được thực thi
        [ValidateAntiForgeryToken]
        public IActionResult ThemSanPhamMoi(TDanhMucSp sanPham)
        {
            // Nếu model hợp lệ
            if (ModelState.IsValid)
            {
                db.TDanhMucSps.Add(sanPham);
                db.SaveChanges();
                // Load lại trang
                return RedirectToAction("DanhMucSanPham");
            }
            return View();
        }

        // Lay thong tin Sp moi sua
        [Route("SuaSanPham")]
        [HttpGet]
        public IActionResult SuaSanPham(string maSanPham)
        {
            ViewBag.MaChatLieu = new SelectList(db.TChatLieus.ToList(), "MaChatLieu", "ChatLieu");
            ViewBag.MaHangSx = new SelectList(db.THangSxes.ToList(), "MaHangSx", "HangSx");
            ViewBag.MaNuocSx = new SelectList(db.TQuocGia.ToList(), "MaNuoc", "TenNuoc");
            ViewBag.MaLoai = new SelectList(db.TLoaiSps.ToList(), "MaLoai", "Loai");
            ViewBag.MaDt = new SelectList(db.TLoaiDts.ToList(), "MaDt", "TenLoai");

            var sanPham = db.TDanhMucSps.Find(maSanPham);

            return View(sanPham);
        }

        // Luu Sp moi sua vao csdl
        [Route("SuaSanPham")]
        [HttpPost]
        // kiểm tra xem request gửi đến action đó có chứa thông tin xác thực từ một nguồn tin cậy hay không. Nếu thông tin xác thực không hợp lệ, request sẽ bị từ chối và action sẽ không được thực thi
        [ValidateAntiForgeryToken]
        public IActionResult SuaSanPham(TDanhMucSp sanPham)
        {
            // Nếu model hợp lệ
            if (ModelState.IsValid)
            {
                db.Update(sanPham);
                db.SaveChanges();
                // chuyển đến action "DanhMucSanPham" trong controller "HomeAdmin"
                return RedirectToAction("DanhMucSanPham","HomeAdmin");
            }
            return View(sanPham);
        }

        // Lay thong tin Sp moi xóa
        [Route("XoaSanPham")]
        [HttpGet]
        public IActionResult XoaSanPham(string maSanPham)
        {
            // TempData : đưa ra thông tin cho người dùng
            TempData["Message"] = "";

            // Kiem tra dmsp cos trong bang ChiTietSanPham khong
            var chiTietSanPhams = db.TChiTietSanPhams.Where(x => x.MaSp == maSanPham).ToList();
            if (chiTietSanPhams.Count() > 0)
            {
                TempData["Message"] = "Không xóa được sản phẩm này";
                // chuyển đến action "DanhMucSanPham" trong controller "HomeAdmin"
                return RedirectToAction("DanhMucSanPham", "HomeAdmin");
            }

            // xóa tất cả dl trong bảng TAnhSps có maSanPham
            var anhSanPhams = db.TAnhSps.Where(x => x.MaSp == maSanPham);
            if (anhSanPhams.Any()) db.RemoveRange(anhSanPhams);

            // xóa DanhMucSp
            db.Remove(db.TDanhMucSps.Find(maSanPham));
            db.SaveChanges();

            TempData["Message"] = "Sản phẩm đã được xóa";
            // chuyển đến action "DanhMucSanPham" trong controller "HomeAdmin"
            return RedirectToAction("DanhMucSanPham", "HomeAdmin");
        }
    }
}
