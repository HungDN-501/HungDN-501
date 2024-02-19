using Microsoft.AspNetCore.Mvc;
using WebBanHang.Repository;

namespace WebBanHang.ViewComponents
{
    public class LoaiSpMenuViewComponent : ViewComponent
    {
        private readonly ILoaiSpRepository _loaiSp;

        public LoaiSpMenuViewComponent(ILoaiSpRepository loaiSp)
        {
            _loaiSp = loaiSp;
        }

        // 
        public IViewComponentResult Invoke() 
        { 
            var loaiSp = _loaiSp.GetAllLoaiSp().OrderBy(x => x.Loai);
            return View(loaiSp);
        }
    }
}
