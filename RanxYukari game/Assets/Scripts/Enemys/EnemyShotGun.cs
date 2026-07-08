using UnityEngine;

public class EnemyShotGun : MonoBehaviour
{
    GameObject Player;
    [SerializeField] GameObject BulletToFire;
    [SerializeField] float FireCoolDown;
    [SerializeField] float BulletSpeed;
    float FireTimer;
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
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
        var ReltivePos = (Player.transform.position) - this.transform.position; //Subtract the mouse pos with the pos of the gameobject to find how we need to rotate later
        var Bullet = Instantiate(BulletToFire, transform.position, Quaternion.identity);
        var BulletInterface = Bullet.GetComponent<BulletInterface>();
        BulletInterface.EnemyBullet = true;
        BulletInterface.Velocity = BulletSpeed;
        float rot_z = Mathf.Atan2(ReltivePos.y, ReltivePos.x) * Mathf.Rad2Deg;
        Bullet.transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);

        var Bullet2 = Instantiate(BulletToFire, transform.position, Quaternion.identity);
        var Bullet2Interface = Bullet2.GetComponent<BulletInterface>();
        Bullet2Interface.EnemyBullet = true;
        Bullet2Interface.Velocity = BulletSpeed;
        Bullet2.transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 85);

        var Bullet3 = Instantiate(BulletToFire, transform.position, Quaternion.identity);
        var Bullet3Interface = Bullet3.GetComponent<BulletInterface>();
        Bullet3Interface.EnemyBullet = true;
        Bullet3Interface.Velocity = BulletSpeed;
        Bullet3.transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 95);
    }
}
