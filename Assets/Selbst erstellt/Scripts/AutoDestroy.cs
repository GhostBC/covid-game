using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{

    public int TimeToLive = 5;
    // Start is called before the first frame update
    void Start()
    {
        // Kills the game object in 5 seconds after loading the object
        Destroy(gameObject, TimeToLive);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
