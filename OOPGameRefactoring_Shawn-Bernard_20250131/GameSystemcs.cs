using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

public class GameSystem
{
    Random random = new Random();
    void DrawCards(Character Entity)
    {
        while (Entity.Hand.Count < 3 && Entity.Deck.Count > 0)
        {
            Entity.Hand.Add(Entity.Deck[0]);
            Entity.Deck.RemoveAt(0);
        }
    }
    public void StartGame()
    {
        Player player = new Player();
        Enemy enemy = new Enemy();
        Console.WriteLine("=== Card Battle Game ===");
        //InitializeDecks();

        while (player.Health > 0 && enemy.Health > 0)
        {
            // Draw cards if needed
            if (player.Hand.Count < 3) DrawCards(player);
            if (enemy.Hand.Count < 3) DrawCards(enemy);

            // Player turn
            DisplayGameState(player, enemy);
            PlayTurn(true, player);

            if (enemy.Health <= 0) break;

            // Enemy turn
            Console.WriteLine("\nEnemy's turn...");
            Thread.Sleep(1000);
            PlayTurn(false, enemy);

            if (player.Health <= 0) break;

            // End of round effects
            UpdateBuffs(true, enemy);
            UpdateBuffs(false, enemy);
            UpdateBuffs(true,player);
            UpdateBuffs(false,player);

            Console.WriteLine("\nPress any key for next round...");
            Console.ReadKey();
            Console.Clear();
        }

        Console.WriteLine(player.Health <= 0 ? "You Lost!" : "You Won!");
        Console.ReadKey();
    }
    public void DisplayGameState(Character player, Character enemy)
    {
        Console.WriteLine($"\nPlayer Health: {player.Health} | Mana: {player.Mana} | Shield: {player.Shield}");
        Console.WriteLine($"Enemy Health: {enemy.Health} | Mana: {enemy.Mana} | Shield: {enemy.Shield}");

        Console.WriteLine("\nYour hand:");
        for (int i = 0; i < player.Hand.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {(player.Hand[i])}");
        }
    }
        public void PlayTurn(bool isPlayer, Character Entity)
        {
            var hand = Entity.Hand;

            if (isPlayer)
            {
                Console.Write("\nChoose a card to play (1-3) or 0 to skip: ");
                if (!int.TryParse(Console.ReadKey(true).KeyChar.ToString(), out int choice) || choice < 0 || choice > hand.Count)
                {
                    Console.WriteLine($" {choice} Invalid choice! Turn skipped.");
                    return;
                }
                Console.WriteLine(choice.ToString());
                if (choice == 0) return;

                PlayCard(hand[choice - 1], isPlayer);
                hand.RemoveAt(choice - 1);
            }
            else
            {
                // Simple AI: randomly play a card if enough mana
                int cardIndex = random.Next(hand.Count);
                Cards cardToPlay = hand[cardIndex];

                // Check if enough mana
                if ((cardToPlay is FireBallCard && Entity.Mana >= 30) ||
                    (cardToPlay is IceShieldCard && Entity.Mana >= 20) ||
                    (cardToPlay is HealCard && Entity.Mana >= 40) ||
                    (cardToPlay is SlashCard && Entity.Mana >= 20) ||
                    (cardToPlay is PowerUpCard && Entity.Mana >= 30))
                {
                    PlayCard(cardToPlay, isPlayer);
                    hand.RemoveAt(cardIndex);
                }
            }
        }
         void PlayCard(Cards cardName, bool isPlayer)
        {
            if (isPlayer)
            {
                
            }
        }
    public void UpdateBuffs(bool isPlayer,Character Entity)
    {
        if (isPlayer)
        {
            if (Entity.HasFireBuff) Entity.HasFireBuff = false;
            if (Entity.HasIceShield) Entity.HasIceShield = false;
            Entity.Mana = Math.Min(100, Entity.Mana + 20);
        }
    }
} 

