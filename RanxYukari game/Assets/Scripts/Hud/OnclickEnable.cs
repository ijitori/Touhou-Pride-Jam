using UnityEngine;
using UnityEngine.Audio;

public class OnclickEnable : MonoBehaviour
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
        
        if(ThingToThisAble.activeSelf == true)
        {
            ThingToThisAble.SetActive(false);
            //this.gameObject.SetActive(true);
            
        } else {ThingToThisAble.SetActive(true); }
    }
}
