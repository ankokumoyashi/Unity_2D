using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    Rigidbody2D rb2d;
    public GameObject sprite;
    public float speed_x;
    public float speed_jump;
    public float speed_StairsCase;
    public float gravity;
    public float shotSpeed;
    public LifePanel lifePanel;
    public GameObject fire_prefab;

    Vector2 velocity = Vector2.zero;
    Animator animator;

    float direction;
    float mytime=0.0f;
    float nextfire=0.5f;
    float firedelta=0.5f;
    bool isGrounded=false;
    bool isStairsCase=false;
    int life = 6;

	// Use this for initialization
	void Start () {
        //player = GetComponent<CharacterController>();
        rb2d = GetComponent<Rigidbody2D>();
        animator = sprite.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        mytime = mytime + Time.deltaTime;
        if(!isGrounded){
             velocity.y += -1.0f * Time.deltaTime * gravity; 
        }

        if(Input.GetAxis("Horizontal")>0.0f)MoveToRight();
        if(Input.GetAxis("Horizontal")<0.0f)MoveToLeft();
        if(Input.GetAxis("Vertical")>0.0f)Up();
        if(Input.GetKeyDown("space")&&isGrounded)Jump();
        if(Input.GetKeyDown("f")&&mytime > nextfire)Shot();

        //transform.position = new Vector2(transform.position.x+moveDirection.x*Time.deltaTime,transform.position.y+moveDirection.y * Time.deltaTime);
        transform.eulerAngles = new Vector3(0.0f,direction,0.0f);

        if (velocity.x >= 1.0f || velocity.x <= -1.0f) {
            animator.SetBool("isRun",true);
        } else {
            animator.SetBool("isRun",false);
        }
        rb2d.velocity = velocity;
        velocity.x = 0;
        //Vector3 globalDirection = transform.TransformDirection(moveDirection);
        // = Mathf.Lerp(angle, targetAngle, Time.deltaTime * 10.0f);

        //player.Move(globalDirection * Time.deltaTime);
	}
    void Shot(){
        nextfire = mytime+firedelta;
        GameObject fire = (GameObject)Instantiate(
                fire_prefab,
                transform.position + new Vector3(Mathf.Cos(direction*Mathf.Deg2Rad)*1.5f,0.3f,0.0f),
                Quaternion.identity
                );
        fire.transform.eulerAngles = new Vector3(0.0f,direction,0.0f);
        Rigidbody2D fireRigitBody = fire.GetComponent<Rigidbody2D>();
        fireRigitBody.AddForce(new Vector2(Mathf.Cos(direction*Mathf.Deg2Rad)*shotSpeed,0.0f));
        animator.SetTrigger("Shot");
    }
    void Up(){
        if(isStairsCase){
            Debug.Log("up");
            velocity.y = speed_StairsCase*Time.deltaTime;
        }
    }
    void MoveToLeft(){
        //moveDirection.x = Input.GetAxis("Horizontal") * speed_x;
        velocity.x = Input.GetAxis("Horizontal")*speed_x*Time.deltaTime;
        direction = 180.0f;
    }
    void MoveToRight(){
        //moveDirection.x = Input.GetAxis("Horizontal") * speed_x;
        velocity.x = Input.GetAxis("Horizontal")*speed_x*Time.deltaTime;
        direction = 0.0f;
    }
    void Jump(){
        Debug.Log("aa");
        velocity.y = speed_jump;
        Debug.Log(velocity.y);
        //rb2d.AddForce(Vector2.up * speed_jump);
        //isGrounded=false;
    }
    void OnTriggerStay2D(Collider2D other){
        if(other.gameObject.tag == "Ground"){
            Debug.Log("接地");
            velocity.y = 0.0f;
            isGrounded=true;
            animator.SetBool("isGrounded",isGrounded);
        }
        if(other.gameObject.tag == "StairsCase"){
            isStairsCase = true;
        }
    }
    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Enemy"||other.gameObject.tag == "Enemy_fire"){
            //velocity.x = 10000.0f * Time.deltaTime * -1.0f * (float)Math.Cos(angle * (Math.PI / 180.0f));
            rb2d.AddForce(new Vector3(-5000.0f,5000.0f,0.0f));
            //velocity.x = -1000.0f * Time.deltaTime; 
            //velocity.y = 1000.0f * Time.deltaTime;
            life--;
            lifePanel.UpdateLifePanel(life);
            if(other.gameObject.tag == "Enemy_fire")Destroy(other.gameObject);
            Debug.Log(life);
        }
    }
    void OnTriggerExit2D(Collider2D other){
        if(other.gameObject.tag == "Ground"){
            isGrounded=false;
            animator.SetBool("isGrounded",isGrounded);
        }
        if(other.gameObject.tag == "StairsCase"){
            isStairsCase=false;
        }
    }
}
