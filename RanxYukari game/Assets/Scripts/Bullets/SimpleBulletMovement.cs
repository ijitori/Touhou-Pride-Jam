using UnityEngine;

public class SimpleBulletMovement : MonoBehaviour
{
    public float Velocity = 100;
    [SerializeField]
    float MaxDistance;
    Vector3 TrajectoryStart;
    Vector3 LastFramePos;
    float DistanceTravled;
    float TimeAlive;
    void Start()
    {
        TrajectoryStart = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        TimeAlive += 1;
        
        DistanceTravled = Vector3.Distance(TrajectoryStart, transform.position);

        LastFramePos = this.transform.position;
        transform.position += transform.up * Velocity * Time.deltaTime;
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        Debug.DrawRay(transform.position, Vector2.down, Color.red, 10.0f);

        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, transform.up * -1, Vector2.Distance(new Vector2(transform.position.x,transform.position.y), LastFramePos), this.GetComponent<Collider2D>().contactCaptureLayers);
        foreach (RaycastHit2D hit in hits)
        {
            if(hit)
            {
                HitObject(hit.transform.gameObject);   
            }
        }

        if (TimeAlive == 1000 || DistanceTravled >= MaxDistance)
        {
            
            Destroy(gameObject);
        }
    }

    void HitObject(GameObject collision)
    {
        collision.GetComponent<EnemyBase>().Hit(1);
    }
}
