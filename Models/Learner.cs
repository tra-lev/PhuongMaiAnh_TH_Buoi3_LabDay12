using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Pma_Lab05.Models;

[Table("Learner")]
public partial class Learner
{
    [Key]
    [Column("LearnerID")]
    public int LearnerId { get; set; }

    [StringLength(50)]
    public string LastName { get; set; } = null!;

    [StringLength(50)]
    public string FirstMidName { get; set; } = null!;

    public DateOnly EnrollmentDate { get; set; }

    [Column("MajorID")]
    public int MajorId { get; set; }

    [InverseProperty("Learner")]
    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();

    [ForeignKey("MajorId")]
    [InverseProperty("Learners")]
    public virtual Major Major { get; set; } = null!;
}
