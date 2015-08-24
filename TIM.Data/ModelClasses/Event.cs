namespace TIM.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Event")]
    public partial class Event
    {
        public Event()
        {
            Athlete = new HashSet<Athlete>();
            Team = new HashSet<Team>();
            User = new HashSet<User>();
        }

        [Required]
        [StringLength(64)]
        public string Name { get; set; }

        public double? Longitude { get; set; }

        public double? Latitude { get; set; }

        [StringLength(32)]
        public string Sport { get; set; }

        public bool Individual { get; set; }

        [Column(TypeName = "numeric")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal EventId { get; set; }

        [Column(TypeName = "date")]
        public DateTime StartDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime EndDate { get; set; }

        public virtual ICollection<Athlete> Athlete { get; set; }

        public virtual ICollection<Team> Team { get; set; }

        public virtual ICollection<User> User { get; set; }
    }
}
