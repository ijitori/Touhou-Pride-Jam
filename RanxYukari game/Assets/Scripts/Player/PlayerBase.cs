using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using TMPro;
//The base of the player, contains stuff like HP and meta data, by rat queen
public class PlayerBase : MonoBehaviour
{
    public int HP;
    public bool Immortal;
    [SerializeField]
    int MaxHP;
    
    [SerializeField] GameObject DeathUI;
    [SerializeField]
    TMP_Text Display;
    [SerializeField] float IFrameTime;
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
        Display.text = "Health: " +  HP;
    }

    public void Hit(int damage)
    {
        if(Immortal==false)
        {
        StartCoroutine("IFrames");

        HP -= damage;
        if(HP == 0)
        {
            DeathEventCall.Invoke("Death"); //Calls the death event in all functions
        }
        }
    }

    IEnumerator IFrames()
    {
        Immortal = true;
        for (float i = 0; i <= IFrameTime; i += Time.deltaTime)
        {
            
            yield return null;
        }
        Immortal = false;
    }

    public void Die(string Massage)
    {
        DeathUI.SetActive(true);
        Time.timeScale = 0;
        Destroy(this.gameObject); 
    }
}
