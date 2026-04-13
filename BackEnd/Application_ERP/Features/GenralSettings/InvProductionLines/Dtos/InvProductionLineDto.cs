using System;

namespace ApplicationLayer.Features.GenralSettings.InvProductionLines.Dtos;

public class InvProductionLineDto
{
    public long Id { get; set; }
    public long ProductionLinesCode { get; set; }
    public string Name { get; set; } = null!;
    public long AccountNumber { get; set; }
    public byte StartBalanceStatus { get; set; }
    public decimal StartBalance { get; set; }
    public decimal CurrentBalance { get; set; }
    public string? Notes { get; set; }
    public int AddedBy { get; set; }
    public int? UpdatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool Active { get; set; }
    public int ComCode { get; set; }
    public DateOnly Date { get; set; }
    public string? Address { get; set; }
    public string? Phones { get; set; }
}
