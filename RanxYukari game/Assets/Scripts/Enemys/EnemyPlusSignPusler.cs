using UnityEngine;
//Shoots bullets in a plus sign pattern. Code by rat queen
public class EnemyPlusSignPusler : MonoBehaviour
{
    GameObject Player;
    [SerializeField] GameObject BulletToFire;
    [SerializeField] float FireCoolDown;
    [SerializeField] int BulletSpeed;
    bool AltFire;
    [SerializeField] float FireTimer;
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
        if(AltFire == false)
        {
            var Bullet1 = Instantiate(BulletToFire, transform.position, Quaternion.Euler(0f, 0f, 0f));
            var Bullet2 = Instantiate(BulletToFire, transform.position, Quaternion.Euler(0f, 0f, 90f));
            var Bullet3 = Instantiate(BulletToFire, transform.position, Quaternion.Euler(0f, 0f, 180f));
            var Bullet4 = Instantiate(BulletToFire, transform.position, Quaternion.Euler(0f, 0f, -90f)); //this code is PISSING ME OFF, im the oringal      Bullet walker

            var Bullet1Interface = Bullet1.GetComponent<BulletInterface>();
            var Bullet2Interface = Bullet2.GetComponent<BulletInterface>();
            var Bullet3Interface = Bullet3.GetComponent<BulletInterface>();
            var Bullet4Interface = Bullet4.GetComponent<BulletInterface>();
            Bullet1Interface.EnemyBullet = true;
            Bullet2Interface.EnemyBullet = true;
            Bullet3Interface.EnemyBullet = true;
            Bullet4Interface.EnemyBullet = true;
            Bullet1Interface.Velocity = BulletSpeed;
            Bullet2Interface.Velocity = BulletSpeed;
            Bullet3Interface.Velocity = BulletSpeed;
            Bullet4Interface.Velocity = BulletSpeed;   
            AltFire = true;
        } else
        {

            var Bullet1 = Instantiate(BulletToFire, transform.position, Quaternion.Euler(0f, 0f, 0f + 45f));
            var Bullet2 = Instantiate(BulletToFire, transform.position, Quaternion.Euler(0f, 0f, 90f + 45f));
            var Bullet3 = Instantiate(BulletToFire, transform.position, Quaternion.Euler(0f, 0f, 180f + 45f));
            var Bullet4 = Instantiate(BulletToFire, transform.position, Quaternion.Euler(0f, 0f, -90f + 45f)); //this code is PISSING ME OFF, im the oringal      Bullet walker

            var Bullet1Interface = Bullet1.GetComponent<BulletInterface>();
            var Bullet2Interface = Bullet2.GetComponent<BulletInterface>();
            var Bullet3Interface = Bullet3.GetComponent<BulletInterface>();
            var Bullet4Interface = Bullet4.GetComponent<BulletInterface>();
            Bullet1Interface.EnemyBullet = true;
            Bullet2Interface.EnemyBullet = true;
            Bullet3Interface.EnemyBullet = true;
            Bullet4Interface.EnemyBullet = true;
            Bullet1Interface.Velocity = BulletSpeed;
            Bullet2Interface.Velocity = BulletSpeed;
            Bullet3Interface.Velocity = BulletSpeed;
            Bullet4Interface.Velocity = BulletSpeed;  
            
            AltFire = false;
        }
        
    }

}
