namespace TIM.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User")]
    public partial class User
    {
        public User()
        {
            Athlete = new HashSet<Athlete>();
            Event = new HashSet<Event>();
            Team = new HashSet<Team>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal User_ID { get; set; }

        [Required]
        [StringLength(256)]
        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public virtual ICollection<Athlete> Athlete { get; set; }

        public virtual ICollection<Event> Event { get; set; }

        public virtual ICollection<Team> Team { get; set; }
    }
}
