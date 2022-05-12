using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    [Header("movement speed")]
    [SerializeField] private float moveSpeed;

    private Rigidbody2D playerRigidbody;
    private float timeBetweenTaps = 0.2f;
    private int taps = 0;
    private float activeMovespeed;
    private float dashCounter;
    private float dashCoolCounter;



    void Start()
    {
        playerRigidbody = this.GetComponent<Rigidbody2D>();
        activeMovespeed = moveSpeed;
    }

    void Update()
    {
        playerRigidbody.velocity = new Vector2(activeMovespeed, playerRigidbody.velocity.y);
        FallingSpeedHandler();
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
            return true;
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

        if(dashCounter <= 0){
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
            }
        }

        if(dashCoolCounter > 0){
            dashCoolCounter -= Time.deltaTime;
        }
    }

    private void FallingSpeedHandler(){
        if(playerRigidbody.velocity.y < 0){
            playerRigidbody.gravityScale = 6;
        }
        else{
            playerRigidbody.gravityScale = 3;
        }
    }

    IEnumerator JumpWithDelay(){
        yield return new WaitForSeconds(jumpDelay);
        if(GroundedCheck()){
        playerRigidbody.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    void Dash(){
        if(dashCoolCounter <= 0 && dashCounter <= 0){
            activeMovespeed = dashSpeed;
            dashCounter = dashLenght;
        }
    }
}
