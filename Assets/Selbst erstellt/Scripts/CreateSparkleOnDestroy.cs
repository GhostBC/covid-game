using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateSparkleOnDestroy : MonoBehaviour
{
    public GameObject myPrefab;
    private bool isQuitting = false;




    void OnApplicationQuit()
    {
        isQuitting = true;
    }


    void OnDestroy()
    {
        if (!isQuitting)
        {


            GameObject player = GameObject.Find("Bip001");
           

            Vector3 end = new Vector3(transform.position.x , transform.position.y + 1.1f, transform.position.z );


            Instantiate(myPrefab, new Vector3(player.transform.position.x, player.transform.position.y + 0.5f, player.transform.position.z), Quaternion.identity);


        }
    }


    // Update is called once per frame
    void Update()
    {

    }
}
