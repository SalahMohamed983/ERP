using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DominLayer.Entites;

[Table("inv_itemcard")]
public partial class InvItemcard
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Column("item_code")]
    public long ItemCode { get; set; }

    [Column("barcode")]
    [StringLength(50)]
    public string Barcode { get; set; } = null!;

    [Column("name")]
    [StringLength(225)]
    public string Name { get; set; } = null!;

    [Column("item_type")]
    public byte ItemType { get; set; }

    [Column("inv_itemcard_categories_id")]
    public int InvItemcardCategoriesId { get; set; }

    [Column("parent_inv_itemcard_id")]
    public long? ParentInvItemcardId { get; set; }

    [Column("does_has_retailunit")]
    public bool DoesHasRetailunit { get; set; }

    [Column("retail_uom_id")]
    public int? RetailUomId { get; set; }

    [Column("uom_id")]
    public int UomId { get; set; }

    [Column("retail_uom_quntToParent", TypeName = "decimal(10, 2)")]
    public decimal? RetailUomQuntToParent { get; set; }

    [Column("added_by")]
    public int AddedBy { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at")]
    public DateTime? UpdatedAt { get; set; }

    [Column("updated_by")]
    public int? UpdatedBy { get; set; }

    [Column("active")]
    public bool Active { get; set; }

    [Column("date")]
    public DateOnly Date { get; set; }

    [Column("com_code")]
    public int ComCode { get; set; }

    [Column("price", TypeName = "decimal(10, 2)")]
    public decimal Price { get; set; }

    [Column("nos_gomla_price", TypeName = "decimal(10, 2)")]
    public decimal NosGomlaPrice { get; set; }

    [Column("gomla_price", TypeName = "decimal(10, 2)")]
    public decimal GomlaPrice { get; set; }

    [Column("price_retail", TypeName = "decimal(10, 2)")]
    public decimal? PriceRetail { get; set; }

    [Column("nos_gomla_price_retail", TypeName = "decimal(10, 2)")]
    public decimal? NosGomlaPriceRetail { get; set; }

    [Column("gomla_price_retail", TypeName = "decimal(10, 2)")]
    public decimal? GomlaPriceRetail { get; set; }

    [Column("cost_price", TypeName = "decimal(10, 2)")]
    public decimal CostPrice { get; set; }

    [Column("cost_price_retail", TypeName = "decimal(10, 2)")]
    public decimal? CostPriceRetail { get; set; }

    [Column("has_fixced_price")]
    public bool HasFixcedPrice { get; set; }

    [Column("All_QUENTITY", TypeName = "decimal(10, 2)")]
    public decimal? AllQuentity { get; set; }

    [Column("QUENTITY", TypeName = "decimal(10, 3)")]
    public decimal? Quentity { get; set; }

    [Column("QUENTITY_Retail", TypeName = "decimal(10, 3)")]
    public decimal? QuentityRetail { get; set; }

    [Column("QUENTITY_all_Retails", TypeName = "decimal(10, 3)")]
    public decimal? QuentityAllRetails { get; set; }

    [Column("photo")]
    [StringLength(225)]
    public string? Photo { get; set; }
}
