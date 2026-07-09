using UnityEngine;
using UnityEngine.InputSystem;
//Controls Player shooting, by rat queen
public class PlayerAttacking : MonoBehaviour
{
    [SerializeField]
    public InputAction AttackInput;
    [SerializeField]
    GameObject BulletToFire;
    public bool AutoFire;
    [SerializeField] float FireCoolDown;
    float FireTimer;
    Camera Maincamera; //We need the main camera to find where the mouse pos is.
    [SerializeField] Animator Animator;
    private void OnDisable()
    {
        AttackInput.Disable();
        
    }
    private void OnEnable() //These are for the input to not break!
    {
        AttackInput.Enable();
        AttackInput.performed += ToggleFire;
    } 

    void Start()
    {
        Maincamera = Camera.main;
        
    }

    void ToggleFire(InputAction.CallbackContext context)
    {
        if(AutoFire)
        {
            Animator.Play("Idle Shooting", -1, 0f);
            AutoFire = false;
        } else { 
            AutoFire = true;
            Animator.Play("Idle", -1, 0f);
        }
    }

    void FixedUpdate()
    {
        if(AutoFire && FireTimer<=0)
        {
            FireTimer = FireCoolDown;
            Fire();
        } 
        if(AutoFire==false)
        {
            Animator.SetBool("IsShoot", false);
        }

        FireTimer -= Time.deltaTime;
    }

    // Spawns a bullet when called, pew pew!
    void Fire()
    {
        var MousePos = Maincamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        
        var ReltivePos = (MousePos) - this.transform.position; //Subtract the mouse pos with the pos of the gameobject to find how we need to rotate later
        var Bullet = Instantiate(BulletToFire, transform.position, Quaternion.identity);

        float rot_z = Mathf.Atan2(ReltivePos.y, ReltivePos.x) * Mathf.Rad2Deg;
        Animator.SetFloat("Horizontal", ReltivePos.x);
        Animator.SetFloat("Vertical", ReltivePos.y);
        Animator.SetBool("IsShoot", true);
        Bullet.transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
    }
}
