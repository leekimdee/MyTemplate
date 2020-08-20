using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyTemplate.Models
{
    public class HopDong
    {
        public int Id { get; set; }

        public string SoHopDong { get; set; }

        public int LoaiHopDongID { get; set; }

        public DateTime NgayKi { get; set; }

        public string BenA { get; set; }

        public int BenB { get; set; }

        public DateTime NgayBatDau { get; set; }

        public DateTime NgayKetThuc { get; set; }

        public DateTime? NgayTao { get; set; }

        public string NguoiTao { get; set; }

        public DateTime? NgaySua { get; set; }

        public string NguoiSua { get; set; }

        public decimal? TongTienChuaThue { get; set; }

        public decimal? TongTienSauThue { get; set; }

        public int TrangThaiID { get; set; }

        public string GhiChuTrangThai { get; set; }

        public string GhiChu { get; set; }

        public int? KieuHopDong { get; set; }

        public string KiHieuHopDong { get; set; }

        public int? BaoGiaID { get; set; }

        public int? NhanSuId { get; set; }

        public bool? IsDetele { get; set; }
    }
}