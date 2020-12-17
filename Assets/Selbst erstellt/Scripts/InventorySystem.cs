using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
public class InventorySystem : MonoBehaviour
{

    public int ImpfstoffAnzahl;
    public Text ImpfstoffText;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
            ImpfstoffText.text = ImpfstoffAnzahl.ToString();
       

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Impfstoff")
            Destroy(other.gameObject);
        this.ImpfstoffAnzahl++;
    }
}
