using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeController : MonoBehaviour {
    Rigidbody2D rb2d;
    public GameObject sprite;
    public float speed_x;
    public float speed_y;
    public float shotSpeed;
    public GameObject fire_prefab;

    Vector2 target = Vector2.zero;
    Vector2 velocity = Vector2.zero;

    float angle;
    float direction;
    float mytime=0.0f;
    float nextfire=1.5f;
    float firedelta=1.5f;
    string target_character;
    bool isGrounded=false;

	// Use this for initialization
	void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        target_character = "Player_char";
	}
	
	// Update is called once per frame
	void Update () {
        mytime = mytime + Time.deltaTime;
        //移動計算(ターゲットを追尾)
        target = GameObject.FindWithTag(target_character).transform.position;
        target.x = target.x - transform.position.x;
        target.y = target.y - transform.position.y;
        angle = Mathf.Atan2(target.y,target.x);
        velocity.x = speed_x * Mathf.Cos(angle);
        velocity.y = speed_y * Mathf.Sin(angle);
        direction = Mathf.Atan2(velocity.y,velocity.x)*Mathf.Rad2Deg;
        if(mytime > nextfire)Shot();
        transform.rotation = Quaternion.Euler(0,0,direction);
        rb2d.velocity = velocity * Time.deltaTime;
	}
    void Shot(){
        nextfire = mytime+firedelta;
        GameObject fire = (GameObject)Instantiate(
                fire_prefab,
                transform.position + new Vector3(Mathf.Cos(direction*Mathf.Deg2Rad)*1.5f,0.0f,0.0f),
                Quaternion.identity
                );
        fire.transform.rotation = Quaternion.Euler(0,0,direction);
        Rigidbody2D fireRigitBody = fire.GetComponent<Rigidbody2D>();
        fireRigitBody.AddForce(new Vector2(Mathf.Cos(direction*Mathf.Deg2Rad)*shotSpeed,Mathf.Sin(direction*Mathf.Deg2Rad)*shotSpeed));
    }
    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "fire"){
            Debug.Log("着弾");
            Destroy(this.gameObject);
            Destroy(other.gameObject);
        }
    }
}
