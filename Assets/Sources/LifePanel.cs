using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifePanel : MonoBehaviour {
    public GameObject[] life_sprites;
	// Use this for initialization
	void Start () {
        life_sprites[4].SetActive(false);
        life_sprites[3].SetActive(false);
        life_sprites[2].SetActive(false);
        life_sprites[1].SetActive(false);
        life_sprites[0].SetActive(false);
	}
	// Update is called once per frame
	public void UpdateLifePanel (int life) {
        if(life==0)return;
        life_sprites[life].SetActive(false);
        life_sprites[life-1].SetActive(true);
	}
}
