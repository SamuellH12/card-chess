using UnityEngine;
using System.Collections.Generic;

public class GlobalManager : MonoBehaviour{
    
    public Board board = null;
    int turn = 0;

    private Cell selectedCell = null;
    private List<Cell> highlightedCells = new List<Cell>();

    void Start(){
        board.globalManager = this;
        turn = 0;
    }

    void Update(){ }

    void fixedUpdate(){ }

    public void ClickedCell(Cell cell){
        
        // select this cell
        if(selectedCell == null){
            selectedCell = cell;
            AddHighlight(cell);
            return;
        }

        // deselect
        if(selectedCell == cell){
            ClearHighlights();
            selectedCell = null;
            return;
        }

        // if clicked cell is highlighted, move piece
        if(highlightedCells.Contains(cell)){
            Piece piece = selectedCell.piece;

            if(piece != null){
                piece.MoveToCell(cell);
                Debug.Log("Moving piece from " + selectedCell.x + "," + selectedCell.y + " to " + cell.x + "," + cell.y);
            }
            else {
                Debug.Log("No piece in selected cell " + selectedCell.x + "," + selectedCell.y);
            } 

            ClearHighlights();
            selectedCell = null;
            turn ^= 1; // switch turn
            
            return;
        }
        
        // clicked cell is not highlighted, clear selection
        ClearHighlights();
        selectedCell = null;
    }

    void AddHighlight(Cell cell){
        Debug.Log("Highlighting cell " + cell.x + "," + cell.y);
        ClearHighlights();
        
        highlightedCells.Add(cell);
        cell.HighlightCell();

        if(cell.piece == null || cell.piece.player != turn ) return;

        List<Cell> moves = cell.piece.ListOfMoves(board);

        foreach(Cell move in moves){
            highlightedCells.Add(move);
            move.HighlightCell();
        }
    }

    void ClearHighlights(){
        foreach(Cell cell in highlightedCells) cell.ClearHighlight();
        highlightedCells.Clear();
    }

}
