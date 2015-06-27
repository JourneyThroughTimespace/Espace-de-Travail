using UnityEngine;
using System.Collections;

public abstract class _Entity : MonoBehaviour 
{
    protected int life;
    protected int dmg;
    protected int range;

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

    public abstract void LoseLife(int loss);
    
    
    void Start () 
    {
        
	}
	
	
	void Update () 
    {
	
	}
}
