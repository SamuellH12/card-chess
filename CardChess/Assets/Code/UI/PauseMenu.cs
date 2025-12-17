using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GlobalManager globalManager;
    public GameObject atual = null;
    public void SwapScreen( GameObject next){
        if(atual) atual.SetActive(false);
        next.SetActive(true);
        atual = next;
    }
}
