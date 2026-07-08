using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using System;
using Unity.VisualScripting;
// Similar To EnemyBase, but has a spellcard system simialr to the games. By rat queen
public class BossBase : MonoBehaviour, EnemyHitInterface
{
    GameObject Player;
    public int EnemyHp;
    [SerializeField] List<BossAttackMetaData> SpellCardList;
    public GameObject GameMasterObject;
    [SerializeField] MonoBehaviour CurrentAttack;
    [SerializeField] TMP_Text Display;
    public UnityEvent<string> SpellcardChangeCall;
    void Start()
    {
        if (SpellcardChangeCall == null)
        {
            SpellcardChangeCall = new UnityEvent<string>();
        }
       // SpellcardChangeCall.AddListener(Die);

        Display = GameObject.FindGameObjectWithTag("BossHP").GetComponent<TMP_Text>();
        Player = GameObject.FindGameObjectWithTag("Player");
        Player.GetComponent<PlayerBase>().DeathEventCall.AddListener(OnPlayerDeath);

        this.transform.position = Vector3.zero;
        
        
    }

    void Update()
    {
        if(SpellCardList.Count != 0)
        {
            if(SpellCardList[0].IsTimeOut!=true)
            {
                Display.text = "Boss Health: " +  EnemyHp;
            } else
            {
                Display.text = "";
            }
        
        }
    }

    void OnPlayerDeath(string Massage)
    {
        Destroy(this.gameObject); 
    }

    public void ChangeAttack()
    {
        Debug.Log(SpellCardList.Count);
        SpellCardList.Remove(SpellCardList[0]);
        Destroy(CurrentAttack); //Remove the old attack
        if(SpellCardList.Count != 0)
        {
             
            
            
            SpellCardList[0].SpellCardMonoBehaivor.enabled = true;
            EnemyHp = SpellCardList[0].SpellcardHealth;
            CurrentAttack = SpellCardList[0].SpellCardMonoBehaivor;

            SpellcardChangeCall.Invoke("Clear Bullets");
            //BossAttackInterface NewAttack = (BossAttackInterface)this.gameObject.AddComponent(SpellCardList[0].SpellCardMonoBehaivor);
            
        } else
        {

            SpellcardChangeCall.Invoke("Clear Bullets");
            GameMasterObject.GetComponent<Wavespawner>().StartWave();
            Destroy(this.gameObject);
        }
    }
    
    public void Hit(int Damage)
    {
        EnemyHp -= Damage;

        if(EnemyHp<=0)
        {
            ChangeAttack();
        }
    }
}

[System.Serializable]
public struct BossAttackMetaData
{
    public bool IsSpellCard;
    public bool IsTimeOut;
    public int SpellcardHealth;
    public MonoBehaviour SpellCardMonoBehaivor;
}