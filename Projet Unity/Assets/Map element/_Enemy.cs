using UnityEngine;
using System.Collections;

public abstract class _Enemy : _Entity
{
    

    //public static _Enemy instance;

    public override void PoisonEffect(int poisonDamage, int Duration)
    {
        while (Duration > 0)
        {
            isPoisoned = true;
            life -= poisonDamage;
            Duration = Duration - 1;
        }
        isPoisoned = false;

    }

    public override void BurningEffect(int burningDamage, int Duration)
    {
        while (Duration > 0)
        {
            isBurning = true;
            life -= burningDamage;
            Duration = Duration - 1;
        }
        isBurning = false;
    }

    public override void LoseLife(int loss)
    {
        life -= loss;
        if (life <= 0)
        {
            dmg = 0;
            gameObject.SetActive(false);
        }
    }
    void Start()
    {
        //instance = this;
    }

    public abstract void turnUpdate(Transform target);

}
