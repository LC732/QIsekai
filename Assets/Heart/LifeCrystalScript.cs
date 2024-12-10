
using UnityEngine;

public class LifeCrystalScript : MonoBehaviour
{

[SerializeField] GameObject targetObject; // Objeto cuja tag será comparada
[SerializeField] GameObject player;
[SerializeField] PlayerScript pS;
[SerializeField] GameObject IceHeart;
[SerializeField] GameObject Heart;
public bool iscrystal = false;

void Start(){
    IceHeart.SetActive(true);
    Heart.SetActive(false);
}

void OnCollisionEnter2D(Collision2D collision)
{
    // Verifica se o targetObject foi atribuído
    if (targetObject != null && player != null)
    {
        // Compara a tag da colisão com a tag do targetObject
        if (collision.gameObject.tag == targetObject.tag && !iscrystal)
        {
            changeToCrystal();
        }
        if (collision.gameObject.tag == player.tag && iscrystal){
            Destroy(gameObject);
        }
    }
    else
    {
        Debug.LogWarning("TargetObject não foi atribuído no inspetor!");
    }
}

    private void changeToCrystal()
    {
        iscrystal = true;
        IceHeart.SetActive(false);
        Heart.SetActive(true);
    }
}


