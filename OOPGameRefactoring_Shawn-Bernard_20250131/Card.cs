using System;
using System.Runtime.CompilerServices;

public abstract class Card
{
    public int manaCost;

    public Card(int Cost)
    {
        manaCost = Cost;
    }
    
    // Making a method to be override in my other cards
    // Also since the cards all have an effect
    public abstract void Effects(Character user, Character target);

    // Same thing with here
    public abstract string GetCardDescription();
}

public class FireBallCard : Card
{
    int damage = 40;
    // This is inheriting from the base cost tbh I didn't know you could do this
    public FireBallCard(int Cost) : base(Cost)
    {
        manaCost = Cost;
    }
    // I put the user here since the user could be anyone and same with the target
    public override void Effects(Character user, Character target)
    {
        // this would check the if the user has enough mana and returns with writeline 
        if (user.Mana < manaCost)
        {
            Console.WriteLine("Not enough mana");
            return;
        }
        else
        {
            // Taking away the user mana
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
public class IceShieldCard : Card
{
    public IceShieldCard(int Cost) : base(Cost)
    {
        manaCost = Cost;
    }

    public override void Effects(Character user, Character target)
    {
        if (user.Mana < manaCost)
        {
            Console.WriteLine("Not enough mana");
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
public  class HealCard : Card
{
    public HealCard(int Cost) : base(Cost)
    {
        manaCost = Cost;
    }

    public override void Effects(Character user, Character target)//Maybe character could fit here for heals or damage
    {
        if (user.Mana < 40)
        {
            Console.WriteLine("Not enough mana");
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
public class SlashCard : Card
{
    int damage = 20;

    public SlashCard(int Cost) : base(Cost)
    {
        manaCost = Cost;
    }

    public override void Effects(Character user, Character target)
    {
        if (user.Mana < manaCost)
        {
            Console.WriteLine("Not enough mana");
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
public class PowerUpCard : Card
{
    public PowerUpCard(int Cost ) : base(Cost)
    {
        manaCost = Cost;
    }

    public override void Effects(Character user, Character target)
    {
        if (user.Mana < manaCost)
        {
            Console.WriteLine("Not enough mana");
            return;
        }
        else
        {
            // Adding 3 turn for a real 2 turn buff, because every round take buff duration away 
            user.BuffDuration += 3;
            user.Mana -= manaCost;
            Console.WriteLine($"{user} gains Fire Buff!");
        }
    }
    public override string GetCardDescription()
    {
        return "Power Up (Costs 30 mana): Gain fire buff for 2 turns";
    }
}
public class ShieldShatterCard : Card
{
    public ShieldShatterCard(int Cost) : base(Cost)
    {
        manaCost = Cost;
    }

    public override void Effects(Character user, Character target)
    {
        if (user.Mana < manaCost)
        {
            Console.WriteLine("Not enough mana");
            return;
        }
        else
        {

            // Making a new local int damage equal to the user shield
            int damage = user.Shield;
            //Checking if the user has the buff
            if (user.HasFireBuff) damage *= 2;
            user.Mana -= manaCost;
            user.Shield = 0;
            // Find out where to take from
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