using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public enum Item {cle, page1, page2, page3, pageFinal, Sword, NULL};
public class inventaire : MonoBehaviour
{
    static bool[] collection = {false, false, false, false, false};
    static GameObject[] images = new GameObject[5];
    static GameObject[] itemList = new GameObject[5];
    // Start is called before the first frame update
    void Awake()
    {
        for (int i = 0; i < images.Length; i++){
            string nom = "item" + (i + 1);
            GameObject go = GameObject.Find(nom);
            if (go == null){
                Debug.Log("Error: Pas d'objet nomme " + nom);
            }
            images[i] = go;
            itemList[i] = null;
            go.SetActive(false);
            gameObject.GetComponent<Canvas>().enabled = false;
        }
    }

    public static bool IsEmpty(){
        if (System.Array.IndexOf(collection, true) == -1){
            return true;
        }
        return false;
    }

    public static int Present(Item item){
        for (int i = 0; i < itemList.Length; i++){
            if (itemList[i] != null && itemList[i].name.Equals(item.ToString())){
                return i;
            }
        }
        return -1;
    }
    public static void Ajouter(Item item, Texture2D image, GameObject itemObject){
        int indiceItem = System.Array.IndexOf(collection, false);
        collection[indiceItem] = true;
        images[indiceItem].GetComponent<RawImage>().texture = image;
        images[indiceItem].SetActive(true);
        itemList[indiceItem] = itemObject;
    }
    public static void Retirer(int valueItem){
        collection[valueItem] = false;
        images[valueItem].SetActive(false);
        itemList[valueItem].transform.position = GameObject.Find("Main Camera").transform.position - new Vector3(0F, 0.25F, 0F);
        itemList[valueItem].SetActive(true);
        itemList[valueItem] = null;
    }
    public static void Destroy(int valueItem){
        collection[valueItem] = false;
        images[valueItem].SetActive(false);
        Destroy(itemList[valueItem]);
        itemList[valueItem] = null;
    }
    public static GameObject Steal(){
        List<int> collectionOn = new List<int>();
        for(int i = 0; i < itemList.Length; i++){
            if(collection[i]){
                collectionOn.Add(i);
            }
        }
        int IDInv = Random.Range(0, collectionOn.Count);
        collection[collectionOn[IDInv]] = false;
        images[collectionOn[IDInv]].SetActive(false);
        var GameObjectEphemere = itemList[collectionOn[IDInv]];
        itemList[collectionOn[IDInv]] = null;
        return GameObjectEphemere;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("i")){
            gameObject.GetComponent<Canvas>().enabled = ! gameObject.GetComponent<Canvas>().enabled;
        }
    }
}
