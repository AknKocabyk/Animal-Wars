using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
     public float health = 100f;  // Karakterin başlangıç sağlığı
     private Animator animator;


     private void Start()
     {
         animator = GetComponent<Animator>();
     }

     public void TakeDamage(float damage)
        {
            health -= damage;  // Sağlık miktarını azalt
            Debug.Log($"Karakterin canı: {health}");
    
            if (health <= 0f)
            {
                Die();  // Eğer can 0'ın altına düşerse, karakter ölür
            }
        }
    
        void Die()
        {
            Debug.Log("Karakter öldü");
            // Ölüm işlemi yapılabilir
            //Menü sahnesini yükle
            animator.SetBool("Death",true);
        }
}
