using UnityEngine;
//Shoots it in a circle like her non spell 
public class BossAttackTest : MonoBehaviour
{
    [SerializeField] GameObject BulletToFire;
    [SerializeField] float AttackCoolDown;
    [SerializeField] float OrbAttackCoolDown;
    [SerializeField] int PillarAmount;
    [SerializeField] Sprite NormalBulletSprite;
    [SerializeField] Sprite SlowBulletSprite;
    [SerializeField] Sprite OrbSprite;
    float InternalTimer;
    float InternalOrbTimer;
    BossBase BossBase;
    GameObject Player;
    void OnEnable()
    {
        Debug.Log("Im starting attack");
        Player = GameObject.FindGameObjectWithTag("Player");
        BulletToFire = Resources.Load<GameObject>("Prefabs/Enemy/Enemy Bullet") as GameObject;
        BossBase = this.GetComponent<BossBase>();
    }

    
    void Update()
    {
        if(InternalTimer<=0)
        {
            for(int i = 0; i <= PillarAmount; i++)
            {
                var NomralBullet = Instantiate(BulletToFire, transform.position, Quaternion.Euler(0f, 0f, 0f + (i * (80))));
                NomralBullet.GetComponent<BulletInterface>().Velocity = 50;
                NomralBullet.GetComponent<BulletInterface>().EnemyBullet = true;
                NomralBullet.GetComponent<SpriteRenderer>().sprite = NormalBulletSprite;
                NomralBullet.transform.localScale  = new Vector3(9,9,0);
                var SlowBullet = Instantiate(BulletToFire, transform.position, Quaternion.Euler(0f, 0f, 0f + (i * (80))));
                SlowBullet.GetComponent<BulletInterface>().Velocity = 10;
                SlowBullet.GetComponent<BulletInterface>().EnemyBullet = true;
                SlowBullet.GetComponent<SpriteRenderer>().sprite = SlowBulletSprite;
                SlowBullet.transform.localScale  = new Vector3(9,9,0);
                BossBase.SpellcardChangeCall.AddListener(NomralBullet.GetComponent<BulletInterface>().BulletClear);
                BossBase.SpellcardChangeCall.AddListener(SlowBullet.GetComponent<BulletInterface>().BulletClear);
            }
            

            InternalTimer = AttackCoolDown;
        }

        if(InternalOrbTimer<=0)
        {
            var ReltivePos = (Player.transform.position) - this.transform.position;
            float rot_z = Mathf.Atan2(ReltivePos.y, ReltivePos.x) * Mathf.Rad2Deg;

            var OrbBullet = Instantiate(BulletToFire, transform.position, Quaternion.identity);
            OrbBullet.transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
            OrbBullet.GetComponent<BulletInterface>().Velocity = 30;
            OrbBullet.GetComponent<BulletInterface>().EnemyBullet = true;
            OrbBullet.transform.localScale  += new Vector3(5,5,0);

            var OrbExtra1Bullet = Instantiate(BulletToFire, transform.position, Quaternion.identity);
            OrbExtra1Bullet.transform.rotation = Quaternion.Euler(0f, 0f, rot_z);
            OrbExtra1Bullet.GetComponent<BulletInterface>().Velocity = 5;
            OrbExtra1Bullet.GetComponent<BulletInterface>().EnemyBullet = true;
            OrbExtra1Bullet.transform.localScale  += new Vector3(3,3,0);

            var OrbExtra2Bullet = Instantiate(BulletToFire, transform.position, Quaternion.identity);
            OrbExtra2Bullet.transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 180);
            OrbExtra2Bullet.GetComponent<BulletInterface>().Velocity = 5;
            OrbExtra2Bullet.GetComponent<BulletInterface>().EnemyBullet = true;
            OrbExtra2Bullet.transform.localScale  += new Vector3(3,3,0);

            var OrbExtra3Bullet = Instantiate(BulletToFire, transform.position, Quaternion.identity);
            OrbExtra3Bullet.transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
            OrbExtra3Bullet.GetComponent<BulletInterface>().Velocity = 5;
            OrbExtra3Bullet.GetComponent<BulletInterface>().EnemyBullet = true;
            OrbExtra3Bullet.transform.localScale  += new Vector3(3,3,0);

            OrbBullet.GetComponent<SpriteRenderer>().sprite = OrbSprite;
            OrbExtra1Bullet.GetComponent<SpriteRenderer>().sprite = OrbSprite;
            OrbExtra2Bullet.GetComponent<SpriteRenderer>().sprite = OrbSprite;
            OrbExtra3Bullet.GetComponent<SpriteRenderer>().sprite = OrbSprite;

            BossBase.SpellcardChangeCall.AddListener(OrbBullet.GetComponent<BulletInterface>().BulletClear);
            BossBase.SpellcardChangeCall.AddListener(OrbExtra1Bullet.GetComponent<BulletInterface>().BulletClear);
            BossBase.SpellcardChangeCall.AddListener(OrbExtra2Bullet.GetComponent<BulletInterface>().BulletClear);
            BossBase.SpellcardChangeCall.AddListener(OrbExtra3Bullet.GetComponent<BulletInterface>().BulletClear);

            InternalOrbTimer = OrbAttackCoolDown;
        }
        InternalOrbTimer -= Time.deltaTime;
        InternalTimer -= Time.deltaTime;
    }
}
