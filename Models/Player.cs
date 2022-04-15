using System.ComponentModel.DataAnnotations;

namespace Gamescore.Models
{

    // Для каждого участника нужно иметь возможность:
    // добавить как зарегистрированного игрока так и простого оппонента,
    // указать кто победил,
    // количество очков, цвет или название команды.

    public class Player
    {
        [Key]
        public virtual int Id { get; set; }

        // User

        public int Points { get; set; }
        public string Team { get; set; }   
        
        public bool IsWinner { get; set; }

    }
}
