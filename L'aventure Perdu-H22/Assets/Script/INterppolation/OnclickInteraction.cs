using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnclickInteraction : MonoBehaviour
{
    public float ValueInit;
    public float ValueFin;
    public float temps;
    public GameObject TargetInteraction;

    [SerializeField] TypesInteraction Interaction;
    public AnimationCurve courbe = AnimationCurve.Linear(0F, 0F, 1F, 1F);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if( Input.GetMouseButtonDown(0) )
        {
            Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );
            RaycastHit hit;
            
            if( Physics.Raycast( ray, out hit, 100 ) )
            {
                Debug.Log( hit.transform.gameObject.name );
            }
        }
    }

    enum TypesInteraction{
        Translate,
        Rotate,
        Scale,
        Color
    }
}
