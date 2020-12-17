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


            Vector3 end = new Vector3(transform.position.x, 1, transform.position.z);

            Instantiate(myPrefab, end, Quaternion.identity);


        }
    }


    // Update is called once per frame
    void Update()
    {

    }
}
