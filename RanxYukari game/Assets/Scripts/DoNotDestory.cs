using UnityEngine;

public class DoNotDestory : MonoBehaviour
{

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
