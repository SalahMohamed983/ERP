using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DominLayer.Entites;

[Table("inv_itemcard_movements")]
public partial class InvItemcardMovement
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Column("inv_itemcard_movements_categories")]
    public int InvItemcardMovementsCategories { get; set; }

    [Column("item_code")]
    public long ItemCode { get; set; }

    [Column("store_id")]
    public int StoreId { get; set; }

    [Column("items_movements_types")]
    public int ItemsMovementsTypes { get; set; }

    [Column("FK_table")]
    public long FkTable { get; set; }

    [Column("FK_table_details")]
    public long FkTableDetails { get; set; }

    [Column("byan")]
    [StringLength(100)]
    public string Byan { get; set; } = null!;

    [Column("quantity_befor_movement")]
    [StringLength(60)]
    public string QuantityBeforMovement { get; set; } = null!;

    [Column("quantity_after_move")]
    [StringLength(60)]
    public string QuantityAfterMove { get; set; } = null!;

    [Column("added_by")]
    public int AddedBy { get; set; }

    [Column("date")]
    public DateOnly Date { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("com_code")]
    public int ComCode { get; set; }

    [Column("quantity_befor_move_store")]
    [StringLength(60)]
    public string QuantityBeforMoveStore { get; set; } = null!;

    [Column("quantity_after_move_store")]
    [StringLength(60)]
    public string QuantityAfterMoveStore { get; set; } = null!;
}
