using System;
using System.Collections.Generic;

namespace WebSellPhoneAPI.Models;

public partial class Chitietdonhang
{
    public int Id { get; set; }

    public int? IdDh { get; set; }

    public int? IdSp { get; set; }

    public int? Soluong { get; set; }

    public double? Dongia { get; set; }

    public sbyte? Trangthai { get; set; }

    public virtual Donhang? IdDhNavigation { get; set; }

    public virtual Sanpham? IdSpNavigation { get; set; }
}
