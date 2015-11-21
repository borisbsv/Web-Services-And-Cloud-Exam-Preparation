namespace BullsAndCows.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Guess
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(4)]
        [MaxLength(4)]
        public string Number { get; set; }
        
        [Required]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        
        public DateTime MadeOn { get; set; }
        
        public int Bulls { get; set; }

        public int Cows { get; set; }
    }
}
