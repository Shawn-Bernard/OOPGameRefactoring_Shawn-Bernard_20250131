using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public abstract class Character
{
    public List<Cards> Deck = new List<Cards>();

    public List<Cards> Hand = new List<Cards>();

    int health = 100, maxHealth = 100;

    int mana = 100, maxMana = 100;

    int shield = 0, maxShield = 100;

    public bool HasFireBuff;

    public bool HasIceShield;

    Random random = new Random();
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

}
public class Player : Character
{
}
public class Enemy : Character
{

}