using UnityEngine;

public class TrainMovement : MonoBehaviour, BulletInterface
{
   public float Velocity { get; set; } = 100;
    [SerializeField]
    float MaxDistance;
    Vector3 TrajectoryStart;
    Vector3 LastFramePos;
    float DistanceTravled;
    float TimeAlive;
    public bool EnemyBullet { get; set; }
    bool IsPaused;
    Renderer Renderer;
    Color ColorSave;
    GameObject Player;
    
    void Awake()
    {
        TrajectoryStart = this.transform.position;
        Player = GameObject.FindGameObjectWithTag("Player");
        Renderer = gameObject.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(IsPaused!=true)
        {
        TimeAlive += 1;
        
        DistanceTravled = Vector3.Distance(TrajectoryStart, transform.position);

        LastFramePos = this.transform.position;
        transform.position += transform.up * Velocity * Time.deltaTime;
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        
        }
        if(EnemyBullet==false|| Vector3.Distance(Player.transform.position, this.transform.position) <= 30)
        {
            
            RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, transform.up * -1, Vector2.Distance(new Vector2(transform.position.x,transform.position.y), LastFramePos), this.GetComponent<Collider2D>().contactCaptureLayers);
            foreach (RaycastHit2D hit in hits)
            {
                if(hit)
                {
                
                    HitObject(hit.transform.gameObject);   
                }
            }
        }

        if (TimeAlive == 1000 || DistanceTravled >= MaxDistance)
        {
            
            Destroy(gameObject);
        }
        
    }

    public void BulletClear(string Massage)
    {
        Destroy(this.gameObject); 
    }

    public void PauseBullet(string Massage)
    {
        if(IsPaused==false)
        {
            ColorSave = Renderer.material.color;
            Renderer.material.color = new Color(0f,0f,0f,1f);
            IsPaused = true;
        } else
        {
            Renderer.material.color = ColorSave;
            IsPaused = false;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log(col.gameObject);
        HitObject(col.gameObject);
    }

    void HitObject(GameObject collision)
    {
        
        //var PlayerBase = collision.GetComponent<PlayerBase>();
        var SimpleBulletMovement = collision.GetComponent<SimpleBulletMovement>();
        //if(PlayerBase)
        //{
        //    if(PlayerBase.Immortal != true)
        //    {
        //        PlayerBase.Hit(1);
                //Object.Destroy(this.gameObject);      
        //    }
        //}
            
        if(SimpleBulletMovement)
        {
            if(SimpleBulletMovement.HasBeenTrained!=true)
            {
                SimpleBulletMovement.HasBeenTrained = true;
                SimpleBulletMovement.HitByTrain(60);
            }
        }
    }
}
