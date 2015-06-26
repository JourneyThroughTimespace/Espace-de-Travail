using UnityEngine;
using System.Collections;

public abstract class _Enemy : _Entity
{
    

    //public static _Enemy instance;
    

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
