using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

public abstract class Cards
{
    public int manaCost;
    // On the other effect check character.mana 
    public abstract void Effects(Character user, Character target);

    public abstract string GetCardDescription();
}

public class FireBallCard : Cards
{
    int damage = 40;
    
    public override void Effects(Character user, Character target)
    {
        manaCost = 30;
        if (user.Mana < manaCost)
        {
            return;
        }
        else
        {
            user.Mana -= manaCost;
            if (user.HasFireBuff) damage *= 2;
            Console.WriteLine($"{user} casts Fireball for {damage} damage!");
            if (target.HasIceShield)
            {
                target.Shield -= damage;
            }
            else
            {
                target.Health -= damage;
            }
            
        }
    }

    public override string GetCardDescription()
    {
            return "Fireball (Costs 30 mana): Deal 40 damage";
    }
}
public class IceShieldCard : Cards
{
    public override void Effects(Character user, Character target)
    {
        manaCost = 20;
        if (user.Mana < manaCost)
        {
            return;
        }
        else
        {
            user.Shield += 30;
            //user.HasIceShield = true;
            user.Mana -= 20;
            Console.WriteLine($"{user} gains Ice Shield!");
        }
    }
    public override string GetCardDescription()
    {
       return "Ice Shield (Costs 20 mana): Gain 30 shield and ice protection";
    }
}
public  class HealCard : Cards
{
    public override void Effects(Character user, Character target)//Maybe character could fit here for heals or damage
    {
        manaCost = 40;
        if (user.Mana < 40)
        {
            return;
        }
        else
        {
            
            user.Health += 40;
            user.Mana -= 40;
            Console.WriteLine($"{user} heals 40 health!");
        }
    }
    public override string GetCardDescription()
    {
        return "Heal (Costs 40 mana): Restore 40 health";
    }
}
public class SlashCard : Cards
{
    int damage = 20;
    public override void Effects(Character user, Character target)
    {
        manaCost = 20;
        if (user.Mana < 20)
        {
            return;
        }
        else
        {

            if (user.HasFireBuff) damage *= 2;
            user.Mana -= 20;
            if (target.HasIceShield)
            {
                target.Shield -= damage;
            }
            else
            {
                target.Health -= damage;
            }
        }
    }
    public override string GetCardDescription()
    {
        return "Slash (Costs 20 mana): Deal 20 damage";
    }

}
public  class PowerUpCard : Cards
{
    public override void Effects(Character user, Character target)
    {
        manaCost = 30;
        if (user.Mana < manaCost)
        {
            return;
        }
        else
        {

            user.HasFireBuff = true;
            user.Mana -= manaCost;
            Console.WriteLine($"{user} gains Fire Buff!");
        }
    }
    public override string GetCardDescription()
    {
        return "Power Up (Costs 30 mana): Gain fire buff for 2 turns";
    }
}
public class ShieldShatter : Cards
{
    public override void Effects(Character user, Character target)
    {
        manaCost = 40;
        if (user.Mana < manaCost)
        {
            return;
        }
        else
        {
            int damage = user.Shield;

            if (user.HasFireBuff) damage *= 2;
            user.Mana -= manaCost;
            user.Shield = 0;
            //user.HasIceShield;
            if (target.HasIceShield)
            {
                target.Shield -= damage;
            }
            else
            {
                target.Health -= damage;
            }
        }
    }

    public override string GetCardDescription()
    {
        return "Shield Shatter (Costs 40 mana): Deal Damage = Shield & destroy shield";
    }
}
