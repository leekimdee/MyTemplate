using System;

namespace MyTemplate.Models
{
    public class NhanSu
    {
        public string stt { get; set; }
        public string HoVaTenDem { get; set; }
        public string Ten { get; set; }
        public string GioiTinh { get; set; }
        public string PhongBan { get; set; }
        public string DanToc { set; get; }
        public string TonGiao { set; get; }
        public DateTime NgaySinh { get; set; }
        public string StrNgaySinh { get; set; }
    }
}