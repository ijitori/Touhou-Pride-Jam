using UnityEngine;
//Run Towards the player. Code by rat queen
public class EnemyChaser : MonoBehaviour
{
    GameObject Player;
    [SerializeField] float Speed;
    [SerializeField] float KnockBack;
    [SerializeField] float TurnSpeed;

    
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        var directionVector = (transform.position - Player.transform.position).normalized;
        

        this.transform.position += new Vector3(directionVector.x * Speed, directionVector.y * Speed, 0);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        var PlayerBase = collision.gameObject.GetComponent<PlayerBase>();
        if(PlayerBase.Immortal != true)
        {
            PlayerBase.Hit(1);
            var directionVector = (transform.position - Player.transform.position).normalized;

            Player.transform.position += new Vector3(directionVector.x * KnockBack, directionVector.y * KnockBack, 0);
        }
    }
}
