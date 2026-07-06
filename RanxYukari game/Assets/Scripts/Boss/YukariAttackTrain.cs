using UnityEngine;
//yukari and her trains. code by rat queen
public class YukariAttackTrain : MonoBehaviour
{
    [SerializeField] GameObject BulletToFire;
    [SerializeField] GameObject TrainObject;
    [SerializeField] GameObject TrainWarning;
    [SerializeField] AnimationCurve Curve;
    [SerializeField] float AttackCoolDown;
    [SerializeField] float TrainCoolDown;
    [SerializeField] float BulletSpeed;
    [SerializeField] Sprite TrainWarningSprite;
    float InternalTimer;
    float TrainTime;
    float TotalTime;
    [SerializeField]  int ChangeRotationAmount;
    BossBase BossBase;
    GameObject Player;
    void Start()
    {
        TrainTime = TrainCoolDown;
        BulletToFire = Resources.Load<GameObject>("Prefabs/Enemy/Enemy Bullet") as GameObject;
        TrainObject = Resources.Load<GameObject>("Prefabs/Enemy/Train") as GameObject;
        Player = GameObject.FindGameObjectWithTag("Player");
        BossBase = this.GetComponent<BossBase>();
        this.transform.position = new Vector3(0, 20, 0);
        TrainWarning = new GameObject("TrainWarning");
        TrainWarning.transform.position = new Vector3(0, 20, 0);
        var TrainWarningRender = TrainWarning.AddComponent<SpriteRenderer>();
        TrainWarningRender.transform.localScale  += new Vector3(10,150,0);
        TrainWarningRender.material.color = new Color (0.5f, 0.5f, 0.5f, 0.1f);
        TrainWarningRender.sprite = TrainWarningSprite;


    }

    
    void Update()
    {
        if(InternalTimer<=0)
        {
            var CurveRightBullet = Instantiate(BulletToFire, transform.position, Quaternion.identity);
            CurveRightBullet.transform.rotation = Quaternion.Euler(0f, 0f, Curve.Evaluate(TotalTime + (-0.5f * ChangeRotationAmount)));
            CurveRightBullet.GetComponent<BulletInterface>().Velocity = BulletSpeed;
            CurveRightBullet.GetComponent<BulletInterface>().EnemyBullet = true;

            var CurveLeftBullet = Instantiate(BulletToFire, transform.position, Quaternion.identity);
            CurveLeftBullet.transform.rotation = Quaternion.Euler(0f, 0f, Curve.Evaluate(TotalTime + (-0.5f * ChangeRotationAmount)) * -1);
            CurveLeftBullet.GetComponent<BulletInterface>().Velocity = BulletSpeed;
            CurveLeftBullet.GetComponent<BulletInterface>().EnemyBullet = true;

            BossBase.SpellcardChangeCall.AddListener(CurveRightBullet.GetComponent<BulletInterface>().BulletClear);
                BossBase.SpellcardChangeCall.AddListener(CurveLeftBullet.GetComponent<BulletInterface>().BulletClear);

            //var StrightDownBullet = Instantiate(BulletToFire, transform.position, Quaternion.identity);
            //StrightDownBullet.transform.rotation = Quaternion.Euler(0f, 0f, 180 * -1);
            //StrightDownBullet.GetComponent<BulletInterface>().Velocity = BulletSpeed;
            //StrightDownBullet.GetComponent<BulletInterface>().EnemyBullet = true;

            InternalTimer = AttackCoolDown;
        }

        if(TrainTime<=0)
        {
            
            if(TrainTime <= -0.5f)
            {
                var Train = Instantiate(TrainObject, transform.position, TrainWarning.transform.rotation);
                
                TrainTime = TrainCoolDown;    
            }
            
        } else
        {
            var ReltivePos = (this.transform.position - Player.transform.position);
            float rotR_z = Mathf.Atan2(ReltivePos.y, ReltivePos.x) * Mathf.Rad2Deg;
            
            TrainWarning.transform.rotation = Quaternion.Euler(0f, 0f, rotR_z + 90);
        }

        TotalTime += Time.deltaTime;
        InternalTimer -= Time.deltaTime;
        TrainTime -= Time.deltaTime;
    }
}
