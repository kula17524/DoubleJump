using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumping : MonoBehaviour
{

    private Animator anim;
    private CharacterController con;
    private Vector3 velocity;
    private bool isDoubleJump = false;
    private bool tryDoubleJump;
    [SerializeField]private float jumpPower = 5f;
    [SerializeField]private float doubleJumpPower = 5.6f;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        con = GetComponent<CharacterController>();
        velocity = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if(con.isGrounded) {
            velocity = Vector3.zero;

            anim.SetBool("Jump", false);
            anim.SetBool("DoubleJump", false);
            isDoubleJump = false;
            tryDoubleJump = false;
            
            if(Input.GetButtonDown("Jump")){
            anim.SetBool("Jump", true);
            velocity.y += jumpPower;
            }
        } else if (anim.GetBool("Jump") && !isDoubleJump &&!tryDoubleJump) {
            if(Input.GetButtonDown("Jump")) {
                var normalizedTime = Mathf.Repeat(anim.GetCurrentAnimatorStateInfo(0).normalizedTime, 1f);

                if(anim.GetCurrentAnimatorStateInfo(0).IsName("Jump") && 0.01f <= normalizedTime && normalizedTime <= 0.5f) {
                    isDoubleJump = true;
                    anim.SetBool("DoubleJump", true);
                    velocity.y += doubleJumpPower;
                } else {
                    tryDoubleJump = true;
                }
            }
        }

        velocity.y += Physics.gravity.y * Time.deltaTime;
        con.Move(velocity * Time.deltaTime);
    }
}
