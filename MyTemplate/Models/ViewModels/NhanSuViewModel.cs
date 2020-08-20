using System;
using System.Collections.Generic;
using System.Linq;

namespace MyTemplate.Models.ViewModels
{
    public class NhanSuViewModel
    {
        public IEnumerable<NhanSu> NhanSues { get; set; }
        public int ItemsPerPage { get; set; }
        public int CurrentPage { get; set; }

        public int PageCount()
        {
            return Convert.ToInt32(Math.Ceiling(NhanSues.Count() / (double)ItemsPerPage));
        }

        public IEnumerable<NhanSu> PaginatedNhanSues()
        {
            int start = (CurrentPage - 1) * ItemsPerPage;
            return NhanSues.Skip(start).Take(ItemsPerPage);
        }
    }
}