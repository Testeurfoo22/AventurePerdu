using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemInv : MonoBehaviour
{
    public int valueItem;
    public void clique()
    {
        inventaire.Retirer(valueItem);
    }
}
