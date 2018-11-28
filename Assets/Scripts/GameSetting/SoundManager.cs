using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public static SoundManager instance = null;
    public static AudioClip AttackSound;
    public static AudioClip EnemyHitSound;
    public static AudioClip basicSound;
    static AudioSource source;
    void Awake()
    {
        //Check if instance already exists
        if (instance == null)

            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
    }
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
    public void PlaySound(string clip)
    {

           source.PlayOneShot(AttackSound);

    }
}
