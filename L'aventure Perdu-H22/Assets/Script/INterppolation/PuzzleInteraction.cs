using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleInteraction : MonoBehaviour
{
    public GameObject Stele;    //la stele
    public GameObject[] puzzleCube = new GameObject[9]; // tableau des 9 positions initiales
    public GameObject[] puzzleCubeFinal = new GameObject[9];    //tablrau des 9 positions finales

    public float temps = 0; // temps d'interpoaltion
    public float distance = 0.2F;   //distance de deplacement;

    GameObject[,] puzzleCube3D = new GameObject[3,3];   //tableau 3d de puzzleCube
    GameObject[,] puzzleCubeFinal3D = new GameObject[3,3];//tableau 3d de puzzleCubeFinal

    Color32 initColor = new Color32(100, 100, 100, 255); //couleur initiales

    bool resolu = false;
    // Start is called before the first frame update
    void Awake()
    {
        for(int x = 0; x < 3; x++){
            for (int y = 0; y < 3; y++){
                puzzleCube3D[x,y] = puzzleCube[(x*3)+y];
                puzzleCubeFinal3D[x,y] = puzzleCubeFinal[(x*3)+y];
            }
        }
    }
    //si le puzzle n'est pas resolu, si un bloc est clique, check si on peut le deplacer et si oui le deplace
    // Update is called once per frame
    void Update()
    {
        if (!resolu){
            Vector2 positionBlock = new Vector2(0, 0);
            Vector2 nullPosition = new Vector2(0, 0);
            if( Input.GetMouseButtonDown(0) )
            {
                Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );
                RaycastHit hit;
                
                if( Physics.Raycast( ray, out hit, 10) )
                {
                    for(int x = 0; x < 3; x++){
                        for (int y = 0; y < 3; y++){
                            try{
                                if (hit.transform.gameObject.name == puzzleCube3D[x,y].name){
                                    positionBlock = new Vector2(x, y);
                                }
                            }
                            catch{
                                nullPosition = new Vector2(x, y);
                            }
                        }
                    }

                    Vector3 direction = Direction(positionBlock);
                    StartCoroutine(translate(puzzleCube3D[(int) positionBlock.x, (int) positionBlock.y] ,
                        direction, positionBlock, nullPosition));
                }
            }
        }
    }
    //change la position du bloc clique
    IEnumerator translate(GameObject block, Vector3 direction, Vector2 initPos, Vector2 finalPos){
        float t = 0;
        Vector3 posFinal = block.transform.localPosition + direction * distance;
        block.GetComponent<AudioSource>().Play();
        while (t <= temps){
            Vector3 ValInter = Vector3.Lerp(block.transform.localPosition, posFinal, t);
            block.transform.localPosition = ValInter;
            t += Time.deltaTime;
            yield return null;
        }
        block.transform.localPosition = posFinal;
        if (direction != Vector3.zero){
            puzzleCube3D[(int) finalPos.x, (int) finalPos.y] = block;
            puzzleCube3D[(int) initPos.x, (int) initPos.y] = null;

            resolu = true;
            for(int x = 0; x < 3; x++){
                for (int y = 0; y < 3; y++){
                    if (puzzleCube3D[x,y] != puzzleCubeFinal3D[x,y]){
                        resolu = false;
                    }
                }
            }
            if (resolu){
                StartCoroutine(Done(0.1F));
            }
        }
    }
    //qudn le puzzle est resolu, change la couleur des blocs et envoie un information a la stele
    IEnumerator Done(float temps){
        gameObject.GetComponent<AudioSource>().Play();
        for (int x = 0; x < 3; x++){
            for (int y = 0; y < 3; y++){
                float t = 0;
                if (puzzleCube3D[x,y] != null){
                    var Basepuzzle = puzzleCube3D[x,y].GetComponent<Renderer>();
                    while (t <= temps){
                        var ValInter = Color.Lerp(initColor, new Color32(0, 0, 0, 255), t);
                        Basepuzzle.material.color = ValInter;
                        t += Time.deltaTime;
                        yield return null;
                    }
                    Basepuzzle.material.color = new Color32(0, 0, 0, 255);
                }
            }
        }
        yield return new WaitForSecondsRealtime(3);
        Stele.GetComponent<SteleInteraction>().UpdateLight();
    }
    //recherche la direction possible du bloc clique
    Vector3 Direction(Vector2 position){

        if(position.x >= 1 && puzzleCube3D[(int) (position.x)-1,(int) position.y] == null){
            return Vector3.left;
        }
        if(position.x <= 1 && puzzleCube3D[(int) (position.x)+1,(int) position.y] == null){
            return Vector3.right;
        }

        if(position.y >= 1 && puzzleCube3D[(int) position.x,(int) (position.y)-1] == null){
            return Vector3.back;
        }
        if(position.y <= 1 &&puzzleCube3D[(int) position.x,(int) (position.y)+1] == null){
            return Vector3.forward;
        }

        return Vector3.zero;
    }
}
