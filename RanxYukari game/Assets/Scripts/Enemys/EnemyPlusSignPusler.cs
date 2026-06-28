using UnityEngine;
//Shoots bullets in a plus sign pattern. Code by rat queen
public class EnemyPlusSignPusler : MonoBehaviour
{
    GameObject Player;
    [SerializeField] GameObject BulletToFire;
    [SerializeField] float FireCoolDown;
    float FireTimer;
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

  
    void Update()
    {
        if(FireTimer<=0)
        {
            FireTimer = FireCoolDown;
            Fire();
        }

        FireTimer -= Time.deltaTime;
    }

    void Fire()
    {
        Instantiate(BulletToFire, transform.position, Quaternion.Euler(0f, 0f, 0f));
        Instantiate(BulletToFire, transform.position, Quaternion.Euler(0f, 0f, 90f));
        Instantiate(BulletToFire, transform.position, Quaternion.Euler(0f, 0f, 180f));
        Instantiate(BulletToFire, transform.position, Quaternion.Euler(0f, 0f, -90f)); //this code is PISSING ME OFF, im the oringal      Bullet walker
        
    }

}
