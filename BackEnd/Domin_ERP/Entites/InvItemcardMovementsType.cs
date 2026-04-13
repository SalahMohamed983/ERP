using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DominLayer.Entites;

[Table("inv_itemcard_movements_types")]
public partial class InvItemcardMovementsType
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("type")]
    [StringLength(50)]
    public string Type { get; set; } = null!;
}
