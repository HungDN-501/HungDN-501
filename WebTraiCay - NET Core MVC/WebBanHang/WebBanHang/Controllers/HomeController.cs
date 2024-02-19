using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WebBanHang.Models;
using WebBanHang.Models.Authentication;
using WebBanHang.ViewModels;
using X.PagedList;

namespace WebBanHang.Controllers
{
    public class HomeController : Controller
    {
        // Tao Interface ILogger de ghi lai Log cua Controller
        private readonly ILogger<HomeController> _logger;

        // Tao Db Db
        private readonly QlbanValiContext db;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            this.db = new QlbanValiContext();
        }

        // Sử dụng Authentication tự tạo để kiểm tra login chưa thì mới cho vào
        //[Authentication]
        public IActionResult Index(int? page)
        {
            int pageSize = 8;
            int pageIndex = (page == null || page < 0 ? 1 : page.Value);

            //// Get list data
            //var lstSanPham = db.TDanhMucSps.ToList();

            // AsNoTracking() : chỉ định các đối tượng trả về từ truy vấn sẽ không được theo dõi bởi context để cải thiện hiệu suất
            var lstSanPham = db.TDanhMucSps.AsNoTracking().OrderBy(x => x.TenSp).ToList();

            Console.WriteLine("Reload");

            // Phân trang bằng Pub "PagedList"
            PagedList<TDanhMucSp> lst = new PagedList<TDanhMucSp>(lstSanPham, pageIndex, pageSize);

            return View(lst);
        }


        public IActionResult SanPhamTheoLoai (String maLoai, int? page)
        {
            int pageSize = 8;
            int pageIndex = (page == null || page < 0 ? 1 : page.Value);

            // AsNoTracking() : chỉ định các đối tượng trả về từ truy vấn sẽ không được theo dõi bởi context để cải thiện hiệu suất
            var lstSanPham = db.TDanhMucSps.AsNoTracking().Where(x => x.MaLoai == maLoai).OrderBy(x => x.TenSp).ToList();

            // Phân trang bằng Pub "PagedList"
            PagedList<TDanhMucSp> lst = new PagedList<TDanhMucSp>(lstSanPham, pageIndex, pageSize);

            // Tao ViewBag de View co the lay thong tin
            ViewBag.maloai = maLoai;

            return View(lst);
        }

        // Control ChiTietSanPham bang ViewBag
        public IActionResult ChiTietSanPham(String maSp)
        {
            var sanPham = db.TDanhMucSps.SingleOrDefault(x => x.MaSp == maSp);
            var anhSanPham = db.TAnhSps.Where(x => x.MaSp == maSp).ToList();

            // Tao ViewBag de View co the lay thong tin
            ViewBag.anhSanPham = anhSanPham;

            return View(sanPham);
        }

        // Control ChiTietSanPham bang ViewModel
        public IActionResult ProductDetail(String maSp)
        {
            var sanPham = db.TDanhMucSps.SingleOrDefault(x => x.MaSp == maSp);
            var anhSanPham = db.TAnhSps.Where(x => x.MaSp == maSp).ToList();

            // Tao Model va truyen tham so
            var homeProductDetailViewModel = new HomeProductDetailViewModel
            {
                danhMucSp = sanPham,
                anhSps = anhSanPham
            };

            return View(homeProductDetailViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        // [ResponseCache] : xác định cách mà response từ action này sẽ được lưu trữ cache
        // Duration = 0 : response sẽ không được cache.
        // Location = ResponseCacheLocation.None : không có vị trí cụ thể nào được chỉ định cho việc lưu trữ cache
        // NoStore = true : response không được lưu trữ trong bất kỳ nơi nào
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}