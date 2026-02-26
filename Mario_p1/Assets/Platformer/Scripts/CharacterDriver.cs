using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;
public class CharacterDriver : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float groundAcceleration = 15f;
    public float walkSpeed = 5f;
    public float runSpeed =10f;
    public float apexHeight = 4.5f;
    public float apexTime = 0.5f;
    Vector2 _Velocity;
    CharacterController _controller;
    Animator _animatior;
    public TextMeshProUGUI CompleteText;
 

    Quaternion facingRight;
    Quaternion facingLeft;
    public TextMeshProUGUI MarioscoreText;
    private int MarioscoreCount = 0;

    void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _animatior = GetComponent<Animator>();
        facingRight = Quaternion.Euler(0, 90f, 0);
        facingLeft = Quaternion.Euler(0, -90f, 0);
        if (MarioscoreText != null)
            MarioscoreText.text = "Mario\n" + MarioscoreCount.ToString("D5");
        if (CompleteText != null)
            CompleteText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
       float direction = 0f;
         if(Keyboard.current.dKey.isPressed) direction += 1f;
         if(Keyboard.current.aKey.isPressed) direction -= 1f;
        // direction = 1f;
         bool jumpPressedThisFrame = Keyboard.current.spaceKey.wasPressedThisFrame;
         bool jumpHeld = Keyboard.current.spaceKey.isPressed;
         float gravityModifier = 1f;

        if(_controller.isGrounded)
        {
            if(direction!= 0f)
            {
                if(Mathf.Sign(direction) != Mathf.Sign(_Velocity.x))
                 _Velocity.x = 0f;
                 _Velocity.x+= direction * groundAcceleration * Time.deltaTime;
                 _Velocity.x = Mathf.Clamp(_Velocity.x, -walkSpeed, walkSpeed);
                    transform.rotation = direction > 0 ? facingRight : facingLeft;
                    
                
            }
            else
            {
                _Velocity.x = Mathf.MoveTowards(_Velocity.x, 0f, groundAcceleration * Time.deltaTime);
            }
            if(jumpPressedThisFrame)
                _Velocity.y = 2f * apexHeight / apexTime;
            
        }
        else
        {
            if(!jumpHeld)
               gravityModifier = 2f;
        }
        float gravity = 2f * apexHeight / (apexTime * apexTime);
        _Velocity.y -= gravity * gravityModifier * Time.deltaTime;

        float deltaX = _Velocity.x * Time.deltaTime;
        float deltaY = _Velocity.y * Time.deltaTime;
        Vector3 deltaPosition = new(deltaX, deltaY,0f);
        CollisionFlags collisions = _controller.Move(deltaPosition);

        if((collisions & CollisionFlags.CollidedAbove) != 0)
            _Velocity.y = -1f;
        if((collisions & CollisionFlags.CollidedSides) != 0)
            _Velocity.x = 0f;
        
      _animatior.SetFloat("Speed", Mathf.Abs(_Velocity.x));
      _animatior.SetBool("Grounded", _controller.isGrounded);

    }
    
   void OnTriggerEnter(Collider other)
   {
    if(other.CompareTag("Brick"))
    {
        MarioscoreCount += 100;
        if (MarioscoreText != null)
            MarioscoreText.text = "Mario\n" + MarioscoreCount.ToString("D5");
        Destroy(other.gameObject);
    }
    else if (other.CompareTag("Finish"))
    {
        Debug.Log("Level Complete!");
        if (CompleteText != null)
        {
            CompleteText.text = "You Won!";
            CompleteText.gameObject.SetActive(true);
            gameObject.tag = "Player";
            Destroy(gameObject);
        }
    }
    else if(other.CompareTag("Enemy"))
    {
        Debug.Log("Game Over!");
        if (CompleteText != null)
        {
            CompleteText.text = "Game Over!";
            CompleteText.gameObject.SetActive(true);
            gameObject.tag = "Player";
            Destroy(gameObject);
        }
    }
   }

   void OnControllerColliderHit(ControllerColliderHit hit)
   {
    if (hit.gameObject.CompareTag("Brick"))
    {
        if (transform.position.y < hit.transform.position.y && _Velocity.y > 0)
        {
            MarioscoreCount += 100;
            if (MarioscoreText != null)
                MarioscoreText.text = "Mario\n" + MarioscoreCount.ToString("D5");
            Destroy(hit.gameObject);
        }
    }
   }
  
}
