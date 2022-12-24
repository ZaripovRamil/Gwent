using System.Collections.Generic;


namespace GwentClient.Models
{
    public static class CardLibrary
    {
        public static Dictionary<int, Card> Cards { get; } =
            new()
            {
                {1, new Card(1, Role.Melee, 6, "Геральт")},
                {2, new Card(2, Role.Shooter, 3, "Лучник")},
                {3, new Card(3, Role.Melee, 3, "Дийкстра")},
                {4, new Card(4, Role.Shooter, 1, "Детмольд")},
                {5, new Card(5, Role.Melee, 3, "Вернон Роше")},
                {6, new Card(6, Role.Shooter, 2, "Иорвет")}
            };
    }
}
