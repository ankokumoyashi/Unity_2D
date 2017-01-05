using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevilController : MonoBehaviour {
    Rigidbody2D rb2d;
    public GameObject sprite;
    public float speed_x;
    public float speed_y;

    Vector2 target = Vector2.zero;
    Vector2 velocity = Vector2.zero;

    float angle;
    float direction;
    float mytime=0.0f;
    float nextfire=0.5f;
    float firedelta=0.5f;
    string target_character;
    bool isGrounded=false;

	// Use this for initialization
	void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        target_character = "Player_char";
	}
	
	// Update is called once per frame
	void Update () {
        //移動計算(ターゲットを追尾)
        target = GameObject.FindWithTag(target_character).transform.position;
        target.x = target.x - transform.position.x;
        target.y = target.y - transform.position.y;
        angle = Mathf.Atan2(target.y,target.x);
        velocity.x = speed_x * Mathf.Cos(angle);
        velocity.y = speed_y * Mathf.Sin(angle);
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
            Debug.Log("着弾");
            Destroy(this.gameObject);
            Destroy(other.gameObject);
        }
    }
}
