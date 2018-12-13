using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public static SoundManager instance = null;
    public static AudioClip FireSound;
    public static AudioClip EnemyHitSound;
    public static AudioClip basicSound;

    public static AudioClip WaterSound;
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
        //DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    void Start()
    {

        FireSound = Resources.Load<AudioClip>("Fire");
        WaterSound = Resources.Load<AudioClip>("Water");
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetUp()
    {

    }

    public void PlaySound(string clip)
    {

        source.PlayOneShot(FireSound);
        source.PlayOneShot(WaterSound);       
            
    }
}
