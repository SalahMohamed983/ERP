using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DominLayer.Entites;

[Table("admins_shifts")]
public partial class AdminsShift
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Column("shift_code")]
    public long ShiftCode { get; set; }

    [Column("admin_id")]
    public int AdminId { get; set; }

    [Column("treasuries_id")]
    public int TreasuriesId { get; set; }

    [Column("treasuries_balnce_in_shift_start", TypeName = "decimal(10, 2)")]
    public decimal TreasuriesBalnceInShiftStart { get; set; }

    [Column("start_date")]
    public DateTime StartDate { get; set; }

    [Column("end_date")]
    public DateTime? EndDate { get; set; }

    [Column("is_finished")]
    public bool IsFinished { get; set; }

    [Column("is_delivered_and_review")]
    public bool IsDeliveredAndReview { get; set; }

    [Column("delivered_to_admin_id")]
    public int? DeliveredToAdminId { get; set; }

    [Column("delivered_to_admin_sift_id")]
    public long? DeliveredToAdminSiftId { get; set; }

    [Column("delivered_to_treasuries_id")]
    public int? DeliveredToTreasuriesId { get; set; }

    [Column("money_should_deviled", TypeName = "decimal(10, 2)")]
    public decimal? MoneyShouldDeviled { get; set; }

    [Column("what_realy_delivered", TypeName = "decimal(10, 2)")]
    public decimal? WhatRealyDelivered { get; set; }

    [Column("money_state")]
    public bool? MoneyState { get; set; }

    [Column("money_state_value", TypeName = "decimal(10, 2)")]
    public decimal MoneyStateValue { get; set; }

    [Column("receive_type")]
    public bool? ReceiveType { get; set; }

    [Column("review_receive_date")]
    public DateTime? ReviewReceiveDate { get; set; }

    [Column("treasuries_transactions_id")]
    public long? TreasuriesTransactionsId { get; set; }

    [Column("added_by")]
    public int AddedBy { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("notes")]
    [StringLength(100)]
    public string? Notes { get; set; }

    [Column("com_code")]
    public int ComCode { get; set; }

    [Column("date")]
    public DateOnly Date { get; set; }

    [Column("updated_by")]
    public int? UpdatedBy { get; set; }

    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }
}
