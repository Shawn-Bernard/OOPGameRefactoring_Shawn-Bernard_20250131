using System;
using System.Collections.Generic;

public abstract class Character
{
    // Making a list of cards that can hold any card that inheritance cards class
    public List<Card> Deck = new List<Card>();
    // I also made the deck/hand in here because they were both using it before 
    public List<Card> Hand = new List<Card>();

    protected int health = 100, maxHealth = 100;

    protected int mana = 100, maxMana = 100;

    protected int shield = 0, maxShield = 100;
    // This is the fire buff duration, made it 3 so its actually 2 turns
    private int buffDuration = 0, maxBuffDuration = 3;

    // Did this so each user has there own properties for each
    public int BuffDuration
    {
        get => buffDuration;
        set
        {
            // I made it so the the min is 0 and max is 3, so not stacking turns
            buffDuration = Math.Max(0, Math.Min(maxBuffDuration, value));
        }
    }

    public bool HasFireBuff
    {
        get
        {
            // If the int buffDuration has more than 0 returns true
            if (buffDuration > 0)
                return true;
            else
                return false;
        }
    }
    public bool HasIceShield
    {
        // If the int shield has more than 0 returns true
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
    //Calling this with my character.updatebuffs
    public void UpdateBuffs()
    {
        //Taking away one every time update buffs is called
        BuffDuration--;
        // Also giving 20 mana
        Mana += 20;
    }

}
public class Player : Character
{

}
public class Enemy : Character
{

}