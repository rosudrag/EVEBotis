using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace DataAccess.Main.Models
{
    /// <summary>
    /// Class that models an Intel submission
    /// Composed of a solarSystem for now
    /// </summary>
    public class Submission
    {
        [Key]
        public int SubmissionId { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        [Display(Name = "Local Pilots")]
        public string SubmissionText { get; set; }

        public bool Processed { get; set; }

        /// <summary>
        /// Gets the submission time.
        /// </summary>
        /// <value>
        /// The submission time.
        /// </value>
        public DateTime SubmissionTime { get; private set; }

        public Submission()
        {
            SubmissionTime = DateTime.Now;
            Processed = false;
        }
 
    }
}
