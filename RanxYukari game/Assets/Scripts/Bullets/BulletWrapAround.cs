using UnityEngine;

public class BulletWrapAround : MonoBehaviour, BulletInterface
{
    public float Velocity { get; set; } = 10;
    [SerializeField]
    float MaxDistance = 500;
    [SerializeField] bool WraparoundCoolDownBool;
    [SerializeField] float WraparoundCoolDownTotal;
    [SerializeField] float InternalTimer;
    Vector3 TrajectoryStart;
    Vector3 LastFramePos;
    float DistanceTravled;
    public bool EnemyBullet { get; set; }
    public bool HasBeenTrained;
    bool IsPaused;
    Renderer Renderer;
    Color ColorSave;
    GameObject Player;

    public float left = .8f; //Temporary until left to right value is worked out
  public float right = .5f; //Temporary until right to left value is worked out

    
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
        if(WraparoundCoolDownBool!=true)
        {
            var newPosition = transform.position;

            var viewportPosition = Camera.main.WorldToViewportPoint(transform.position);
            if (viewportPosition.x > 1 || viewportPosition.x < 0)
                {
                    if (viewportPosition.x <0) 
                    {
                        newPosition.x = -newPosition.x; //- left;
                        WraparoundCoolDownBool = true;
                    }
            else
                newPosition.x = -newPosition.x;
                WraparoundCoolDownBool = true;
            }
            if (viewportPosition.y > 1 || viewportPosition.y < 0)
            {
                newPosition.y = -newPosition.y; //+ right;
                WraparoundCoolDownBool = true;
            }
            transform.position = newPosition; 
        } else
            {
                InternalTimer -= Time.deltaTime;
                if(InternalTimer<=0)
                {
                    WraparoundCoolDownBool = false;
                    InternalTimer = WraparoundCoolDownTotal;
                }
            }
        
        
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

        if ( DistanceTravled >= 999999) // basically disabled but just to make sure it doesnt glitch
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

    public void HitByTrain(float NewVelocity)
    {
        DistanceTravled = 0;
        Velocity = NewVelocity;
        var ReltivePos = (this.transform.position - Player.transform.position);
        float rotR_z = Mathf.Atan2(ReltivePos.y, ReltivePos.x) * Mathf.Rad2Deg;
            
        this.transform.rotation = Quaternion.Euler(0f, 0f, rotR_z + 90);
    }



    void HitObject(GameObject collision)
    {
        if(EnemyBullet)
        {
            var PlayerBase = collision.GetComponent<PlayerBase>();
            if(PlayerBase)
            {
            if(PlayerBase.Immortal != true)
            {
                PlayerBase.Hit(1);
                Object.Destroy(this.gameObject);      
            }
            }
        } else
        {
            collision.GetComponent<EnemyHitInterface>().Hit(1);
            Object.Destroy(this.gameObject);   
        }
    }
}
