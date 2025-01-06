using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Excalibur : MonoBehaviour
{
    public Item Sword;
    public GameObject swordObject;
    // Start is called before the first frame update
    private void OnMouseDown()
    {
        Debug.Log(inventaire.Present(Sword));
        int itemValue = inventaire.Present(Sword);
        if (itemValue != -1){
            swordObject.SetActive(true);
            inventaire.Destroy(itemValue);
        }
    }
}
