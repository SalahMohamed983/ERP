using System;

namespace ApplicationLayer.Features.GenralSettings.Treasuries.Dtos;

public class TreasuryDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public bool IsMaster { get; set; }
    public long LastIsalExhcange { get; set; }
    public long LastIsalCollect { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public int AddedBy { get; set; }
    public int? UpdatedBy { get; set; }
    public int ComCode { get; set; }
    public DateOnly Date { get; set; }
    public bool Active { get; set; }
}
