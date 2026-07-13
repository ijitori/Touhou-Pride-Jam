using UnityEngine;
//Lets out a bullet directed at the player. Code by rat queen
public class EnemySentryAttack : MonoBehaviour
{
    GameObject Player;
    [SerializeField] GameObject BulletToFire;
    [SerializeField] float FireCoolDown;
    [SerializeField] float BulletSpeed;
    [SerializeField] float FireTimer;
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
    }
}
