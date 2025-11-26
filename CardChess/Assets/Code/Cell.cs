using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour {

    public int x, y;
    public Board board;
    public Image backgroundImage;
    public Image highlightImage; // highlight image for possible moves

    public Piece piece = null;

    void Start(){ }
    void Update(){ }

    void fixedUpdate(){
        
       // if clicked, select this cell
       if (Input.GetMouseButtonDown(0)){
           // get mouse position in world coordinates
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
            // check if mouse is over this cell
            Collider2D collider = GetComponent<Collider2D>();
            
            if(collider == Physics2D.OverlapPoint(mousePos2D)){
                Debug.Log("Cell " + x + "," + y + " clicked.");
                // select this cell
                
                // list possible moves for the piece in this cell
                if(piece != null){

                    
                }

            }
       }

    }
}
