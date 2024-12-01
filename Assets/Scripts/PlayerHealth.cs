using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;  // Karakterin maksimum canı
    public float currentHealth;    // Mevcut can
    public Slider healthBar;       // Can barı (UI Slider)
    public GameObject deadPanel;
    
    private Animator animator;

    void Start()
    {
        currentHealth = maxHealth;  // Oyunun başında tam can
        healthBar.maxValue = maxHealth;  // Can barının maksimum değeri
        healthBar.value = currentHealth;  // Başlangıçtaki değer
        
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount; // Alınan hasar kadar can azalt
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Canı 0 ile max arasında tut
        healthBar.value = currentHealth; // Can barını güncelle

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        currentHealth += amount; // Can artır
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Canı sınırla
        healthBar.value = currentHealth; // Can barını güncelle
    }
    
        void Die()
        {
            Debug.Log("Karakter öldü");
            // Ölüm işlemi yapılabilir
            //Menü sahnesini yükle
            animator.SetBool("Death",true);
            Invoke("DeadPanelActive",1.2f);
        }

        public void DeadPanelActive()
        {
            deadPanel.SetActive(true);
        }
}
