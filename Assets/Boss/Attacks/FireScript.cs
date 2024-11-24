using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireScript : MonoBehaviour
{
    private ProjectilePool pool;

    // Método para definir o pool que gerencia este projétil
    public void SetPool(ProjectilePool pool)
    {
        this.pool = pool;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Retorna o projétil ao pool
            pool.ReturnProjectile(this.gameObject);
        }
    }
}

