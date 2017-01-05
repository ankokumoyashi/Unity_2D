using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BatController : MonoBehaviour {
    Rigidbody2D rb2d;
    public GameObject sprite;
    public float speed_x;
    public float gravity;
    public GameObject fire_prefab;

    Vector2 velocity = Vector2.zero;

    float angle;
    float direction;
    float mytime=0.0f;
    float nextfire=0.5f;
    float firedelta=0.5f;
    bool isGrounded=false;

	// Use this for initialization
	void Start () {
        rb2d = GetComponent<Rigidbody2D>();
	}
	// Update is called once per frame
	void Update () {
        //移動計算(x移動一方通行)
        velocity.x = speed_x*Time.deltaTime;
        if (velocity.x>=0.0f){
            direction = 0.0f;
        }else{
            direction = 180.0f;
        }
        transform.eulerAngles = new Vector3(0.0f,direction,0.0f);
        rb2d.velocity = velocity;
	}
    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "fire"){
            Debug.Log("着弾");
            Destroy(this.gameObject);
            Destroy(other.gameObject);
        }
    }
}
