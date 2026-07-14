using UnityEngine;
//Purely for effects
public class RotateHitbox : MonoBehaviour
{
    float Rotation;
    public float RotationSpeed;

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(0, 0, 1f * Time.deltaTime * RotationSpeed);
    }
}
