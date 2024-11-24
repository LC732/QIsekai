using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BossScript : MonoBehaviour
{

    public GameObject chao;
    public float shootTimer;
    public ProjectilePool projectilePool;

    public List<float> pontos = new List<float> { 0.5f, 0f, 0.8f };

    int Vida = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        fireBall();
    }

    void fireBall(){
        shootTimer -= Time.deltaTime;

        if(shootTimer < 0f){
        shootTimer = 1;
        Shoot();
    }
    }

    Vector3 GetRandomPoint()
    {
        int index = Random.Range(0, pontos.Count);  // Gera um índice aleatório entre 0 e o número de elementos - 1
        return transform.position - new Vector3(0f, pontos[index], 0f);
    }

    void Shoot()
    {
        GameObject projectile = projectilePool.GetProjectile();
        projectile.transform.position = GetRandomPoint();
        projectile.transform.rotation = transform.rotation;

        // Obter o componente Projectile e configurar o pool de retorno
        FireScript projectileComponent = projectile.GetComponent<FireScript>();
        projectileComponent.SetPool(projectilePool);

        // Aplicar a força de movimento ou configurar a direção do projétil
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(-10f,0f); // Exemplo de velocidade
    }


}
