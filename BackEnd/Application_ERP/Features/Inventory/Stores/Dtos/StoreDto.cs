using System;
using System.Collections.Generic;

namespace ApplicationLayer.Features.Inventory.Stores.Dtos;

public  class StoreDto
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Phones { get; set; }

    public string? Address { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int AddedBy { get; set; }

    public int? UpdatedBy { get; set; }

    public int ComCode { get; set; }

    public DateOnly Date { get; set; }

    public bool Active { get; set; }
}
