using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
//Controlls how the player moves, by rat queen
public class PlayerMovement : MonoBehaviour
{
    Renderer SpriteRenderer;
    PlayerBase PlayerBase;

    [SerializeField]
    public InputAction MovementInput;
    [SerializeField]
    public InputAction FocusInput;
    [SerializeField]
    public InputAction DashInput;

    [SerializeField]
    float UnfocusedMoveSpeed; // How fast the player pushes a object.
    [SerializeField]
    float focusedMoveSpeed;
    [SerializeField]
    float DashSpeed;
    [SerializeField]

    bool Dashing;
    [SerializeField]
    float DashCoolDown;
    [SerializeField] Animator Animator;
    float DashCoolDownTimer;
    float startTime;
    [SerializeField]
    float journeyTime;
    Vector3 DashTargetPos;
    Vector3 DashStartPos;
    
    [SerializeField]
    Vector2 MoveDir; //Where the player inputs are "pushing" the objcet
    Vector3 TargetPos; //Where the object is going.

    public bool IsFocused;
    public bool AnimationRest;
    bool CollidingWithBordar;

    GameObject DashAfterImage;
    PlayerAttacking PlayerAttacking;

    private void OnDisable()
    {
        MovementInput.Disable();
        FocusInput.Disable();
        DashInput.Disable();
    }
    private void OnEnable() //These are for the input to not break!
    {
        MovementInput.Enable();
        FocusInput.Enable();
        DashInput.Enable();
        DashInput.performed += Dash;
    } 
    void Start()
    {
        SpriteRenderer = this.GetComponent<Renderer>();
        PlayerBase = this.GetComponent<PlayerBase>();
        PlayerAttacking = this.GetComponent<PlayerAttacking>();
        //Animator = this.GetComponent<Animator>();
        //Debug.Log(Animator);
    }

    
    void FixedUpdate()
    {
        MoveDir = MovementInput.ReadValue<Vector2>();
        var FocusInputs = (FocusInput.ReadValue<float>());
        var MoveSpeed = UnfocusedMoveSpeed;
        
        if(Dashing) //Dashing
        {
            
            float Count = (startTime += Time.deltaTime) / journeyTime;
            transform.position = Vector3.Lerp(DashStartPos, DashTargetPos, Count);
            if(transform.position == DashTargetPos)
            {
                SpriteRenderer.material.color = Color.white;
                MovementInput.Enable(); //If this line is removed then normal inputs wont work after dashing !!!!!
                Destroy(DashAfterImage);
                Dashing = false;
                PlayerBase.Immortal = false;
            }
        } 

        if(DashCoolDownTimer >= 0)
        {
            DashCoolDownTimer -= Time.deltaTime;
        } //Dashing cooldown

        if (FocusInput.triggered)
        {
            IsFocused = true;
            MoveSpeed = focusedMoveSpeed; 
        } else 
        { 
            IsFocused = false;
        }

        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, MoveDir, MoveSpeed, this.GetComponent<Collider2D>().contactCaptureLayers);
        Debug.DrawRay(transform.position, MoveDir, Color.red, MoveSpeed);
        var stop = false;
        foreach (RaycastHit2D hit in hits)
            {
                if(hit)
                {
                
                    stop = true;   
                }
            }

        if(MoveDir != Vector2.zero && stop!=true)
        {
            Animator.SetBool("IsMoving", true);
            TargetPos += new Vector3(MoveDir.x * MoveSpeed, MoveDir.y * MoveSpeed, 0); 
            var NewPos = new Vector3(MoveDir.x * MoveSpeed, MoveDir.y * MoveSpeed, 0);  
            AnimationRest = true;

            if(PlayerAttacking.AutoFire == false)
            {
                
                Animator.SetFloat("Horizontal", MoveDir.x);
                Animator.SetFloat("Vertical", MoveDir.y);
            }
            
            transform.position += NewPos;
        } else
        {
            if(AnimationRest == true)
            {
                Animator.Play("Idle", -1, 1.1f);
                AnimationRest = false;    
            }
            
            Animator.SetBool("IsMoving", false);
            
            TargetPos = transform.position; //Rest Target Pos after we stop moving.
        }

    }

    void Dash(InputAction.CallbackContext context) //Starts the dashing process and disables movement inputs
    {
        if(DashCoolDownTimer <= 0)
        {
            DashAfterImage = Instantiate(Resources.Load<GameObject>("Particals/after images") as GameObject, transform.position, Quaternion.identity);
            DashAfterImage.transform.SetParent(this.transform);
            DashStartPos = this.transform.position;
            DashTargetPos = new Vector3(transform.position.x + (MoveDir.x * DashSpeed), transform.position.y + (MoveDir.y * DashSpeed), 0); 

            RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, MoveDir, DashSpeed, this.GetComponent<Collider2D>().contactCaptureLayers);
            Debug.DrawRay(transform.position, DashTargetPos, Color.red, 10.0f);
            foreach (RaycastHit2D hit in hits)
            {
                if(hit)
                {
                
                    DashTargetPos = hit.point;   
                }
            }

            
            DashCoolDownTimer = DashCoolDown;
            startTime = 0;
            SpriteRenderer.material.color = Color.blue;
            Dashing = true;
            PlayerBase.Immortal = true;
            
            MovementInput.Disable(); 
        }
    }
}

    
