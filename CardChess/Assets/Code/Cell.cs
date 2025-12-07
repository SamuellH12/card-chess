using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour {
    /* 
    Represents a cell in the board.
    Holds position, references to board and piece, and handles clicks.
    Requires:
    - An Image component for background
    - An Image component for highlight (initially disabled)
    - A Collider2D component for click detection
    */

    public int x, y;
    public SpriteRenderer highlightImage;
    public Piece piece = null;
    
    [HideInInspector]
    public SpriteRenderer backgroundImage;
    [HideInInspector]
    public Board board;
    
    // if can evolve pawn here
    public bool canEvolveWhite = false;
    public bool canEvolveBlack = false;

    void Start(){
        backgroundImage = GetComponent<SpriteRenderer>();
        highlightImage.enabled = false;
    }

    void Update(){
        // if clicked, tell global manager

        if(Input.GetMouseButtonDown(0)){
        
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            // check if mouse is over this cell
            // cell has a box collider2D
            Collider2D collider = GetComponent<Collider2D>();
            
            if(collider == Physics2D.OverlapPoint(mousePos2D)){
                board.globalManager.ClickedCell(this);
            }
        }
    }

    public void HighlightCell(){
        highlightImage.enabled = true;
    }
    public void ClearHighlight(){
        highlightImage.enabled = false;
    }

    public bool IsEmpty(){ return piece == null; }
    public bool HasPiece(){ return piece != null; }
    public bool HasEnemyPiece(int player){ return piece != null && piece.player != player; }
    public bool EmptyOrEnemy(int player){ return piece == null || piece.player != player; }
}
