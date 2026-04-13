using System;
using System.Collections.Generic;


namespace ApplicationLayer.Features.Inventory.InvItemcard.Dtos;
public class InvItemcardDto
{
    public long Id { get; set; }

    public long ItemCode { get; set; }

    public string Barcode { get; set; } = null!;

    public string Name { get; set; } = null!;

    public byte ItemType { get; set; }

    public int InvItemcardCategoriesId { get; set; }

    public long? ParentInvItemcardId { get; set; }

    public bool DoesHasRetailunit { get; set; }

    public int? RetailUomId { get; set; }

    public int UomId { get; set; }

    public decimal? RetailUomQuntToParent { get; set; }

    public int AddedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? UpdatedBy { get; set; }

    public bool Active { get; set; }

    public DateOnly Date { get; set; }

    public int ComCode { get; set; }

    public decimal Price { get; set; }

    public decimal NosGomlaPrice { get; set; }

    public decimal GomlaPrice { get; set; }

    public decimal? PriceRetail { get; set; }

    public decimal? NosGomlaPriceRetail { get; set; }

    public decimal? GomlaPriceRetail { get; set; }

    public decimal CostPrice { get; set; }

    public decimal? CostPriceRetail { get; set; }

    public bool HasFixcedPrice { get; set; }

    public decimal? AllQuentity { get; set; }

    public decimal? Quentity { get; set; }

    public decimal? QuentityRetail { get; set; }

    public decimal? QuentityAllRetails { get; set; }

    public string? Photo { get; set; }
}
