using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePool : MonoBehaviour
{
    public GameObject projectilePrefab;
    public int poolSize = 10;
    private List<GameObject> projectilePool;

    void Start()
    {
        projectilePool = new List<GameObject>();

        // Inicializa o pool com projéteis desativados
        for (int i = 0; i < poolSize; i++)
        {
            GameObject projectile = Instantiate(projectilePrefab);
            projectile.SetActive(false);
            projectilePool.Add(projectile);
        }
    }

    // Método para pegar um projétil do pool
    public GameObject GetProjectile()
    {
        foreach (GameObject projectile in projectilePool)
        {
            if (!projectile.activeInHierarchy)
            {
                projectile.SetActive(true);
                return projectile;
            }
        }

        // Se nenhum projétil estiver disponível, expandir o pool
        GameObject newProjectile = Instantiate(projectilePrefab);
        newProjectile.SetActive(true);
        projectilePool.Add(newProjectile);
        return newProjectile;
    }

    // Método para retornar o projétil ao pool
    public void ReturnProjectile(GameObject projectile)
    {
        projectile.SetActive(false);
    }
}
