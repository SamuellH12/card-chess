using UnityEngine;
using System.Collections.Generic;

public class Piece : MonoBehaviour
{
    public Cell cell = null;
    public int player = 0;
    public string pieceType = "Generic";
    public SpriteRenderer spriteRenderer;
    public List<GameObject> evolutions = new List<GameObject>();
    public bool hasMoved = false;

    void Start(){
        // change color based on player
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = player == 0 ? Color.white : Color.black;
    }

    void Update(){
        
    }

    // move piece to new cell
    // (implement variations in subclasses if needed)
    public virtual void MoveToCell(Cell newCell){
        if(cell) cell.piece = null;
        hasMoved = true;

        if(newCell.piece != null) CapturePiece(newCell.piece);

        cell = newCell;
        cell.piece = this;

        transform.position = new Vector3(newCell.x, newCell.y, 0);

        // call manager to handle evolution
        if(CanEvolve()) cell.board.globalManager.HandleEvolution(this);
    }

    public void CapturePiece(Piece targetPiece){
        Board board = targetPiece.cell.board;
        board.pieces.Remove(targetPiece);
        Destroy(targetPiece.gameObject);
    }

    // search for possible moves in the board
    // (to be implemented in subclasses)
    public virtual List<Cell> ListOfMoves(Board board){
        List<Cell> moves = new List<Cell>();

        // default: move for one step in any direction
        int x = cell.x, y = cell.y;
        for(int dx=-1; dx<=1; dx++){
            for(int dy=-1; dy<=1; dy++){
                if(dx == 0 && dy == 0) continue;

                int nx = x + dx;
                int ny = y + dy;

                if(board.IsInsideBoard(nx, ny) && (board.cells[nx, ny].piece == null || board.cells[nx, ny].piece.player != player)){
                    moves.Add(board.cells[nx, ny]);
                }
            }
        }

        return moves;
    }

    // list only the cells that can be attacked by this piece
    public virtual List<Cell> ListOfAttacks(Board board, bool couldAtack = false){
        return ListOfMoves(board);
    }

    public virtual bool CanEvolve(){
        return false;
    }
}
