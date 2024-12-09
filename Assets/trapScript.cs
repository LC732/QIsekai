using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class trapScript : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap; // Referência ao Tilemap
    [SerializeField] private GameObject specificTag; // Tag específica do objeto que pode destruir tiles

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Verifica se o objeto que colidiu tem a tag específica
        if (collision.gameObject.CompareTag(specificTag.tag))
        {
            // Obtém o ponto de contato da colisão
            Vector2 hitPosition = Vector2.zero;
            foreach (ContactPoint2D hit in collision.contacts)
            {
                hitPosition = hit.point;
                break;
            }

            // Converte a posição do impacto para coordenadas de célula no tilemap
            Vector3Int cellPosition = tilemap.WorldToCell(hitPosition);

            // Remove o tile na posição da célula
            tilemap.SetTile(cellPosition, null);
        }
    }

}
