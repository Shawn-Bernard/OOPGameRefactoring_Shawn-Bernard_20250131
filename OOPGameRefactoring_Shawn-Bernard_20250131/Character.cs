using System;
using System.Collections.Generic;

public abstract class Character
{
    //Making a list of cards that can hold any card that inheritance cards class
    public List<Card> Deck = new List<Card>();

    public List<Card> Hand = new List<Card>();

    protected int health = 100, maxHealth = 100;

    protected int mana = 100, maxMana = 100;

    protected int shield = 0, maxShield = 100;
    // This is the fire buff duration, made it 3 so its actually 2 turns
    private int buffDuration = 0, maxBuffDuration = 3;

    public int BuffDuration
    {
        get => buffDuration;
        set
        {
            buffDuration = Math.Max(0, Math.Min(maxBuffDuration, value));
        }
    }

    public bool HasFireBuff
    {
        get
        {
            if (buffDuration > 0)
                return true;
            else
                return false;
        }
    }
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