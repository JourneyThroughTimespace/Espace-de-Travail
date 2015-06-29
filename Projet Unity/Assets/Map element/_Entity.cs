using UnityEngine;
using System.Collections;

public abstract class _Entity : MonoBehaviour 
{
    protected int life;
    protected int dmg;
    protected int range;
    public int burningDamage;
    public int poisonDamage;
    public int Duration;
    protected int currentweaponStatusEffect;
    public enum Status { Fine, Poisoned, Burning, Stun };
    public Status StatusEffect;

    public bool isPoisoned;
    public bool isBurning;
    public bool isStun;

    public int get_life()
    {
        return life;
    }
    public void set_life(int l)
    {
        life = l;
    }

    public int get_dmg()
    {
        return dmg;
    }
    public void set_dmg(int d)
    {
        dmg = d;
    }

    public int get_range()
    {
        return range;
    }
    public void set_range(int r)
    {
        range = r;
    }
    public int get_status()
    {
        return currentweaponStatusEffect;
    }

    public void set_status(int newStatus)
    {
        currentweaponStatusEffect = newStatus;
    }


    public abstract void LoseLife(int loss);

    public abstract void PoisonEffect(int poisonDamage, int Duration);

    public abstract void BurningEffect(int burningDamage, int Duration);

   // public abstract void StunEffect(int Duration);
    
    
    void Start () 
    {
        
	}
	
	
	void Update () 
    {
	
	}
}
