using UnityEngine;
using System.Collections;

public class ScriptTest : MonoBehaviour {

    public int attackDamage = 51;


    GameObject player;
    PlayerHealth playerHealth;
    AudioSource TAudio;


    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag ("Player");
        playerHealth = player.GetComponent <PlayerHealth> ();
        TAudio = GetComponent<AudioSource>();

    }
    
    void Update ()
    {
        if (Input.GetKeyDown("q"))
        {
            playerHealth.TakeDamage (attackDamage);
            TAudio.Play();
        }
    }
}