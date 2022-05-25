using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("Jump variables")]
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpDelay;

    [Header("Dash variables")]
    [SerializeField] private float dashDelay;
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashLenght;
    [SerializeField] private float dashCooldown;
    [SerializeField] private GameObject dashSprite;

    [Header("movement speed")]
    [SerializeField] private float moveSpeed;

    [Header("color scriptable object")]
    [SerializeField] private LevelColors levelColors;

    [Header("time until player dies")]
    [SerializeField] private float timeTillDeath;

    [Header("audio")]
    [SerializeField] private AudioClip jump;
    [SerializeField] private AudioClip dash;

    [Header("max velocity")]
    [SerializeField] private float maxVelocity;

    [Header("dash particles prefab")]
    [SerializeField] private GameObject dashParticles;

    private Rigidbody2D playerRigidbody;
    private Animator animator;
    private float timeBetweenTaps = 0.2f;
    private int taps = 0;
    private float activeMovespeed;
    private float dashCounter;
    private float dashCoolCounter;
    private SpriteRenderer spriteRenderer;
    private float timeTillDeathLocal;
    private AudioSource audioSource;
    private bool canDash;
    private bool isFalling = false;

    void Start()
    {
        playerRigidbody = this.GetComponent<Rigidbody2D>();
        animator = this.GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        dashSprite = GameObject.Find("canDash");
        activeMovespeed = moveSpeed;
        ChangeColor();
    }

    void Update()
    {
        playerRigidbody.velocity = new Vector2(activeMovespeed, playerRigidbody.velocity.y);
        // FallingSpeedHandler();
        // RotationReset();
        DeathTimer();
        if(Input.GetKeyDown("space")){
            JumpOrDashHandler();
        }

        DoubleTapCooldownHandler();
        DashCooldownHandler();
        Debug.DrawRay(this.GetComponent<SpriteRenderer>().bounds.max - new Vector3(1.3f , 0 ,0), -Vector2.up, Color.green);
    }

    void FixedUpdate()
    {
        FallingSpeedHandler();
        if(!isFalling){
            playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, Vector2.ClampMagnitude(playerRigidbody.velocity, maxVelocity).y);
        }
    }

    private bool GroundedCheck(){
        RaycastHit2D frontRay = Physics2D.Raycast(this.GetComponent<SpriteRenderer>().bounds.max, -Vector2.up, 1.3f);
        RaycastHit2D backRay = Physics2D.Raycast(this.GetComponent<SpriteRenderer>().bounds.max - new Vector3(1.9f, 0, 0), -Vector2.up, 1.3f);

        if(!frontRay.collider && !backRay.collider){
            return false;
        }
        else{
            canDash = true;
            dashSprite.SetActive(true);
            return true;
        }
    }

    private void RotationReset(){
        if(this.transform.rotation.z != 0){
            this.transform.rotation = Quaternion.identity;
        }
    }

    private void JumpOrDashHandler(){
        if(timeBetweenTaps > 0 && taps == 1){
            Dash();
        }
        else{
            timeBetweenTaps = dashDelay;;
            taps += 1;
        }
        if(dashCounter <= 0 && GroundedCheck()){
            StartCoroutine(JumpWithDelay());
        }
    }

    private void DoubleTapCooldownHandler(){
        if(timeBetweenTaps > 0){
            timeBetweenTaps -= 1 * Time.deltaTime;
        }
        else{
            taps = 0;
        }
    }

    private void DashCooldownHandler(){
        if(dashCounter > 0){
            dashCounter -= Time.deltaTime;
            playerRigidbody.constraints = RigidbodyConstraints2D.FreezePositionY;

            if(dashCounter <= 0){
                activeMovespeed = moveSpeed;
                dashCoolCounter = dashCooldown;
                playerRigidbody.constraints = RigidbodyConstraints2D.None;
                playerRigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
            }
        }
        if(dashCoolCounter > 0){
            dashCoolCounter -= Time.deltaTime;
        }
    }

    private void FallingSpeedHandler(){
        if(playerRigidbody.velocity.y < 0){
            isFalling = true;
            playerRigidbody.gravityScale = 10;
        }
        else{
            isFalling = false;
            playerRigidbody.gravityScale = 3;
        }
    }

    IEnumerator JumpWithDelay(){
        yield return new WaitForSeconds(jumpDelay);
        playerRigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        audioSource.PlayOneShot(jump);
        animator.Play("rotate");
        ChangeColor();
    }

    void Dash(){
        if(dashCoolCounter <= 0 && dashCounter <= 0 && canDash){
            playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, 0);
            activeMovespeed = dashSpeed;
            dashSprite.SetActive(false);
            dashCounter = dashLenght;
            audioSource.PlayOneShot(dash);
            ChangeColor();
            Instantiate(dashParticles,transform.position,Quaternion.Euler(0,-90,0));
            canDash = false;
        }
    }

    private void ChangeColor(){
        spriteRenderer.color = levelColors.colors[Random.Range(0, levelColors.colors.Length)];
    }

    private void DeathTimer(){
        if(!GroundedCheck() && timeTillDeathLocal > 0){
            timeTillDeathLocal -= Time.deltaTime;
        }
        else{
            timeTillDeathLocal = timeTillDeath;
        }

        if(timeTillDeathLocal <= 0){
            SceneManager.LoadScene("Main Game");
        }
    }
}
