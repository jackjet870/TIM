namespace TIM.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Athlete")]
    public partial class Athlete
    {
        public Athlete()
        {
            Event = new HashSet<Event>();
            User = new HashSet<User>();
        }

        [NotMapped]
        [DisplayName("Team")]
        public string TeamName { get; set; }

        [NotMapped]
        [DisplayName("Full name")]
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }

        [Required]
        [StringLength(32)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(32)]
        public string LastName { get; set; }

        [Required]
        [StringLength(32)]
        public string Sport { get; set; }

        [Column(TypeName = "numeric")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal AthleteId { get; set; }

        [Column(TypeName = "numeric")]
        public decimal? TeamId { get; set; }

        public virtual Team Team { get; set; }

        public virtual ICollection<Event> Event { get; set; }

        public virtual ICollection<User> User { get; set; }
    }
}
