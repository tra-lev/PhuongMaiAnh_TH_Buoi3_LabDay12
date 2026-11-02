using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Pma_Lab05.Models;

[Table("Major")]
public partial class Major
{
    [Key]
    [Column("MajorID")]
    public int MajorId { get; set; }

    [StringLength(100)]
    public string MajorName { get; set; } = null!;

    [InverseProperty("Major")]
    public virtual ICollection<Learner> Learners { get; set; } = new List<Learner>();
}
