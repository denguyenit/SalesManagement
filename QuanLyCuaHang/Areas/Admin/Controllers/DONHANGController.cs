using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using QuanLyCuaHang.Models;

namespace QuanLyCuaHang.Areas.Admin.Controllers
{
    public class DONHANGController : Controller
    {
        private qlbanhangEntities1 db = new qlbanhangEntities1();

        public ActionResult Index()
        {
            return View(db.DONHANGs.ToList());
        }

        public ActionResult Donhangtrongngay()
        {
            return View(db.DONHANGs.Where(m=>m.NgayDat>=DateTime.Today).ToList());
        }
        public ActionResult Xemdonhang(int madh)
        {
            DONHANG dh = db.DONHANGs.Where(m => m.MaDH == madh).FirstOrDefault();
            ViewBag.DONHANGs = dh;
            var ctdh = db.CHITIETDONHANGs.Where(m => m.MaDH == madh).ToList();
            ViewBag.Chitiet = ctdh;
            var dssp = db.SANPHAMs.ToList();
            ViewBag.Dssanpham = dssp;
            return View();
        }

        public ActionResult Xacnhandon(int madh)
        {
            var dh = db.DONHANGs.Where(m => m.MaDH == madh).FirstOrDefault();
            dh.TinhTrang = "Đã xử lý";
            db.Entry(dh).State = EntityState.Modified;

            List<CHITIETDONHANG> listct = db.CHITIETDONHANGs.Where(m => m.MaDH == madh).ToList();
            foreach (var t in listct)
            {
                SANPHAM sp = db.SANPHAMs.Where(x => x.MaSP == t.MaSP).FirstOrDefault();
                sp.SoLuongTon -= t.SoluongDat;
                db.Entry(sp).State = EntityState.Modified;
            }
            db.SaveChanges();
            return RedirectToAction("Index", "DONHANG");
        }

        public ActionResult Huydon(int madh)
        {
            var sp = db.DONHANGs.Where(m => m.MaDH == madh).FirstOrDefault();
            sp.TinhTrang = "Hủy đơn";
            db.Entry(sp).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index", "DONHANG");
        }

        public bool CapnhatTonkho(int masp, int soluong)
        {
            SANPHAM sp = db.SANPHAMs.FirstOrDefault(m => m.MaSP == masp);
            sp.SoLuongTon = soluong;
            db.Entry(sp).State = EntityState.Modified;
            db.SaveChanges();
            return true;
        }

        [HttpPost]
        public ActionResult Xuatthongtingiaohang(int madh)
        {
            var gv = new GridView();
            gv.DataSource = db.DONHANGs.Where(m=>m.MaDH==madh).ToList();
            gv.DataBind();
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=Thongtingiaohang.xls");
            Response.ContentType = "application/ms-excel";
            Response.Charset = "utf-8";    
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.BinaryWrite(System.Text.Encoding.UTF8.GetPreamble());
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            for (int i = 0; i < gv.Rows.Count; i++)
            {
                gv.Rows[i].Attributes.Add("class", "textmode");
            }

            gv.HeaderRow.BackColor = System.Drawing.Color.DarkBlue;
            gv.HeaderStyle.ForeColor = System.Drawing.Color.White;

            gv.HeaderRow.Cells[0].Text = "Mã đơn hàng";
            gv.HeaderRow.Cells[1].Text = "Ngày đặt";
            gv.HeaderRow.Cells[2].Text = "Mã khách hàng";
            gv.HeaderRow.Cells[3].Text = "Tên người đặt";
            gv.HeaderRow.Cells[4].Text = "Email";
            gv.HeaderRow.Cells[5].Text = "SĐT";
            gv.HeaderRow.Cells[6].Text = "Địa chỉ";
            gv.HeaderRow.Cells[7].Text = "Tình trạng";
            gv.RenderControl(hw);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
            return RedirectToAction("Index", "DONHANG");
        }
        [HttpPost]
        public ActionResult Xuatchitietdon(int madh)
        {
            var gv = new GridView();
            gv.DataSource = db.CHITIETDONHANGs.Where(m => m.MaDH == madh).ToList();
            gv.DataBind();
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=Thongtindonhang.xls");
            Response.ContentType = "application/ms-excel";
            Response.Charset = "utf-8";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.BinaryWrite(System.Text.Encoding.UTF8.GetPreamble());
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            for (int i = 0; i < gv.Rows.Count; i++)
            {
                //Apply text style to each Row
                gv.Rows[i].Attributes.Add("class", "textmode");
            }
            //Add màu nền cho header của file excel
            gv.HeaderRow.BackColor = System.Drawing.Color.DarkBlue;
            //Màu chữ cho header của file excel
            gv.HeaderStyle.ForeColor = System.Drawing.Color.White;

            gv.HeaderRow.Cells[0].Text = "";
            gv.HeaderRow.Cells[1].Text = "Mã đơn hàng";
            gv.HeaderRow.Cells[2].Text = "Mã sản phẩm";
            gv.HeaderRow.Cells[3].Text = "Số lượng đặt";
            gv.RenderControl(hw);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
            return RedirectToAction("Index", "DONHANG");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }      
    }
}
