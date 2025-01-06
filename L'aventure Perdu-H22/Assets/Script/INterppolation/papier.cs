using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class papier : MonoBehaviour
{
    public GameObject page4;
    public Item page4Item;
    public Texture2D page4texture;
    private void OnMouseDown()
    {
        int page1 = inventaire.Present(Item.page1);
        int page2 = inventaire.Present(Item.page2);
        int page3 = inventaire.Present(Item.page3);
        if (page1 != -1 && page2 != -1 && page3 != -1){
            inventaire.Destroy(page1);
            inventaire.Destroy(page2);
            inventaire.Destroy(page3);
            inventaire.Ajouter(page4Item, page4texture, page4);
        }
    }
}
