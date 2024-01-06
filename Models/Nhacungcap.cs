using System;
using System.Collections.Generic;

namespace WebSellPhoneAPI.Entities;

public partial class Nhacungcap
{
    public int Id { get; set; }

    public string? TenNcc { get; set; }

    public string? Diachi { get; set; }

    public sbyte? Trangthai { get; set; }

    public virtual ICollection<Sanpham> Sanphams { get; set; } = new List<Sanpham>();
}
