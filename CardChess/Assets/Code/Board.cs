using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Board : MonoBehaviour{

    public int H = 8;
    public int W = 8;

    // list of cells in the board
    public Cell[,] cells;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start(){
        recreateBoard();
    }

    // Update is called once per frame
    void Update(){
        
    }

    void recreateBoard(){
        oldCells = cells;
        cells = new Cell[H,W];

        // save old cells to new cells to preserve pieces
        for(int i=0; i<Mathf.Min(oldCells.GetLength(0), H); i++){
            for(int j=0; j<Mathf.Min(oldCells.GetLength(1), W); j++){
                cells[i,j] = oldCells[i,j];
            }
        }

        // create new cells
        for(int i=0; i<H; i++){
            for(int j=0; j<W; j++){
                if(cells[i,j] == null){
                    GameObject cellObject = new GameObject("Cell " + i + "," + j);
                    Cell cell = cellObject.AddComponent<Cell>();
                    cell.x = i;
                    cell.y = j;
                    cells[i,j] = cell;
                }
            }
        }
    }
}
