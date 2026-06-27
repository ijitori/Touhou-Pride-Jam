using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "DialogTree", menuName = "Scriptable Objects/DialogTree")]
public class DialogTree : ScriptableObject
{
    [SerializeField]
    public List<DialogSection> DialogList;
}
[System.Serializable]
public class DialogSection
{
    public string Dialog; // Dialog that goes in the box
    public bool OnTheLeft;
    public DialogCharacterTheme DialogCharacterTheme;
    public Sprite PortaritLeft; 
    public Sprite PortaritRight;

}
