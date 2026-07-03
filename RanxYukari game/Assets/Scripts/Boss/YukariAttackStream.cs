using UnityEngine;

public class YukariAttackStream : MonoBehaviour
{
    [SerializeField] GameObject BulletToFire;
    [SerializeField] float AttackCoolDown;
    [SerializeField] float MoveCoolDown;
    float InternalTimer;
    float MoveInternalTimer;
    GameObject Player;
    BossBase BossBase;

    void OnEnable()
    {
        Debug.Log("Im starting attack");
        Player = GameObject.FindGameObjectWithTag("Player");
        BulletToFire = Resources.Load<GameObject>("Prefabs/Enemy/Enemy Bullet") as GameObject;
        BossBase = this.GetComponent<BossBase>();
    }

    // Update is called once per frame
    void Update()
    {
        if(InternalTimer<=0)
        {
            
            //Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
            

            var AttackRightBullet = Instantiate(BulletToFire, new Vector3(52.1539993f,-29.3759995f,0f), Quaternion.identity);
            var ReltivePosBulletRight = (new Vector3(52.1539993f,-29.3759995f,0f)) - Player.transform.position;
            float rotL_z = Mathf.Atan2(ReltivePosBulletRight.y, ReltivePosBulletRight.x) * Mathf.Rad2Deg;
            
            AttackRightBullet.transform.rotation = Quaternion.Euler(0f, 0f, rotL_z + 90);
            AttackRightBullet.GetComponent<BulletInterface>().Velocity = 30;
            AttackRightBullet.GetComponent<BulletInterface>().EnemyBullet = true;
            
            var AttackLeftBullet = Instantiate(BulletToFire, new Vector3(-52.1539993f,-29.3759995f,0f), Quaternion.identity);
            var ReltivePosBulletLeft = (new Vector3(-52.1539993f,-29.3759995f,0f)) - Player.transform.position;
            float rotR_z = Mathf.Atan2(ReltivePosBulletLeft.y, ReltivePosBulletLeft.x) * Mathf.Rad2Deg;
            
            AttackLeftBullet.transform.rotation = Quaternion.Euler(0f, 0f, rotR_z + 90);
            AttackLeftBullet.GetComponent<BulletInterface>().Velocity = 30;
            AttackLeftBullet.GetComponent<BulletInterface>().EnemyBullet = true;

            var AttackUpperRightBullet = Instantiate(BulletToFire, new Vector3(52.1539993f,29.3759995f,0f), Quaternion.identity);
            var ReltivePosBulletUpperRight = (new Vector3(52.1539993f,29.3759995f,0f)) - Player.transform.position;
            float rotUL_z = Mathf.Atan2(ReltivePosBulletUpperRight.y, ReltivePosBulletUpperRight.x) * Mathf.Rad2Deg;
            
            AttackUpperRightBullet.transform.rotation = Quaternion.Euler(0f, 0f, rotUL_z + 90);
            AttackUpperRightBullet.GetComponent<BulletInterface>().Velocity = 30;
            AttackUpperRightBullet.GetComponent<BulletInterface>().EnemyBullet = true;
            
            var AttackUpperLeftBullet = Instantiate(BulletToFire, new Vector3(-52.1539993f,29.3759995f,0f), Quaternion.identity);
            var ReltivePosBulletUpperLeft = (new Vector3(-52.1539993f,29.3759995f,0f)) - Player.transform.position;
            float rotUR_z = Mathf.Atan2(ReltivePosBulletUpperLeft.y, ReltivePosBulletUpperLeft.x) * Mathf.Rad2Deg;
            
            AttackUpperLeftBullet.transform.rotation = Quaternion.Euler(0f, 0f, rotUR_z + 90);
            AttackUpperLeftBullet.GetComponent<BulletInterface>().Velocity = 30;
            AttackUpperLeftBullet.GetComponent<BulletInterface>().EnemyBullet = true;

            BossBase.SpellcardChangeCall.AddListener(AttackRightBullet.GetComponent<BulletInterface>().BulletClear);
            BossBase.SpellcardChangeCall.AddListener(AttackLeftBullet.GetComponent<BulletInterface>().BulletClear);
            BossBase.SpellcardChangeCall.AddListener(AttackUpperRightBullet.GetComponent<BulletInterface>().BulletClear);
            BossBase.SpellcardChangeCall.AddListener(AttackUpperLeftBullet.GetComponent<BulletInterface>().BulletClear);

            InternalTimer = AttackCoolDown;

        }

        if(MoveInternalTimer<=0)
        {
            MoveAttack();
           MoveInternalTimer = MoveCoolDown; 
        }

        InternalTimer -= Time.deltaTime;
        MoveInternalTimer -= Time.deltaTime;
    }

    void MoveAttack()
    {
        transform.position = Random.insideUnitCircle * 5;
        var RanFloat = Random.Range(-50.0f, 50.0f); //Ran! like the player!

        for(int i = 0; i <= 8; i++)
        {
            var SlowBullet = Instantiate(BulletToFire, transform.position, Quaternion.Euler(0f, 0f, RanFloat + (i * (80))));
            SlowBullet.GetComponent<BulletInterface>().Velocity = 5;
            SlowBullet.GetComponent<BulletInterface>().EnemyBullet = true;

            BossBase.SpellcardChangeCall.AddListener(SlowBullet.GetComponent<BulletInterface>().BulletClear);
        }
    }
}
