using System;
using System.Threading;

public static class GameSystem
{
    static Random random = new Random();

    static Player player;

    static Enemy enemy;

    static public void StartGame()
    {
        player = new Player();

        enemy = new Enemy();

        Console.WriteLine("=== Card Battle Game ===");
        InitializeDecks(enemy);
        InitializeDecks(player);

        while (player.Health > 0 && enemy.Health > 0)
        {
            // Draw cards if needed
            DrawCards(player);
            DrawCards(enemy);

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
            enemy.UpdateBuffs();
            player.UpdateBuffs();

            Console.WriteLine("\nPress any key for next round...");
            Console.ReadKey();
            Console.Clear();
        }

        Console.WriteLine(player.Health <= 0 ? "You Lost!" : "You Won!");
    }
    static public void DrawCards(Character entity)
    {
        if (entity.Hand.Count < 3)
            while (entity.Hand.Count < 3 && entity.Deck.Count > 0)

            {
            entity.Hand.Add(entity.Deck[0]);
            entity.Deck.RemoveAt(0);
            }

    }
    public static void InitializeDecks(Character entity)
    {
        // Adding my cards to entity deck & setting the mana cost
        for (int i = 0; i < 5; i++) entity.Deck.Add(new FireBallCard(30));
        for (int i = 0; i < 5; i++) entity.Deck.Add(new IceShieldCard(20));
        for (int i = 0; i < 3; i++) entity.Deck.Add(new HealCard(40));
        for (int i = 0; i < 4; i++) entity.Deck.Add(new SlashCard(20));
        for (int i = 0; i < 3; i++) entity.Deck.Add(new PowerUpCard(30));
        for (int i = 0; i < 3; i++) entity.Deck.Add(new ShieldShatterCard(40));

        int n = entity.Deck.Count;
        // Shuffling the deck
        while (n > 1)
        {
            n--;
            int k = random.Next(n + 1);
            Card temp = entity.Deck[k];
            entity.Deck[k] = entity.Deck[n];
            entity.Deck[n] = temp;
        }
    }
    static public void DisplayGameState(Character player, Character enemy)
    {
        Console.WriteLine($"\nPlayer Health: {player.Health} | Mana: {player.Mana} | Shield: {player.Shield} |  Buff Duration {player.BuffDuration}");
        Console.WriteLine($"Enemy Health: {enemy.Health} | Mana: {enemy.Mana} | Shield: {enemy.Shield} | Buff Duration: {enemy.BuffDuration}");

        Console.WriteLine("\nYour hand:");
        for (int i = 0; i < player.Hand.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {player.Hand[i].GetCardDescription()}");
        }
    }
    static public void PlayTurn(bool isPlayer, Character User,Character target)
    {
        var hand = isPlayer ? player.Hand : enemy.Hand;

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

                // Use the cards effect on the player (target)
                hand[choice -1].Effects(User,target);

                hand.RemoveAt(choice - 1);
        }
        else
        {
                // Simple AI: randomly play a card if enough mana
                int cardIndex = random.Next(hand.Count);

                Card cardToPlay = hand[cardIndex];
            // Check if enough mana
            // If my card to play is one of my card and is greater than or equal to card to play mana cost
            if ((cardToPlay is FireBallCard && User.Mana >= cardToPlay.manaCost) ||
                    (cardToPlay is IceShieldCard && User.Mana >= cardToPlay.manaCost) ||
                    (cardToPlay is HealCard && User.Mana >= cardToPlay.manaCost) ||
                    (cardToPlay is SlashCard && User.Mana >= cardToPlay.manaCost) ||
                    (cardToPlay is PowerUpCard && User.Mana >= cardToPlay.manaCost)||
                    cardToPlay is ShieldShatterCard && User.Mana >= cardToPlay.manaCost)
                {
                // Then use the cards effect on the player (target)
                cardToPlay.Effects(User, target);

                hand.RemoveAt(cardIndex);
                }
        }
    }
} 

