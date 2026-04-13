using System;
using System.Collections.Generic;

namespace ApplicationLayer.Features.Inventory.Suppliers.Dtos;

public  class SuuplierDto
{
    public int Id { get; set; }

    public long SuuplierCode { get; set; }

    public int SuppliersCategoriesId { get; set; }

    public string Name { get; set; } = null!;

    public long AccountNumber { get; set; }

    public byte StartBalanceStatus { get; set; }

    public decimal StartBalance { get; set; }

    public decimal? CurrentBalance { get; set; }

    public string? Notes { get; set; }

    public int AddedBy { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public bool? Active { get; set; }

    public int ComCode { get; set; }

    public DateOnly Date { get; set; }

    public string? Address { get; set; }

    public string? Phones { get; set; }
}
