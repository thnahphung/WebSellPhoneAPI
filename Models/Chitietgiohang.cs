using System;
using System.Collections.Generic;

namespace WebSellPhoneAPI.Models;

public partial class Chitietgiohang
{
    public int Id { get; set; }

    public int? IdNd { get; set; }

    public int? IdSp { get; set; }

    public int? Soluong { get; set; }

    public sbyte? Trangthai { get; set; }

    public virtual Nguoidung? IdNdNavigation { get; set; }

    public virtual Sanpham? IdSpNavigation { get; set; }
}
