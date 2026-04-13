using System;

namespace ApplicationLayer.Features.GenralSettings.Admin.Dtos;

public class AdminPanelSettingDto
{
    public int Id { get; set; }
    public string SystemName { get; set; } = null!;
    public string Photo { get; set; } = null!;
    public bool Active { get; set; }
    public string? GeneralAlert { get; set; }
    public string Address { get; set; } = null!;
    public string? Phone { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public int ComCode { get; set; }
    public string? Notes { get; set; }
}
