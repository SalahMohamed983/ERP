using System;
using System.Collections.Generic;


namespace ApplicationLayer.Features.Inventory.InvItemcardCategory.Dtos;

public  class InvItemcardCategoryDto
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int AddedBy { get; set; }

    public int? UpdatedBy { get; set; }

    public int ComCode { get; set; }

    public DateOnly Date { get; set; }

    public bool? Active { get; set; }
}
