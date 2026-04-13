using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DominLayer.Entites;

[Table("account_types")]
public partial class AccountType
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Column("name")]
    [StringLength(255)]
    public string Name { get; set; } = null!;

    [Column("active")]
    public byte Active { get; set; }

    [Column("relatediternalaccounts")]
    public byte Relatediternalaccounts { get; set; }
}
