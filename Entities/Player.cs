using System.ComponentModel.DataAnnotations;

namespace Gamescore.Entities
{

    // Для каждого участника нужно иметь возможность:
    // добавить как зарегистрированного игрока так и простого оппонента,
    // указать кто победил,
    // количество очков, цвет или название команды.

    public class Player : BaseEntity
    {

        // User (registered / non-registered)

        public int Points { get; set; }
        public string Team { get; set; }   
        
        public bool IsWinner { get; set; }

    }
}
