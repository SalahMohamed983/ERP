using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationLayer.Features.GenralSettings.InvProductionLines.Dtos
{
    public class SmallInvProductionLineDto
    {
        public long Id { get; set; }
        public long ProductionLinesCode { get; set; }
        public string Name { get; set; } = null!;
        public long AccountNumber { get; set; }
        public decimal CurrentBalance { get; set; }
        public string? Notes { get; set; }

    }
}
