using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public abstract class Cards
{
    int damage;
    // On the other effect check character.mana 
    public abstract void Effects(Character user, Character target);

    public void GetCardDescription()
    {

    }
}

public class FireBallCard : Cards
{
    int damage = 40;
    public override void Effects(Character user, Character target)
    {
        if (user.Mana < 30)
        {
            return;
        }
        else
        {
            if (user.HasFireBuff) damage *= 2;
            if (target.HasIceShield) damage /= 2;
            if (target.Shield > 0)
            {
                if (target.Shield >= damage)
                {
                    target.Shield -= damage;
                    damage = 0;
                }
                else
                {
                    damage -= target.Shield;
                    target.Shield = 0;
                }
            }
        }
    }

}
public class IceShieldCard : Cards
{
    public override void Effects(Character user, Character target)
    {
        throw new NotImplementedException();
    }

}
public class HealCard : Cards
{
    public override void Effects(Character user, Character target)//Maybe character could fit here for heals or damage
    {
        //Something like this maybe
        /*
        if (Person.Health >= 100)
        {

        }
        */
    }

}
public class SlashCard : Cards
{
    public override void Effects(Character user, Character target)
    {
        throw new NotImplementedException();
    }

}
public class PowerUpCard : Cards
{
    public override void Effects(Character user, Character target)
    {

    }

}
