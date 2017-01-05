using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonController : MonoBehaviour {
    Rigidbody2D rb2d;
    public GameObject sprite;
    public float speed_x;
    public float gravity;
    public float shotSpeed;
    public GameObject fire_prefab;

    Vector2 velocity = Vector2.zero;

    string MAIN_CAMERA_TAG_NAME = "Main Camera";
    float angle;
    float direction;
    float mytime=0.0f;
    float nextfire=5.0f;
    float firedelta=5.0f;
    bool isGrounded=false;
    bool isRendered;
    int life = 3;

	// Use this for initialization
	void Start () {
        //player = GetComponent<CharacterController>();
        rb2d = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        if(isRendered){
            mytime = mytime + Time.deltaTime;
            //移動計算(x移動一方通行)
            velocity.x = speed_x*Time.deltaTime;
            if (velocity.x>=0.0f){
                direction = 0.0f;
            }else{
                direction = 180.0f;
            }
            if(mytime > nextfire)Shot();
            transform.eulerAngles = new Vector3(0.0f,direction,0.0f);
            rb2d.velocity = velocity;
        }
		
	}
    void Shot(){
        nextfire = mytime+firedelta;
        GameObject fire = (GameObject)Instantiate(
                fire_prefab,
                transform.position + new Vector3(Mathf.Cos(direction*Mathf.Deg2Rad)*3.5f,0.0f,0.0f),
                Quaternion.identity
                );
        fire.transform.eulerAngles = new Vector3(0.0f,direction,0.0f);
        Rigidbody2D fireRigitBody = fire.GetComponent<Rigidbody2D>();
        fireRigitBody.AddForce(new Vector2(Mathf.Cos(direction*Mathf.Deg2Rad)*shotSpeed,0.0f));
    }
    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "fire"){
            life--;
            Debug.Log("着弾");
            if(life <= 0)Destroy(this.gameObject);
            Destroy(other.gameObject);
        }
    }
    void OnWillRenderObject(){
        Debug.Log(Camera.name);
        //if(Camera.current.tag == MAIN_CAMERA_TAG_NAME){
            isRendered = true; 
        //}
    }
}
