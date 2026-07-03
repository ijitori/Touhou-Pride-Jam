using UnityEngine;
using UnityEngine.Events;
public class YukariSpellcardBorderBetweenStaticAndDynamic : MonoBehaviour
{
    [SerializeField] GameObject BulletToFire;
    [SerializeField] AnimationCurve CurveX;
    [SerializeField] AnimationCurve CurveY;
    [SerializeField] float AttackCoolDown;
    [SerializeField] float ShiftTime;
    [SerializeField] bool IsXBool; //If this is true, the curve wiill be on the X axis
    [SerializeField] float PillarBulletSpeed;
    float InternalTimer;
    float TotalXTime;
    float TotalYTime;
    float ShiftInternalTime;
    public UnityEvent<string> PauseEvent;
    BossBase BossBase;
    GameObject Player;

    void Start()
    {
        this.transform.position = Vector3.zero;
        BulletToFire = Resources.Load<GameObject>("Prefabs/Enemy/Enemy Bullet") as GameObject;
        Player = GameObject.FindGameObjectWithTag("Player");
        BossBase = this.GetComponent<BossBase>();
        if (PauseEvent == null)
        {
            PauseEvent = new UnityEvent<string>();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(InternalTimer<=0)
        {
            if(IsXBool)
            {
                for(int i = 0; i <= 3; i++)
                {
                    var PillarBullet = Instantiate(BulletToFire, new Vector3(CurveX.Evaluate(TotalXTime + (i * 0.75f)),-29.3759995f,0f), Quaternion.identity);
                    PillarBullet.GetComponent<BulletInterface>().Velocity = PillarBulletSpeed;
                    PillarBullet.GetComponent<BulletInterface>().EnemyBullet = true;
                    
                    

                    PauseEvent.AddListener(PillarBullet.GetComponent<BulletInterface>().PauseBullet);
                    BossBase.SpellcardChangeCall.AddListener(PillarBullet.GetComponent<BulletInterface>().BulletClear);
            
                } 
                var ReltivePos = (Player.transform.position) - this.transform.position;
                float rot_z = Mathf.Atan2(ReltivePos.y, ReltivePos.x) * Mathf.Rad2Deg;

                


                var OrbBullet = Instantiate(BulletToFire, transform.position, Quaternion.identity);
                OrbBullet.transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
                OrbBullet.GetComponent<BulletInterface>().Velocity = 40;
                OrbBullet.GetComponent<BulletInterface>().EnemyBullet = true;
                OrbBullet.transform.localScale  += new Vector3(3,3,0);
                BossBase.SpellcardChangeCall.AddListener(OrbBullet.GetComponent<BulletInterface>().BulletClear);
                PauseEvent.AddListener(OrbBullet.GetComponent<BulletInterface>().PauseBullet);
            } else
            {
                for(int i = 0; i <= 3; i++)
                {
                    var PillarBulle = Instantiate(BulletToFire, new Vector3(-52.1539993f,CurveY.Evaluate(TotalYTime + (i * 0.75f)),0f), Quaternion.identity);
                    PillarBulle.transform.rotation = Quaternion.Euler(0f, 0f, 0f - 90f);
                    PillarBulle.GetComponent<BulletInterface>().Velocity = PillarBulletSpeed;
                    PillarBulle.GetComponent<BulletInterface>().EnemyBullet = true;
                    PauseEvent.AddListener(PillarBulle.GetComponent<BulletInterface>().PauseBullet);
                    BossBase.SpellcardChangeCall.AddListener(PillarBulle.GetComponent<BulletInterface>().BulletClear);
                } 

                for(int i = 0; i <= 8; i++)
                {
                    var SlowBullet = Instantiate(BulletToFire, transform.position, Quaternion.Euler(0f, 0f, 0f + (i * (80))));
                    SlowBullet.GetComponent<BulletInterface>().Velocity = 10;
                    SlowBullet.GetComponent<BulletInterface>().EnemyBullet = true;
            
                    
                    BossBase.SpellcardChangeCall.AddListener(SlowBullet.GetComponent<BulletInterface>().BulletClear);
                    PauseEvent.AddListener(SlowBullet.GetComponent<BulletInterface>().PauseBullet);
                    BossBase.SpellcardChangeCall.AddListener(SlowBullet.GetComponent<BulletInterface>().BulletClear);
                }

                var ReltivePos = (Player.transform.position) - this.transform.position;
                float rot_z = Mathf.Atan2(ReltivePos.y, ReltivePos.x) * Mathf.Rad2Deg;

                var OrbBullet = Instantiate(BulletToFire, transform.position, Quaternion.identity);
                OrbBullet.transform.rotation = Quaternion.Euler(0f, 0f, rot_z + 90);
                OrbBullet.GetComponent<BulletInterface>().Velocity = 10;
                OrbBullet.GetComponent<BulletInterface>().EnemyBullet = true;
                OrbBullet.transform.localScale  += new Vector3(1,1,0);
                BossBase.SpellcardChangeCall.AddListener(OrbBullet.GetComponent<BulletInterface>().BulletClear);
                PauseEvent.AddListener(OrbBullet.GetComponent<BulletInterface>().PauseBullet);
            }
            

            
            InternalTimer = AttackCoolDown;
        }

        if(ShiftInternalTime<=0)
        {
            if(IsXBool==false)
            {
                IsXBool = true;
            } else
            {
                IsXBool = false;
            }
            PauseEvent.Invoke("PauseBullets");

            //InternalTimer = AttackCoolDown;
            
            ShiftInternalTime = ShiftTime;
        }

        InternalTimer -= Time.deltaTime;
        if(IsXBool)
        {
            TotalXTime += Time.deltaTime;
        } else
        {
            TotalYTime += Time.deltaTime;
        }
    
        
        ShiftInternalTime -= Time.deltaTime;
    }
}
