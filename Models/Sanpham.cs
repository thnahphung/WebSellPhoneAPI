using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebSellPhoneAPI.Models;

public partial class Sanpham
{
    public int Id { get; set; }

    public string? TenSp { get; set; }

    public int? IdNcc { get; set; }

    public string? Thuonghieu { get; set; }

    public double? Giadagiam { get; set; }

    public double? Giagoc { get; set; }

    public int? Soluong { get; set; }

    public string? Mausanpham { get; set; }

    public string? Manhinh { get; set; }

    public string? Hedieuhanh { get; set; }

    public string? Camera { get; set; }

    public string? Chip { get; set; }

    public string? Ram { get; set; }

    public string? Dungluong { get; set; }

    public string? Pin { get; set; }

    public string? Mota { get; set; }

    public string? Tenviettat { get; set; }

    public sbyte? Trangthai { get; set; }

    public virtual ICollection<Binhluan> Binhluans { get; set; } = new List<Binhluan>();

    public virtual ICollection<Chitietdonhang> Chitietdonhangs { get; set; } = new List<Chitietdonhang>();

    public virtual ICollection<Chitietgiohang> Chitietgiohangs { get; set; } = new List<Chitietgiohang>();

    public virtual ICollection<Hinhanh> Hinhanhs { get; set; } = new List<Hinhanh>();

    public virtual Nhacungcap? IdNccNavigation { get; set; }
    [NotMapped]
    public string? TepHinhAnh { get; set; }
    [NotMapped]
    public string? TenTepHinhAnh { get; set; }
}
