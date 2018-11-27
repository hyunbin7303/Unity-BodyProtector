using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {


    public static AudioClip AttackSound;
    public static AudioClip EnemyHitSound;
    public static AudioClip basicSound;
    static AudioSource source;


	// Use this for initialization
	void Start () {

        AttackSound = Resources.Load<AudioClip>("Fire");
        source = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void SetUp()
    {

    }
    public static void PlaySound(string clip)
    {
        switch(clip)
        {
            case "Fire":
                source.PlayOneShot(AttackSound);
                break;

        }
    }
}
