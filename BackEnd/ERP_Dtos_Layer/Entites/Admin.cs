using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DominLayer.Entites;

[Table("admins")]
public partial class Admin
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    [StringLength(100)]
    public string Name { get; set; } = null!;

    [Column("permission_roles_id")]
    public int PermissionRolesId { get; set; }

    [Column("email")]
    [StringLength(100)]
    public string Email { get; set; } = null!;

    [Column("username")]
    [StringLength(100)]
    public string Username { get; set; } = null!;

    [Column("password")]
    [StringLength(225)]
    public string Password { get; set; } = null!;

    [Column("created_at")]
    public DateTime? CreatedAt { get; set; }

    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    [Column("added_by")]
    public int? AddedBy { get; set; }

    [Column("updated_by")]
    public int? UpdatedBy { get; set; }

    [Column("active")]
    public bool Active { get; set; }

    [Column("com_code")]
    public int ComCode { get; set; }

    [Column("date")]
    public DateOnly? Date { get; set; }

    [InverseProperty("Admin")]
    public virtual ICollection<AdminsStore> AdminsStores { get; set; } = new List<AdminsStore>();

    [InverseProperty("Admin")]
    public virtual ICollection<AdminsTreasury> AdminsTreasuries { get; set; } = new List<AdminsTreasury>();
}
