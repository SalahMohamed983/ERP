using System;

namespace ApplicationLayer.Features.GenralSettings.TreasuriesDeliveries.Dtos;

public class TreasuriesDeliveryDto
{
    public int Id { get; set; }
    public int TreasuriesId { get; set; }
    public int TreasuriesCanDeliveryId { get; set; }
    public DateTime CreatedAt { get; set; }
    public int AddedBy { get; set; }
    public int? UpdatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public int ComCode { get; set; }
}
