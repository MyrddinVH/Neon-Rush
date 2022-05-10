using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float jumpForce;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float dashForce;

    private Rigidbody2D playerRigidbody;
    private float dashCooldown = 0.2f;
    private int taps = 0;

    private bool tapping;
    private float lastTap;

    void Start()
    {
        playerRigidbody = this.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(Input.GetKeyDown("space")){

            // **** version with jump delay ****//

            // if(!tapping){
            //     tapping = true;
            //     StartCoroutine(SingleTap());
            // }
            // if((Time.time - lastTap) < dashCooldown){
            //     Dash();
            //     tapping = false;
            // }
            // lastTap = Time.time;

            //**** version without delay ****//

            if(dashCooldown > 0 && taps == 1){
                Dash();
            }
            else{
                dashCooldown = 0.2f;
                taps += 1;
            }

            if(GroundedCheck()){
                Jump();
            }

        }
            if(dashCooldown > 0){
                dashCooldown -= 1 * Time.deltaTime;
            }
            else{
                taps = 0;
            }
    }

    private bool GroundedCheck(){
        RaycastHit2D frontRay = Physics2D.Raycast(this.GetComponent<SpriteRenderer>().bounds.max, -Vector2.up, 1f);
        RaycastHit2D backRay = Physics2D.Raycast(this.GetComponent<SpriteRenderer>().bounds.min, -Vector2.up, 1f);

        Debug.DrawLine(this.GetComponent<SpriteRenderer>().bounds.max, transform.position + new Vector3(0.5f, -1, 0), Color.green);
        Debug.DrawLine(this.GetComponent<SpriteRenderer>().bounds.min, transform.position + new Vector3(-0.5f, -1, 0), Color.green);

        if(!frontRay.collider && !backRay.collider){
            return false;
        }
        else{
            return true;
        }
    }

    // IEnumerator SingleTap(){
    //     yield return new WaitForSeconds(dashCooldown);
    //     if(tapping){
    //         if(GroundedCheck()){
    //             Jump();
    //         }
    //         tapping = false;
    //     }
    // }

    void Jump(){
        playerRigidbody.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }

    void Dash(){
        playerRigidbody.AddForce(transform.right * dashForce, ForceMode2D.Impulse);
    }
}
