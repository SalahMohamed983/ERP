using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DominLayer.Entites;

[Table("personal_access_tokens")]
public partial class PersonalAccessToken
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Column("tokenable_type")]
    [StringLength(255)]
    public string TokenableType { get; set; } = null!;

    [Column("tokenable_id")]
    public long TokenableId { get; set; }

    [Column("name")]
    [StringLength(255)]
    public string Name { get; set; } = null!;

    [Column("token")]
    [StringLength(64)]
    public string Token { get; set; } = null!;

    [Column("abilities")]
    public string? Abilities { get; set; }

    [Column("last_used_at")]
    public DateTime? LastUsedAt { get; set; }

    [Column("expires_at")]
    public DateTime? ExpiresAt { get; set; }

    [Column("created_at")]
    public DateTime? CreatedAt { get; set; }

    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }
}
