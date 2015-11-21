namespace BullsAndCows.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Game
    {
        private ICollection<Guess> guesses;

        public Game()
        {
            this.guesses = new HashSet<Guess>();
        }

        [Required]
        [MinLength(5)]
        [MaxLength(50)]
        public string Name { get; set; }

        [Key]
        public int Id { get; set; }

        public DateTime DateCreated { get; set; }

        [Required]
        public GameState GameState { get; set; }

        public GameResult Result { get; set; }

        public string RedPlayerId { get; set; }

        [ForeignKey("RedPlayerId")]
        public virtual User Red { get; set; }

        public string BluePlayerId { get; set; }

        [ForeignKey("BluePlayerId")]
        public virtual User Blue { get; set; }

        [MinLength(4)]
        [MaxLength(4)]
        public string RedPlayerNumber { get; set; }

        [MinLength(4)]
        [MaxLength(4)]
        public string BluePlayerNumber { get; set; }

        public virtual ICollection<Guess> Guesses { get; set; }
    }
}
