using UnityEngine;
using UnityEngine.Events;
//The base of the player, contains stuff like HP and meta data, by rat queen
public class PlayerBase : MonoBehaviour
{
    public int HP;
    public bool Immortal;
    [SerializeField]
    int MaxHP;
    public UnityEvent<string> DeathEventCall; //Called Whenever the player dies to clean up everything 


    void Start()
    {
        if (DeathEventCall == null)
        {
            DeathEventCall = new UnityEvent<string>();
        }
        HP = MaxHP;
        DeathEventCall.AddListener(Die);
    }

    void Update()
    {
    
    }

    public void Hit(int damage)
    {
        HP -= damage;
        if(HP == 0)
        {
            DeathEventCall.Invoke("Death"); //Calls the death event in all functions
        }
    }

    public void Die(string Massage)
    {
        Destroy(this.gameObject); 
    }
}
