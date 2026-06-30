using UnityEngine;
//Base of the enemy. By rat queen
public class EnemyBase : MonoBehaviour, EnemyHitInterface
{
    GameObject Player;
    public int EnemyHp;
    public GameObject GameMasterObject;
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Player.GetComponent<PlayerBase>().DeathEventCall.AddListener(OnPlayerDeath);
    }

    void OnPlayerDeath(string Massage)
    {
        Destroy(this.gameObject); 
    }
    
    public void Hit(int Damage)
    {
        EnemyHp -= Damage;

        if(EnemyHp<=0)
        {
            Object.Destroy(this.gameObject);
            GameMasterObject.GetComponent<Wavespawner>().EnemyDeath(this.gameObject);
        }
    }
}
