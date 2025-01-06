using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemClic : MonoBehaviour
{
    public Item itemObject;
    public Texture2D image;
    public static int vitesse;

    private void OnMouseDown()
    {
        inventaire.Ajouter(itemObject, image, gameObject);
        gameObject.SetActive(false);
    }
    private void Update(){
        transform.Rotate(new Vector3(0f, 1F, 0F));
    }
}
