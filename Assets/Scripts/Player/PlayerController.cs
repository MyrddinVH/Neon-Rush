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



    private Rigidbody2D playerRigidbody;
    private float timeBetweenTaps = 0.2f;
    private int taps = 0;
    private float activeMovespeed;
    private float dashCounter;
    private float dashCoolCounter;
    private SpriteRenderer spriteRenderer;
    private float timeTillDeathLocal;
    private AudioSource audioSource;
    public bool canDash;




    void Start()
    {
        playerRigidbody = this.GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        activeMovespeed = moveSpeed;
        ChangeColor();
    }

    void Update()
    {
        playerRigidbody.velocity = new Vector2(activeMovespeed, playerRigidbody.velocity.y);
        FallingSpeedHandler();
        RotationReset();
        DeathTimer();
        if(Input.GetKeyDown("space")){
            JumpOrDashHandler();
        }

        DoubleTapCooldownHandler();
        DashCooldownHandler();
        Debug.DrawLine(this.GetComponent<SpriteRenderer>().bounds.max, transform.position + new Vector3(0.5f, -0.6f, 0), Color.green);
        Debug.DrawLine(this.GetComponent<SpriteRenderer>().bounds.min, transform.position + new Vector3(-0.5f, -0.6f, 0), Color.green);
    }

    private bool GroundedCheck(){
        RaycastHit2D frontRay = Physics2D.Raycast(this.GetComponent<SpriteRenderer>().bounds.max, -Vector2.up, .6f);
        RaycastHit2D backRay = Physics2D.Raycast(this.GetComponent<SpriteRenderer>().bounds.min, -Vector2.up, .6f);

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
            playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, 0);
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
            playerRigidbody.gravityScale = 8;
        }
        else{
            playerRigidbody.gravityScale = 3;
        }
    }

    IEnumerator JumpWithDelay(){
        yield return new WaitForSeconds(jumpDelay);
        playerRigidbody.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        audioSource.PlayOneShot(jump);
        ChangeColor();
    }

    void Dash(){
        if(dashCoolCounter <= 0 && dashCounter <= 0 && canDash){
            activeMovespeed = dashSpeed;
            dashSprite.SetActive(false);
            dashCounter = dashLenght;
            audioSource.PlayOneShot(dash);
            ChangeColor();
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
