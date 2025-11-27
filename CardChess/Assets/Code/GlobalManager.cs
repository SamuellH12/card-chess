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
        moves.AddRange(cell.piece.ListOfAttacks(board)); // add atacks as well
        moves = new List<Cell>(new HashSet<Cell>(moves)); // unique

        foreach(Cell move in moves){
            highlightedCells.Add(move);
            move.HighlightCell();
        }
    }

    void ClearHighlights(){
        foreach(Cell cell in highlightedCells) cell.ClearHighlight();
        highlightedCells.Clear();
    }

    public void HandleEvolution(Piece piece){
        if(piece.evolutions.Count == 0) return;

        // for simplicity, just evolve to the first option
        GameObject evolutionPrefab = piece.evolutions[0];

        // create new piece
        GameObject newPieceObj = Instantiate(evolutionPrefab);
        newPieceObj.transform.parent = board.transform;

        Piece newPiece = newPieceObj.GetComponent<Piece>();
        newPiece.player = piece.player;
        newPiece.MoveToCell(piece.cell);

        // remove old piece
        board.pieces.Remove(piece);
        Destroy(piece.gameObject);

        board.pieces.Add(newPiece);

        Debug.Log("Piece evolved for player " + newPiece.player + " at cell " + newPiece.cell.x + "," + newPiece.cell.y);
    }

}
