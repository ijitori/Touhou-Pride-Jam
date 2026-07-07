using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//using UnityEngine.UIElements;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;
//Manage the UI and dialog relating to UI. By rat queen
public class DialogMaster : MonoBehaviour
{
    [SerializeField] GameObject Ui;
    [SerializeField] Image PortaritLeft;
    [SerializeField] Image PortaritRight;
    [SerializeField] TMP_Text ContentText; //I dont know a better way to get he gameobjects, I could get them via transform but but that's asking for the code to break. This is the best thing i know of :(
    [SerializeField] public DialogTree CurrentDialogTree;
    [SerializeField] int DialogTreeListPlacement; //what place on the list our dialog is on
    [SerializeField] InputAction DialogInput;
    public bool IsEnding;

    PlayerAttacking PlayerAttacking;
    PlayerBase PlayerBase;
    PlayerMovement PlayerMovement;

    Wavespawner Wavespawner;

    private void OnDisable()
    {
        DialogInput.Disable();
        
    }
    private void OnEnable() //These are for the input to not break!
    {
        DialogInput.Enable();
        DialogInput.performed += MoveDialog;
    } 

    void Start()
    {
        Ui.SetActive(false); //Set UI to false if its active.

        if(IsEnding == false)
        {
            var Player = GameObject.FindGameObjectWithTag("Player");

            PlayerAttacking = Player.GetComponent<PlayerAttacking>();
            PlayerBase = Player.GetComponent<PlayerBase>();
            PlayerMovement = Player.GetComponent<PlayerMovement>();
        
            Wavespawner = this.GetComponent<Wavespawner>();
        } else
        {
            StartDialog();
        }
    }

    public void StartDialog()
    {
        Ui.SetActive(true);
        DialogTreeListPlacement = 0;

        var TreePlacement = CurrentDialogTree.DialogList[DialogTreeListPlacement];

        if(IsEnding == false)
        {
            PlayerMovement.MovementInput.Disable();
            PlayerMovement.FocusInput.Disable();
            PlayerMovement.DashInput.Disable();
            PlayerAttacking.AttackInput.Disable();
            PlayerBase.Immortal = true;

            PlayerAttacking.AutoFire = false;
        }

        DialogInput.Enable();
        
        PortaritLeft.sprite = TreePlacement.PortaritLeft;
        PortaritRight.sprite = TreePlacement.PortaritRight;
        ContentText.color = TreePlacement.DialogCharacterTheme.TextColor;

        if (TreePlacement.OnTheLeft)
        {
            PortaritLeft.color = new Color(1f, 1f, 1f);
            PortaritRight.color = new Color(0.4f, 0.4f, 0.4f);
        }
        else
        {
            PortaritLeft.color = new Color(0.4f, 0.4f, 0.4f);
            PortaritRight.color = new Color(1f, 1f, 1f);
            ContentText.text += "<align=right>";
        }

        ContentText.text = ""; //Rest text
        StartCoroutine(WriteText(TreePlacement.Dialog));
    }

    public void MoveDialog(InputAction.CallbackContext context)
    {
        if(Ui.active) //check if the dialog is active to make sure this isnt being called when the UI isnt there.
        {
            DialogTreeListPlacement += 1;
            ContentText.text = ""; //Rest text
            if(CurrentDialogTree.DialogList.Count - 1 >= DialogTreeListPlacement )
            {
                var TreePlacement = CurrentDialogTree.DialogList[DialogTreeListPlacement];

                if (TreePlacement.OnTheLeft)
                {
                    PortaritLeft.color = new Color(1f, 1f, 1f);
                    PortaritRight.color = new Color(0.4f, 0.4f, 0.4f);
                }
                else
                {
                    PortaritLeft.color = new Color(0.4f, 0.4f, 0.4f);
                    PortaritRight.color = new Color(1f, 1f, 1f);
                    ContentText.text += "<align=right>";
                }
        
                PortaritLeft.sprite = TreePlacement.PortaritLeft;
                PortaritRight.sprite = TreePlacement.PortaritRight;
                StartCoroutine(WriteText(TreePlacement.Dialog));
            }   else
            {
                
                EndDialog();
            }
        }
    }

    IEnumerator WriteText(string Text)
    {
        char[] nextLetter = Text.ToCharArray();

        for (int i = 0; i < Text.Length; i++)
        {
            ContentText.text += nextLetter[i];
            //if (Input.GetKeyDown(KeyCode.X))
            //{
            //    TextBody.text = Talker[Whatbox].WhatIHaveToSay;
            //    i = WhatSay.Length;
            //}
            yield return  new WaitForSeconds(0.03f);
        }
        yield return null;
    }

    public void EndDialog()
    {
        Ui.SetActive(false);
        ContentText.text = ""; //Rest text
        DialogInput.Disable();
        if(IsEnding == false)
        {
            PlayerBase.Immortal = false;

            PlayerMovement.MovementInput.Enable();
            PlayerMovement.FocusInput.Enable();
            PlayerMovement.DashInput.Enable();
            PlayerAttacking.AttackInput.Enable();

            Wavespawner.StartWave();   
        }
    }
}
