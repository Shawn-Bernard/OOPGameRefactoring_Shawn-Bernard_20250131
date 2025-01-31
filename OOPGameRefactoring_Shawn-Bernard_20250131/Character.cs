using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public abstract class Character
{
    public List<string> Deck = new List<string>();

    public List<string> Hand = new List<string>();

    int health = 100, maxHealth = 100;

    int mana = 100, maxMana = 100;

    int shield = 0, maxShield = 100;

    public bool HasFireBuff;

    Random random = new Random();

    public bool HasIceShield
    {
        get { if (shield > 0)
                return true;
            else 
                return false;
            }
    }
    public int Health
    {
        get => health;
        set
        {
            health = Math.Max(0, Math.Min(maxHealth, value));
        }
    }
    public int Mana
    {
        get => mana;
        set
        {
            mana = Math.Max(0, Math.Min(maxMana, value));
        }
    }
    public int Shield
    {
        get => shield;
        set
        {
            shield = Math.Max(0, Math.Min(maxShield, value));
        }
    }
    public void InitializeDecks(Character entity)
    {
        // Add cards to player deck
        for (int i = 0; i < 5; i++) entity.Deck.Add("FireballCard");
        for (int i = 0; i < 5; i++) entity.Deck.Add("IceShieldCard");
        for (int i = 0; i < 3; i++) entity.Deck.Add("HealCard");
        for (int i = 0; i < 4; i++) entity.Deck.Add("SlashCard");
        for (int i = 0; i < 3; i++) entity.Deck.Add("PowerUpCard");
        for (int i = 0; i < 3; i++) entity.Deck.Add("ShieldShatter");


        // Shuffle decks
        ShuffleDeck(entity.Deck);
    }
    void ShuffleDeck(List<string> deck)
    {
        int n = deck.Count;
        while (n > 1)
        {
            n--;
            int k = random.Next(n + 1);
            string temp = deck[k];
            deck[k] = deck[n];
            deck[n] = temp;
        }
    }

}
public class Player : Character
{

}
public class Enemy : Character
{

}