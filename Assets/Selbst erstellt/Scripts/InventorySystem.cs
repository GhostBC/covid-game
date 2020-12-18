using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
public class InventorySystem : MonoBehaviour
{

    public int ImpfstoffAnzahl = 0;
    public Text ImpfstoffText;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        

if(this.ImpfstoffAnzahl.ToString() != this.ImpfstoffText.text) {
  this.ImpfstoffText.text = this.ImpfstoffAnzahl.ToString();
}

      
          
       

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Impfstoff")
            Destroy(other.gameObject);
        this.ImpfstoffAnzahl++;
    }
}
