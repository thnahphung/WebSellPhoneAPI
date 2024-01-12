using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebSellPhoneAPI.Models;

public partial class SellPhoneContext : DbContext
{
    public SellPhoneContext()
    {
    }

    public SellPhoneContext(DbContextOptions<SellPhoneContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Binhluan> Binhluans { get; set; }

    public virtual DbSet<Chitietdonhang> Chitietdonhangs { get; set; }

    public virtual DbSet<Chitietgiohang> Chitietgiohangs { get; set; }

    public virtual DbSet<Diachi> Diachis { get; set; }

    public virtual DbSet<Donhang> Donhangs { get; set; }

    public virtual DbSet<Hinhanh> Hinhanhs { get; set; }

    public virtual DbSet<Lienhe> Lienhes { get; set; }

    public virtual DbSet<Log> Logs { get; set; }

    public virtual DbSet<Nguoidung> Nguoidungs { get; set; }

    public virtual DbSet<Nhacungcap> Nhacungcaps { get; set; }

    public virtual DbSet<Sanpham> Sanphams { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("server=localhost;database=sell_phone;user=root;password=");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Binhluan>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("binhluan");

            entity.HasIndex(e => e.IdNd, "fk_bl_nd");

            entity.HasIndex(e => e.IdSp, "fk_bl_sp");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Danhgia)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("danhgia");
            entity.Property(e => e.IdNd)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("idND");
            entity.Property(e => e.IdSp)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("idSP");
            entity.Property(e => e.Ngaybinhluan)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime")
                .HasColumnName("ngaybinhluan");
            entity.Property(e => e.Noidung)
                .HasMaxLength(255)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("noidung");
            entity.Property(e => e.Trangthai)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("tinyint(4)")
                .HasColumnName("trangthai");

            entity.HasOne(d => d.IdNdNavigation).WithMany(p => p.Binhluans)
                .HasForeignKey(d => d.IdNd)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_bl_nd");

            entity.HasOne(d => d.IdSpNavigation).WithMany(p => p.Binhluans)
                .HasForeignKey(d => d.IdSp)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_bl_sp");
        });

        modelBuilder.Entity<Chitietdonhang>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("chitietdonhang");

            entity.HasIndex(e => e.IdDh, "fk_ctdh_dh");

            entity.HasIndex(e => e.IdSp, "fk_ctdh_sp");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Dongia)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("dongia");
            entity.Property(e => e.IdDh)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("idDH");
            entity.Property(e => e.IdSp)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("idSP");
            entity.Property(e => e.Soluong)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("soluong");
            entity.Property(e => e.Trangthai)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("tinyint(4)")
                .HasColumnName("trangthai");

            entity.HasOne(d => d.IdDhNavigation).WithMany(p => p.Chitietdonhangs)
                .HasForeignKey(d => d.IdDh)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_ctdh_dh");

            entity.HasOne(d => d.IdSpNavigation).WithMany(p => p.Chitietdonhangs)
                .HasForeignKey(d => d.IdSp)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_ctdh_sp");
        });

        modelBuilder.Entity<Chitietgiohang>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("chitietgiohang");

            entity.HasIndex(e => e.IdNd, "fk_ctgh_nd");

            entity.HasIndex(e => e.IdSp, "fk_ctgh_sp");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.IdNd)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("idND");
            entity.Property(e => e.IdSp)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("idSP");
            entity.Property(e => e.Soluong)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("soluong");
            entity.Property(e => e.Trangthai)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("tinyint(4)")
                .HasColumnName("trangthai");

            entity.HasOne(d => d.IdNdNavigation).WithMany(p => p.Chitietgiohangs)
                .HasForeignKey(d => d.IdNd)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_ctgh_nd");

            entity.HasOne(d => d.IdSpNavigation).WithMany(p => p.Chitietgiohangs)
                .HasForeignKey(d => d.IdSp)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_ctgh_sp");
        });

        modelBuilder.Entity<Diachi>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("diachi");

            entity.HasIndex(e => e.IdNd, "fk_dc_nd");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Ghichu)
                .HasMaxLength(255)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("ghichu");
            entity.Property(e => e.Huyen)
                .HasMaxLength(255)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("huyen");
            entity.Property(e => e.IdNd)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("idND");
            entity.Property(e => e.Sdt)
                .HasMaxLength(255)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("sdt");
            entity.Property(e => e.Ten)
                .HasMaxLength(255)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("ten");
            entity.Property(e => e.Tinh)
                .HasMaxLength(255)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("tinh");
            entity.Property(e => e.Trangthai)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("tinyint(4)")
                .HasColumnName("trangthai");
            entity.Property(e => e.Xa)
                .HasMaxLength(255)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("xa");

            entity.HasOne(d => d.IdNdNavigation).WithMany(p => p.Diachis)
                .HasForeignKey(d => d.IdNd)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_dc_nd");
        });

        modelBuilder.Entity<Donhang>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("donhang");

            entity.HasIndex(e => e.IdDc, "fk_dh_dc");

            entity.HasIndex(e => e.IdNd, "fk_dh_nd");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.IdDc)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("idDC");
            entity.Property(e => e.IdNd)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("idND");
            entity.Property(e => e.Ngaydathang)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime")
                .HasColumnName("ngaydathang");
            entity.Property(e => e.Ngaythanhtoan)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime")
                .HasColumnName("ngaythanhtoan");
            entity.Property(e => e.Tongtien)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("tongtien");
            entity.Property(e => e.Trangthai)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("tinyint(4)")
                .HasColumnName("trangthai");

            entity.HasOne(d => d.IdDcNavigation).WithMany(p => p.Donhangs)
                .HasForeignKey(d => d.IdDc)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_dh_dc");

            entity.HasOne(d => d.IdNdNavigation).WithMany(p => p.Donhangs)
                .HasForeignKey(d => d.IdNd)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_dh_nd");
        });

        modelBuilder.Entity<Hinhanh>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("hinhanh");

            entity.HasIndex(e => e.IdSp, "fk_ha_sp");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.IdSp)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("idSP");
            entity.Property(e => e.Url)
                .HasMaxLength(255)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("url");

            entity.HasOne(d => d.IdSpNavigation).WithMany(p => p.Hinhanhs)
                .HasForeignKey(d => d.IdSp)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_ha_sp");
        });

        modelBuilder.Entity<Lienhe>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("lienhe");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("email");
            entity.Property(e => e.Hoten)
                .HasMaxLength(255)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("hoten");
            entity.Property(e => e.Ngaytao)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime")
                .HasColumnName("ngaytao");
            entity.Property(e => e.Noidung)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("text")
                .HasColumnName("noidung");
            entity.Property(e => e.Sdt)
                .HasMaxLength(20)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("sdt");
            entity.Property(e => e.Tieude)
                .HasMaxLength(255)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("tieude");
            entity.Property(e => e.Trangthai)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("tinyint(4)")
                .HasColumnName("trangthai");
        });

        modelBuilder.Entity<Log>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("log");

            entity.HasIndex(e => e.IdNd, "fk_log_nd");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Capdo)
                .HasMaxLength(100)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("capdo");
            entity.Property(e => e.IdNd)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("idND");
            entity.Property(e => e.Ip)
                .HasMaxLength(255)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("ip");
            entity.Property(e => e.Ngaytao)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime")
                .HasColumnName("ngaytao");
            entity.Property(e => e.Nguon)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("text")
                .HasColumnName("nguon");
            entity.Property(e => e.Noidung)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("text")
                .HasColumnName("noidung");
            entity.Property(e => e.Trangthai)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("tinyint(4)")
                .HasColumnName("trangthai");

            entity.HasOne(d => d.IdNdNavigation).WithMany(p => p.Logs)
                .HasForeignKey(d => d.IdNd)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_log_nd");
        });

        modelBuilder.Entity<Nguoidung>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("nguoidung");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Anhdaidien)
                .HasMaxLength(255)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("anhdaidien");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("email");
            entity.Property(e => e.FacebookId)
                .HasMaxLength(255)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("facebookID");
            entity.Property(e => e.Gioitinh)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("bit(1)")
                .HasColumnName("gioitinh");
            entity.Property(e => e.GoogleId)
                .HasMaxLength(255)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("googleID");
            entity.Property(e => e.Matkhau)
                .HasMaxLength(255)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("matkhau");
            entity.Property(e => e.Ngaycapnhat)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime")
                .HasColumnName("ngaycapnhat");
            entity.Property(e => e.Ngaytao)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("datetime")
                .HasColumnName("ngaytao");
            entity.Property(e => e.Quyen)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("tinyint(4)")
                .HasColumnName("quyen");
            entity.Property(e => e.Sdt)
                .HasMaxLength(255)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("sdt");
            entity.Property(e => e.Ten)
                .HasMaxLength(255)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("ten");
            entity.Property(e => e.Trangthai)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("tinyint(4)")
                .HasColumnName("trangthai");
        });

        modelBuilder.Entity<Nhacungcap>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("nhacungcap");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Diachi)
                .HasMaxLength(255)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("diachi");
            entity.Property(e => e.TenNcc)
                .HasMaxLength(255)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("tenNCC");
            entity.Property(e => e.Trangthai)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("tinyint(4)")
                .HasColumnName("trangthai");
        });

        modelBuilder.Entity<Sanpham>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("sanpham");

            entity.HasIndex(e => e.IdNcc, "fk_sp_ncc");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Camera)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("text")
                .HasColumnName("camera");
            entity.Property(e => e.Chip)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("text")
                .HasColumnName("chip");
            entity.Property(e => e.Dungluong)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("text")
                .HasColumnName("dungluong");
            entity.Property(e => e.Giadagiam)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("giadagiam");
            entity.Property(e => e.Giagoc)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("giagoc");
            entity.Property(e => e.Hedieuhanh)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("text")
                .HasColumnName("hedieuhanh");
            entity.Property(e => e.IdNcc)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("idNCC");
            entity.Property(e => e.Manhinh)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("text")
                .HasColumnName("manhinh");
            entity.Property(e => e.Mausanpham)
                .HasMaxLength(255)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("mausanpham");
            entity.Property(e => e.Mota)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("text")
                .HasColumnName("mota");
            entity.Property(e => e.Pin)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("text")
                .HasColumnName("pin");
            entity.Property(e => e.Ram)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("text")
                .HasColumnName("ram");
            entity.Property(e => e.Soluong)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("int(11)")
                .HasColumnName("soluong");
            entity.Property(e => e.TenSp)
                .HasMaxLength(255)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("tenSP");
            entity.Property(e => e.Tenviettat)
                .HasMaxLength(4)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("tenviettat");
            entity.Property(e => e.Thuonghieu)
                .HasMaxLength(255)
                .HasDefaultValueSql("'NULL'")
                .HasColumnName("thuonghieu");
            entity.Property(e => e.Trangthai)
                .HasDefaultValueSql("'NULL'")
                .HasColumnType("tinyint(4)")
                .HasColumnName("trangthai");

            entity.HasOne(d => d.IdNccNavigation).WithMany(p => p.Sanphams)
                .HasForeignKey(d => d.IdNcc)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_sp_ncc");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
