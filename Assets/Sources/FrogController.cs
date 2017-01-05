using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogController : MonoBehaviour {
    Rigidbody2D rb2d;
    public GameObject sprite;
    public float speed_x;
    public float speed_y;
    public float gravity;
    public float shotSpeed;
    public GameObject fire_prefab;

    Vector2 velocity = Vector2.zero;

    float angle;
    float direction;
    float mytime=0.0f;
    float nextfire=2.5f;
    float firedelta=2.5f;
    bool isGrounded=false;
    int life = 2;

	// Use this for initialization
	void Start () {
        rb2d = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        mytime = mytime + Time.deltaTime;
        if(!isGrounded){
             velocity.y += -1.0f * Time.deltaTime * gravity; 
        }
        //移動計算(x移動一方通行)
        if(isGrounded&&mytime > nextfire)Jump();
        velocity.x = speed_x;
        if (velocity.x>=0.0f){
            direction = 0.0f;
        }else{
            direction = 180.0f;
        }
        transform.eulerAngles = new Vector3(0.0f,direction,0.0f);
        rb2d.velocity = velocity * Time.deltaTime;
		
	}
    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "fire"){
            life--;
            Debug.Log("着弾");
            if(life <= 0)Destroy(this.gameObject);
            Destroy(other.gameObject);
        }
    }
    void Jump(){
        Debug.Log("FrogJump!");
        velocity.y = speed_y;
        //rb2d.AddForce(Vector2.up * speed_jump);
        //isGrounded=false;
    }
    void OnTriggerStay2D(Collider2D other){
        if(other.gameObject.tag == "Ground"){
            Debug.Log("接地");
            velocity.y = 0.0f;
            isGrounded=true;
        }
    }
    void OnTriggerExit2D(Collider2D other){
        if(other.gameObject.tag == "Ground"){
            isGrounded=false;
        }
    }
}
