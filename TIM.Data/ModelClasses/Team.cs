namespace TIM.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Team")]
    public partial class Team
    {
        public Team()
        {
            Athlete = new HashSet<Athlete>();
            Event = new HashSet<Event>();
            User = new HashSet<User>();
        }

        [Required]
        [StringLength(32)]
        public string Name { get; set; }

        [Required]
        [StringLength(32)]
        public string Sport { get; set; }

        [Column(TypeName = "numeric")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal TeamId { get; set; }

        public virtual ICollection<Athlete> Athlete { get; set; }

        public virtual ICollection<Event> Event { get; set; }

        public virtual ICollection<User> User { get; set; }
    }
}
