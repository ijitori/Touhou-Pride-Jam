using UnityEngine;

public interface BulletInterface
{
    public float Velocity { get; set; }
    public bool EnemyBullet { get; set; }
    public void BulletClear(string Massage){}
}
