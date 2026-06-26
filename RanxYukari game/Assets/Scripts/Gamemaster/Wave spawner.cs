using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//Spawns enemys via waves
public class Wavespawner : MonoBehaviour
{
    [SerializeField]
    int WaveNumber;
    [SerializeField]
    int Points; //Each enemy costs points to spawn, the game will spawn enemys until the game master runs out of points
     [SerializeField]
    int PointsIncressEveryWave;
    [SerializeField] List<EnemyStruct> UsableEnemysList; //Spawnable enemys the game can use.
    [SerializeField] List<GameObject> EnemysSpawned; // Enemys on the field.
    void Start()
    {
        StartWave();
    }

    public void StartWave()
    {
        WaveNumber += 1;
        Points = (PointsIncressEveryWave * WaveNumber);

        int randEnemyId = Random.Range(0, UsableEnemysList.Count - 1);

        while(Points >= 0)
        {
            Points -= UsableEnemysList[randEnemyId].PointsCost;
            var Enemy = Instantiate(UsableEnemysList[randEnemyId].EnemyObject, Vector3.zero, Quaternion.identity);

            var EnemyBase = Enemy.GetComponent<EnemyBase>();
            EnemyBase.GameMasterObject = this.gameObject;
        }
    }
}
[System.Serializable]
public struct EnemyStruct
{
    public int PointsCost;
    public GameObject EnemyObject;
}