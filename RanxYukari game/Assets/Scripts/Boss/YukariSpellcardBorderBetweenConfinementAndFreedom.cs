using System.Collections.Generic;
using UnityEngine;
//I HATE MOVEMENT. code by rat queen
public class YukariSpellcardBorderBetweenConfinementAndFreedom : MonoBehaviour
{
    [SerializeField] GameObject BulletToFire;
    [SerializeField] GameObject WallObject;
    [SerializeField] AnimationCurve Curve;
    [SerializeField] AnimationCurve CirlceCurve;
    [SerializeField] float AttackCoolDown;
    
    float InternalTimer;
    float TotalTime;
    BossBase BossBase;
    GameObject Player;
    GameObject LeftWall;
    GameObject RightWall;
    GameObject UpWall;
    GameObject DownWall; //is this the best way to do it? probably not
    
    void Start()
    {
        BulletToFire = Resources.Load<GameObject>("Prefabs/Enemy/Enemy Bullet") as GameObject;
        WallObject = Resources.Load<GameObject>("Prefabs/Wall") as GameObject;
        Player = GameObject.FindGameObjectWithTag("Player");
        BossBase = this.GetComponent<BossBase>();

        LeftWall = (Instantiate(WallObject, new Vector3(110,18.4916992f,0), Quaternion.identity));
        RightWall = (Instantiate(WallObject, new Vector3(-110f,18.4916992f,0), Quaternion.identity));
        DownWall = (Instantiate(WallObject, new Vector3(0,-110f,0f), Quaternion.identity));
        UpWall = (Instantiate(WallObject, new Vector3(0,110f,0f), Quaternion.identity));
        
    }

    
    void Update()
    {
        LeftWall.transform.position = new Vector3(110f + (-32.1f * Curve.Evaluate(TotalTime)), 18.4916992f, 0);
        RightWall.transform.position = new Vector3(-110f - (-32.1f * Curve.Evaluate(TotalTime)), 18.4916992f, 0);
        UpWall.transform.position = new Vector3(0, -110f - (-32.1f * Curve.Evaluate(TotalTime)), 0);
        DownWall.transform.position = new Vector3(0, 110f + (-32.1f * Curve.Evaluate(TotalTime)), 0);

        if(InternalTimer<=0)
        {
            var OrbBullet = Instantiate(BulletToFire, transform.position, Quaternion.identity);
            OrbBullet.transform.rotation = Quaternion.Euler(0f, 0f, CirlceCurve.Evaluate(TotalTime));
            OrbBullet.GetComponent<BulletInterface>().Velocity = 40;
            OrbBullet.GetComponent<BulletInterface>().EnemyBullet = true;

            var OrbBullet2 = Instantiate(BulletToFire, transform.position, Quaternion.identity);
            OrbBullet2.transform.rotation = Quaternion.Euler(0f, 0f, CirlceCurve.Evaluate(TotalTime) - 90);
            OrbBullet2.GetComponent<BulletInterface>().Velocity = 10;
            OrbBullet2.GetComponent<BulletInterface>().EnemyBullet = true;

            var ReltivePos = (Player.transform.position) - this.transform.position;
            float rot_z = Mathf.Atan2(ReltivePos.y, ReltivePos.x) * Mathf.Rad2Deg;

            var OrbBullet3 = Instantiate(BulletToFire, transform.position, Quaternion.identity);
            OrbBullet3.transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
            OrbBullet3.GetComponent<BulletInterface>().Velocity = 35;
            OrbBullet3.GetComponent<BulletInterface>().EnemyBullet = true;
            OrbBullet3.transform.localScale  += new Vector3(1,1,0);
            BossBase.SpellcardChangeCall.AddListener(OrbBullet3.GetComponent<BulletInterface>().BulletClear);

            BossBase.SpellcardChangeCall.AddListener(OrbBullet.GetComponent<BulletInterface>().BulletClear);
            BossBase.SpellcardChangeCall.AddListener(OrbBullet2.GetComponent<BulletInterface>().BulletClear);
            
            InternalTimer = AttackCoolDown;
        }

        TotalTime += Time.deltaTime; 
        InternalTimer -= Time.deltaTime;
    }
    void OnDestroy()
    {
        Destroy(RightWall); 
        Destroy(LeftWall); 
        Destroy(UpWall); 
        Destroy(DownWall); 
    }
}
