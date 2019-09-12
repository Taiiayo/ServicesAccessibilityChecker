using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServicesAccessibilityChecker.Context
{
    public class Catalog
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public bool IsAvailable { get; set; }
        public int LastHourErrors { get; set; }
        public int LastDayErrors { get; set; }
        public double ResponseDuration { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
