using UnityEngine;
using UnityEngine.UI;

public class EvoButton : MonoBehaviour
{
    private GlobalManager gm;
    private Piece originalPiece;
    private GameObject evolutionPrefab;
    public Image image;

    public void Init(GlobalManager gm, Piece piece, GameObject prefab)
    {
        this.gm = gm;
        originalPiece = piece;
        evolutionPrefab = prefab;

        // Optional: use sprite from prefab
        Piece evoPiece = prefab.GetComponent<Piece>();
        image.sprite = prefab.GetComponent<SpriteRenderer>().sprite;
    }

    public void OnClick()
    {
        gm.ConfirmEvolution(originalPiece, evolutionPrefab);
    }
}
