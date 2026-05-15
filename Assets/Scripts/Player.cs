using System.Runtime.CompilerServices;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float jumpForce = 10f;
    private bool isGrounded;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;
    private Vector2 touchStartPos;
    private Vector2 touchEndPos;
    [SerializeField] private float swipeThreshold = 50f;
    private Animator anim;
    [SerializeField] BoxCollider2D normalCollider;
    [SerializeField] CapsuleCollider2D duckCollider;
    bool didSwipeUp = false;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        normalCollider.enabled = true;
        duckCollider.enabled = false;

    }

    void Update()
    {
        isGrounded = CheckIfGround();
        HandleJump();
        HandleDuck();
        HandleSoundEffect();
    }
    private bool CheckIfGround()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }

    private void HandleJump()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                touchStartPos = touch.position;
                StartDuck();
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                touchEndPos = touch.position;
                StopDuck();
                if (touchEndPos.y - touchStartPos.y > swipeThreshold && isGrounded)
                {
                    rb.velocity = Vector2.up * jumpForce;
                }
            }
        }

    }
    private bool IsMouseSwipeUp()
    {
        if (Input.GetMouseButtonDown(0))
        {
            touchStartPos = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            touchEndPos = Input.mousePosition;
            return (touchEndPos.y - touchStartPos.y > swipeThreshold);
        }
        return false;
    }
    private void HandleDuck()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartDuck();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopDuck();
        }
    }
    private void StartDuck()
    {
        normalCollider.enabled = false;
        duckCollider.enabled = true;
        anim.SetBool("isDuck", true);
    }

    private void StopDuck()
    {
        normalCollider.enabled = true;
        duckCollider.enabled = false;
        anim.SetBool("isDuck", false);
    }
    private void HandleSoundEffect()
    {
        if (AudioManager.instance == null) return;

        if (didSwipeUp && isGrounded)
        {
            AudioManager.instance.PlayJumpClip();
            AudioManager.instance.SetHasPlayEffectSound(true);
            didSwipeUp = false; // Đặt lại cờ vuốt
        }
        else if (isGrounded && !AudioManager.instance.HasPlayEffectSound())
        {
            AudioManager.instance.PlayTapClip();
            AudioManager.instance.SetHasPlayEffectSound(true);
        }
        else if (!isGrounded)
        {
            AudioManager.instance.SetHasPlayEffectSound(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle") && AudioManager.instance != null)
        {
            AudioManager.instance.PlayHurtClip();
        }
    }
}