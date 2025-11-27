using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Board : MonoBehaviour {

    public int H = 8;
    public int W = 8;
    public GameObject cellPrefab;
    
    [HideInInspector]
    public Cell[,] cells;
    [HideInInspector]
    public GlobalManager globalManager;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start(){
        RecreateBoard();
    }

    // Update is called once per frame
    void Update(){ }

    void RecreateBoard(){
        cells = new Cell[H,W];

        // create new cells
        for(int i=0; i<H; i++){
            for(int j=0; j<W; j++){
                if(cells[i,j] == null){
                    GameObject cellObj = Instantiate(cellPrefab);
                    cellObj.transform.parent = this.transform;
                    cells[i,j] = cellObj.GetComponent<Cell>();
                    
                    if((i + j) % 2 == 0){
                        cells[i,j].backgroundImage.color = Color.white;
                    } else {
                        cells[i,j].backgroundImage.color = Color.gray;
                    }
                    
                    cells[i,j].x = i;
                    cells[i,j].y = j;
                }
                cells[i,j].board = this;
                cells[i,j].transform.position = new Vector3(i, j, 0);
            }
        }
    }
}
