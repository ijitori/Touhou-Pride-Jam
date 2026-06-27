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
    [SerializeField] List<WaveDialog> WaveDialogList; // When and what dialogs start.
    DialogMaster DialogMaster;
    void Start()
    {
        WaveNumber += 1;
        DialogMaster = this.GetComponent<DialogMaster>();
        StartWave();
    }

    public void StartWave()
    {
        //Dialog and boss checking will go here when its added

        //Spawn enemys
        if(WaveDialogList.Count != 0)
        {
            if(WaveDialogList[0].WaveStartOn == WaveNumber)
            {

                DialogMaster.CurrentDialogTree = WaveDialogList[0].WaveDialogTree;
                WaveDialogList.Remove(WaveDialogList[0]);
                DialogMaster.StartDialog();
                return;
            }
        }
            
            
            Points = (PointsIncressEveryWave * WaveNumber);

            int randEnemyId = Random.Range(0, UsableEnemysList.Count - 1);

            while(Points >= 0)
            {
                Points -= UsableEnemysList[randEnemyId].PointsCost;
                var Enemy = Instantiate(UsableEnemysList[randEnemyId].EnemyObject, GetSpawnPoint(), Quaternion.identity);

                var EnemyBase = Enemy.GetComponent<EnemyBase>();
                EnemyBase.GameMasterObject = this.gameObject;

                EnemysSpawned.Add(Enemy);
            } 
        
    }

    public Vector3 GetSpawnPoint()
    {
        int randX = Random.Range(-25, 25);
        int randY = Random.Range(-25, 25);

        Vector3 spawnPoint = new Vector3(randX, randY, 0.0f);
        

        return spawnPoint;

    }

    public void EnemyDeath(GameObject DyingEnemy)
    {
        EnemysSpawned.Remove(DyingEnemy);

        if(EnemysSpawned.Count==0)
        {
            WaveNumber += 1;
            StartWave();
        }
    }
}
[System.Serializable]
public struct EnemyStruct
{
    public int PointsCost;
    public GameObject EnemyObject;
}
[System.Serializable]
public struct WaveDialog
{
    public DialogTree WaveDialogTree;
    public int WaveStartOn;
}