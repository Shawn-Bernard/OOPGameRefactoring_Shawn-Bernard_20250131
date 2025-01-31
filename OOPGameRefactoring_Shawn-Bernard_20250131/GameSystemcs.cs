using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

public static class GameSystem
{
    static Random random = new Random();

    static Player player;

    static Enemy enemy;
    static void DrawCards(Character Entity)
    {
        while (Entity.Hand.Count < 3 && Entity.Deck.Count > 0)
        {
            Entity.Hand.Add(Entity.Deck[0]);
            Entity.Deck.RemoveAt(0);
        }
    }
    static public void StartGame()
    {
        player = new Player();

        enemy = new Enemy();

        Console.WriteLine("=== Card Battle Game ===");
        enemy.InitializeDecks(enemy);
        player.InitializeDecks(player);

        while (player.Health > 0 && enemy.Health > 0)
        {
            // Draw cards if needed
            if (player.Hand.Count < 3) DrawCards(player);
            if (enemy.Hand.Count < 3) DrawCards(enemy);

            // Player turn
            DisplayGameState(player, enemy);
            PlayTurn(true, player,enemy);

            if (enemy.Health <= 0) break;

            // Enemy turn
            Console.WriteLine("\nEnemy's turn...");
            Thread.Sleep(1000);
            PlayTurn(false, enemy,player);

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
    static public void DisplayGameState(Character player, Character enemy)
    {
        Console.WriteLine($"\nPlayer Health: {player.Health} | Mana: {player.Mana} | Shield: {player.Shield}");
        Console.WriteLine($"Enemy Health: {enemy.Health} | Mana: {enemy.Mana} | Shield: {enemy.Shield}");

        Console.WriteLine("\nYour hand:");
        for (int i = 0; i < player.Hand.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {(player.Hand[i])}");
            
        }
    }
    static public void PlayTurn(bool isPlayer, Character Entity,Character target)
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
                //if (choice == 0) return;

                PlayCard(hand[choice - 1], isPlayer, Entity, target);

                hand.RemoveAt(choice - 1);
            }
            else
            {
                // Simple AI: randomly play a card if enough mana
                int cardIndex = random.Next(hand.Count);

                if (cardIndex == hand.Count) return ;

                string cardToPlay = hand[cardIndex];

                // Check if enough mana
                if ((cardToPlay == "FireBallCard" && Entity.Mana >= 30) ||
                    (cardToPlay == "IceShieldCard" && Entity.Mana >= 20) ||
                    (cardToPlay == "HealCard" && Entity.Mana >= 40) ||
                    (cardToPlay == "SlashCard" && Entity.Mana >= 20) ||
                    (cardToPlay == "PowerUpCard" && Entity.Mana >= 30))
                {
                    PlayCard(cardToPlay, isPlayer, Entity, target);
                    hand.RemoveAt(cardIndex);
                }
            }
        }
    static void PlayCard(string cardName, bool isPlayer, Character user, Character target)
    {
        Console.WriteLine(isPlayer);
        Console.WriteLine(cardName);
        Cards card = null;
            if (isPlayer)
            {
            switch (cardName)
            {
                case "FireBallCard":
                    card = new FireBallCard();
                    card.Effects(user, target);
                    break;
                case "IceShieldCard":
                    card = new IceShieldCard();
                    card.Effects(user, target);
                    break;
                case "HealCard":
                    card = new HealCard();
                    card.Effects(user, target);
                    break;
                case "SlashCard":
                    card = new SlashCard();
                    card.Effects(user, target);
                    break;
                case "PowerUpCard":
                    card = new PowerUpCard();
                    card.Effects(user, target);
                    break;
            }
                if (cardName == "FireBallCard")
                {
                card = new FireBallCard();
                card.Effects(user, target);
                }
            }
            else
            {

            }
        }
    static public void UpdateBuffs(bool isPlayer,Character Entity)
    {
        if (isPlayer)
        {
            if (Entity.HasFireBuff) Entity.HasFireBuff = false;
            if (Entity.HasIceShield) Entity.HasIceShield = false;
            Entity.Mana = Math.Min(100, Entity.Mana + 20);
        }
    }
} 

