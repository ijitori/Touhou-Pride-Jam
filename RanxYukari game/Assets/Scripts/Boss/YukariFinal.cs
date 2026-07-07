using UnityEngine;

public class YukariFinal : MonoBehaviour
{
    [SerializeField] GameObject BulletToFire;
    [SerializeField] float AttackCoolDown;
    [SerializeField] float OrbAttackCoolDown;
    [SerializeField] float totalTime;
    [SerializeField] int PillarAmount;
    [SerializeField] AnimationCurve Curve;
    [SerializeField] AnimationCurve WallAnimation;
    [SerializeField] float CurveBulletSpeed;
    [SerializeField] float CurveCoolDownSpeed;
    [SerializeField]  int ChangeRotationAmount;
    [SerializeField] GameObject WallObject;
    [SerializeField] AnimationCurve CirlceCurve;
    [SerializeField] float CricleCoolDown;
    [SerializeField] Sprite OrbBulltSprite;
    [SerializeField] Sprite CurveBulltSprite;
    GameObject LeftWall;
    GameObject RightWall;
    GameObject UpWall;
    GameObject DownWall;
    bool FirstBulletClearToggle;
    float InternalTimer;
    float WallTotalTIme;
    float InternalOrbTimer;
    float CricleTimer;
    BossBase BossBase;
    GameObject Player;
    void OnEnable()
    {
        Debug.Log("Im starting attack");
        Player = GameObject.FindGameObjectWithTag("Player");
        WallObject = Resources.Load<GameObject>("Prefabs/Wall") as GameObject;
        BulletToFire = Resources.Load<GameObject>("Prefabs/Enemy/Wraparound Enemy Bullet") as GameObject;
        BossBase = this.GetComponent<BossBase>();
        this.transform.position = new Vector3(0, 0, 0);
        LeftWall = (Instantiate(WallObject, new Vector3(110,18.4916992f,0), Quaternion.identity));
        RightWall = (Instantiate(WallObject, new Vector3(-110f,18.4916992f,0), Quaternion.identity));
        DownWall = (Instantiate(WallObject, new Vector3(0,-110f,0f), Quaternion.identity));
        UpWall = (Instantiate(WallObject, new Vector3(0,110f,0f), Quaternion.identity));
        
        
    }

    
    void Update()
    {
        
        if(totalTime<=10)
        {
            if(InternalOrbTimer<=0)
            {
                var ReltivePos = (Player.transform.position) - this.transform.position;
                float rot_z = Mathf.Atan2(ReltivePos.y, ReltivePos.x) * Mathf.Rad2Deg;

                var OrbBullet = Instantiate(BulletToFire, transform.position, Quaternion.identity);
                OrbBullet.transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
                OrbBullet.GetComponent<BulletInterface>().Velocity = 30;
                OrbBullet.GetComponent<BulletInterface>().EnemyBullet = true;
                OrbBullet.transform.localScale  = new Vector3(9,9,0);
                OrbBullet.GetComponent<SpriteRenderer>().sprite = OrbBulltSprite;

                var OrbExtra3Bullet = Instantiate(BulletToFire, transform.position, Quaternion.identity);
                OrbExtra3Bullet.transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
                OrbExtra3Bullet.GetComponent<BulletInterface>().Velocity = 5;
                OrbExtra3Bullet.GetComponent<BulletInterface>().EnemyBullet = true;
                OrbExtra3Bullet.transform.localScale  = new Vector3(9,9,0);
                OrbExtra3Bullet.GetComponent<SpriteRenderer>().sprite = OrbBulltSprite;
                //OrbExtra3Bullet.transform.localScale  += new Vector3(3,3,0);

                BossBase.SpellcardChangeCall.AddListener(OrbBullet.GetComponent<BulletInterface>().BulletClear);
                BossBase.SpellcardChangeCall.AddListener(OrbExtra3Bullet.GetComponent<BulletInterface>().BulletClear);

                InternalOrbTimer = OrbAttackCoolDown;
            }
        } else if(totalTime<=12)
        {
            if(InternalTimer<=0)
            {
                InternalTimer = AttackCoolDown;
                var CurveRightBullet = Instantiate(BulletToFire, transform.position, Quaternion.identity);
                CurveRightBullet.transform.rotation = Quaternion.Euler(0f, 0f, Curve.Evaluate(totalTime + (-0.5f * ChangeRotationAmount)));
                CurveRightBullet.GetComponent<BulletInterface>().Velocity = CurveBulletSpeed;
                CurveRightBullet.GetComponent<BulletInterface>().EnemyBullet = true; 
                

                var CurveLeftBullet = Instantiate(BulletToFire, transform.position, Quaternion.identity);
                CurveLeftBullet.transform.rotation = Quaternion.Euler(0f, 0f, Curve.Evaluate(totalTime + (-0.5f * ChangeRotationAmount)) * -1);
                CurveLeftBullet.GetComponent<BulletInterface>().Velocity = CurveBulletSpeed;
                CurveLeftBullet.GetComponent<BulletInterface>().EnemyBullet = true;

                CurveRightBullet.GetComponent<SpriteRenderer>().sprite = CurveBulltSprite;
                CurveLeftBullet.GetComponent<SpriteRenderer>().sprite = CurveBulltSprite;

                BossBase.SpellcardChangeCall.AddListener(CurveRightBullet.GetComponent<BulletInterface>().BulletClear);
                BossBase.SpellcardChangeCall.AddListener(CurveLeftBullet.GetComponent<BulletInterface>().BulletClear);
            }
        } else if(totalTime<=14)
        {
            if(InternalTimer<=0)
            {
                InternalTimer = AttackCoolDown;
                var CurveRightBullet = Instantiate(BulletToFire, transform.position, Quaternion.identity);
                CurveRightBullet.transform.rotation = Quaternion.Euler(0f, 0f, Curve.Evaluate(totalTime + (-0.5f * ChangeRotationAmount)) + 90);
                CurveRightBullet.GetComponent<BulletInterface>().Velocity = CurveBulletSpeed;
                CurveRightBullet.GetComponent<BulletInterface>().EnemyBullet = true; 

                var CurveLeftBullet = Instantiate(BulletToFire, transform.position, Quaternion.identity);
                CurveLeftBullet.transform.rotation = Quaternion.Euler(0f, 0f, Curve.Evaluate(totalTime + (-0.5f * ChangeRotationAmount)) * -1 + 90);
                CurveLeftBullet.GetComponent<BulletInterface>().Velocity = CurveBulletSpeed;
                CurveLeftBullet.GetComponent<BulletInterface>().EnemyBullet = true;

                CurveRightBullet.GetComponent<SpriteRenderer>().sprite = CurveBulltSprite;
                CurveLeftBullet.GetComponent<SpriteRenderer>().sprite = CurveBulltSprite;

                BossBase.SpellcardChangeCall.AddListener(CurveRightBullet.GetComponent<BulletInterface>().BulletClear);
                BossBase.SpellcardChangeCall.AddListener(CurveLeftBullet.GetComponent<BulletInterface>().BulletClear);
            }
        } else if(totalTime<=17)
        {
            if(InternalTimer<=0)
            {
                InternalTimer = AttackCoolDown;
                var CurveRightBullet = Instantiate(BulletToFire, transform.position, Quaternion.identity);
                CurveRightBullet.transform.rotation = Quaternion.Euler(0f, 0f, Curve.Evaluate(totalTime + (-0.5f * ChangeRotationAmount)) + 180);
                CurveRightBullet.GetComponent<BulletInterface>().Velocity = CurveBulletSpeed;
                CurveRightBullet.GetComponent<BulletInterface>().EnemyBullet = true; 

                var CurveLeftBullet = Instantiate(BulletToFire, transform.position, Quaternion.identity);
                CurveLeftBullet.transform.rotation = Quaternion.Euler(0f, 0f, Curve.Evaluate(totalTime + (-0.5f * ChangeRotationAmount)) * -1 + 180);
                CurveLeftBullet.GetComponent<BulletInterface>().Velocity = CurveBulletSpeed;
                CurveLeftBullet.GetComponent<BulletInterface>().EnemyBullet = true;

                CurveRightBullet.GetComponent<SpriteRenderer>().sprite = CurveBulltSprite;
                CurveLeftBullet.GetComponent<SpriteRenderer>().sprite = CurveBulltSprite;

                BossBase.SpellcardChangeCall.AddListener(CurveRightBullet.GetComponent<BulletInterface>().BulletClear);
                BossBase.SpellcardChangeCall.AddListener(CurveLeftBullet.GetComponent<BulletInterface>().BulletClear);
            }
        } else if(totalTime<=19)
        {
            if(InternalTimer<=0)
            {
                InternalTimer = AttackCoolDown;
                var CurveRightBullet = Instantiate(BulletToFire, transform.position, Quaternion.identity);
                CurveRightBullet.transform.rotation = Quaternion.Euler(0f, 0f, Curve.Evaluate(totalTime + (-0.5f * ChangeRotationAmount)) - 90);
                CurveRightBullet.GetComponent<BulletInterface>().Velocity = CurveBulletSpeed;
                CurveRightBullet.GetComponent<BulletInterface>().EnemyBullet = true; 

                var CurveLeftBullet = Instantiate(BulletToFire, transform.position, Quaternion.identity);
                CurveLeftBullet.transform.rotation = Quaternion.Euler(0f, 0f, Curve.Evaluate(totalTime + (-0.5f * ChangeRotationAmount)) * -1 - 90);
                CurveLeftBullet.GetComponent<BulletInterface>().Velocity = CurveBulletSpeed;
                CurveLeftBullet.GetComponent<BulletInterface>().EnemyBullet = true;

                CurveRightBullet.GetComponent<SpriteRenderer>().sprite = CurveBulltSprite;
                CurveLeftBullet.GetComponent<SpriteRenderer>().sprite = CurveBulltSprite;

                BossBase.SpellcardChangeCall.AddListener(CurveRightBullet.GetComponent<BulletInterface>().BulletClear);
                BossBase.SpellcardChangeCall.AddListener(CurveLeftBullet.GetComponent<BulletInterface>().BulletClear);
            }
        }

        if(totalTime>=25)
        {
            WallTotalTIme  += Time.deltaTime;
            LeftWall.transform.position = new Vector3(110f + (-22.1f * WallAnimation.Evaluate(WallTotalTIme)), 18.4916992f, 0);
            RightWall.transform.position = new Vector3(-110f - (-22.1f * WallAnimation.Evaluate(WallTotalTIme)), 18.4916992f, 0);
            UpWall.transform.position = new Vector3(0, -110f - (-28.1f * WallAnimation.Evaluate(WallTotalTIme)), 0);
            DownWall.transform.position = new Vector3(0, 110f + (-28.1f * WallAnimation.Evaluate(WallTotalTIme)), 0);
        }

        if(totalTime>=35)
        {
            if(FirstBulletClearToggle==false)
            {
                BossBase.SpellcardChangeCall.Invoke("Clear Bullets");
                FirstBulletClearToggle = true;

                
            }
            CricleTimer -= Time.deltaTime;
            if(totalTime<=36)
            {
            if(CricleTimer<=0)
            {
                var OrbBullet = Instantiate(BulletToFire, transform.position, Quaternion.identity);
                OrbBullet.transform.rotation = Quaternion.Euler(0f, 0f, CirlceCurve.Evaluate(totalTime));
                OrbBullet.GetComponent<BulletInterface>().Velocity = 20;
                OrbBullet.GetComponent<BulletInterface>().EnemyBullet = true;
                OrbBullet.transform.localScale  = new Vector3(9,9,0);
                OrbBullet.GetComponent<SpriteRenderer>().sprite = CurveBulltSprite;
                

                BossBase.SpellcardChangeCall.AddListener(OrbBullet.GetComponent<BulletInterface>().BulletClear);

                CricleTimer = CurveCoolDownSpeed;
            }
            }
            
        }

        if(totalTime>=50)
        {
            BossBase.ChangeAttack();
        }

        InternalOrbTimer -= Time.deltaTime;
        InternalTimer -= Time.deltaTime;
        totalTime += Time.deltaTime;
    }

    void OnDestroy()
    {
        Destroy(RightWall); 
        Destroy(LeftWall); 
        Destroy(UpWall); 
        Destroy(DownWall); 
    }
}
