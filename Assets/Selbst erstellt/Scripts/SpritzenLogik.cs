using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpritzenLogik : MonoBehaviour
{
    InventorySystem inv;
    // Start is called before the first frame update
    void Start()
    {
        inv = transform.parent.gameObject.GetComponent<InventorySystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
            GetComponent<MeshRenderer>().enabled = inv.hatSpritze;
       
    }
}
