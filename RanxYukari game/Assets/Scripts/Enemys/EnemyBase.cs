using UnityEngine;
//Base of the enemy.
public class EnemyBase : MonoBehaviour
{
    public int EnemyHp;
    public GameObject GameMasterObject;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void Hit(int Damage)
    {
        EnemyHp -= Damage;

        if(EnemyHp<=0)
        {
            Object.Destroy(this.gameObject);
        }
    }
}
