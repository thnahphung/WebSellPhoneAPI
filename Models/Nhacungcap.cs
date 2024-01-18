using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebSellPhoneAPI.Models;

public partial class Nhacungcap
{
    public int Id { get; set; }

    public string? TenNcc { get; set; }

    public string? Diachi { get; set; }

    public sbyte? Trangthai { get; set; }

    public virtual ICollection<Sanpham> Sanphams { get; set; } = new List<Sanpham>();
    [NotMapped]
    public string? TepHinhAnh { get; set; }
    [NotMapped]
    public string? TenTepHinhAnh { get; set; }
}
