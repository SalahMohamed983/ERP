using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DominLayer.Entites;

[Table("treasuries_transactions")]
public partial class TreasuriesTransaction
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Column("auto_serial")]
    public long AutoSerial { get; set; }

    [Column("isal_number")]
    public long IsalNumber { get; set; }

    [Column("shift_code")]
    public long ShiftCode { get; set; }

    [Column("money", TypeName = "decimal(10, 2)")]
    public decimal Money { get; set; }

    [Column("treasuries_id")]
    public int TreasuriesId { get; set; }

    [Column("is_approved")]
    public bool IsApproved { get; set; }

    [Column("mov_type")]
    public int MovType { get; set; }

    [Column("move_date")]
    public DateOnly MoveDate { get; set; }

    [Column("the_foregin_key")]
    public long? TheForeginKey { get; set; }

    [Column("account_number")]
    public long? AccountNumber { get; set; }

    [Column("is_account")]
    public bool? IsAccount { get; set; }

    [Column("money_for_account", TypeName = "decimal(10, 2)")]
    public decimal MoneyForAccount { get; set; }

    [Column("byan")]
    [StringLength(225)]
    public string Byan { get; set; } = null!;

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("added_by")]
    public int AddedBy { get; set; }

    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    [Column("updated_by")]
    public int? UpdatedBy { get; set; }

    [Column("com_code")]
    public int ComCode { get; set; }
}
