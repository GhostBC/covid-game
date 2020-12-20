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
 this.ImpfstoffText.text = "00" + this.ImpfstoffAnzahl.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        

if(this.ImpfstoffAnzahl.ToString() != this.ImpfstoffText.text) {

    if(this.ImpfstoffAnzahl >0) {
   

    if(this.ImpfstoffAnzahl < 10) {
          this.ImpfstoffText.text = "00" + this.ImpfstoffAnzahl.ToString();
          return;
    }
     if(this.ImpfstoffAnzahl < 100) {
          this.ImpfstoffText.text = "0" + this.ImpfstoffAnzahl.ToString();
          return;
    } else {
         this.ImpfstoffText.text =  this.ImpfstoffAnzahl.ToString();
          return;
    }

         
    }


}

      
          
       

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Impfstoff")
            Destroy(other.gameObject);
        this.ImpfstoffAnzahl++;
    }
}
