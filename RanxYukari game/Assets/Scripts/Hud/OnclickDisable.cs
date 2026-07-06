using UnityEngine;

public class OnclickDisable : MonoBehaviour
{
    public GameObject ThingToThisAble; 
    void Start()
    {
        
    }


    void Update()
    {

    }
    
    public void Pressed()
    {
        ThingToThisAble.SetActive(false);   
    }
}
