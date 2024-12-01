using UnityEngine;

public class Animal : MonoBehaviour
{
    public AnimalGroup group; // Hayvanın ait olduğu grup
    public float attackRange = 2f; // Hayvanın karaktere saldırabileceği mesafe
    public float moveSpeed = 3f; // Hayvanın normal hareket hızı
    public float chaseSpeed = 6f; // Hayvanın koşarak hareket edeceği hız
    public float attackDamage = 10f; // Hayvanın vereceği hasar
    public float rotationSpeed = 5f; // Hayvanın döneceği hız

    private Transform player; // Oyuncu karakterinin transformu
    private Animator animator; // Hayvanın animasyonlarını kontrol etmek için Animator

    private bool isChasing = false; // Hayvanın koşup koşmadığını kontrol eder
    private bool isAttacking = false; // Hayvanın saldırıp saldırmadığını kontrol eder
    
    public float runDistance;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // Oyuncuyu buluyoruz
        animator = GetComponent<Animator>(); // Animator'ı alıyoruz
    }

    void Update()
    {
        if (group != null)
        {
            HandleAnimalBehavior(); // Hayvanın davranışını kontrol et
        }
    }

    void HandleAnimalBehavior()
    {
        if (group.relationshipPoints >= 85)
        {
            // İlişki puanı 80 veya üzerindeyse hayvan Idle durumuna geçer
            if (isChasing || animator.GetBool("Running"))
            {
                isChasing = false;
                animator.SetBool("Running", false); // Koşma animasyonunu durdur
                animator.SetBool("Attack",false);
            }

            animator.SetBool("Attack",false);
            animator.SetBool("Running",false);
            return; // Daha fazla işlem yapma
        }

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer < attackRange)
        {
            // Saldırmak için mesafeye girdiyse
            if (!isAttacking)
            {
                isAttacking = true;
                AttackPlayer();
            }
        }
        else if (distanceToPlayer < runDistance) // Eğer runDistance birimden daha yakına gelirse koşmaya başlasın
        {
            // Koşma animasyonuna geçip hareket etmeye başlasın
            if (!isChasing)
            {
                isChasing = true;
                animator.SetBool("Attack",false);
                animator.SetBool("Running",true);
            }

            MoveTowardsPlayer(chaseSpeed); // Oyuncuya doğru koşarak hareket et
        }
        else
        {
            // Koşma animasyonunu durdur ve normal hızda hareket et
            if (isChasing)
            {
                isChasing = false;
                animator.SetBool("Attack",false);
                animator.SetBool("Running",false);
            }
        }
    }

    // Oyuncuya doğru hareket etmek için fonksiyon
    void MoveTowardsPlayer(float speed)
    {
        Vector3 direction = (player.position - transform.position).normalized; // Oyuncuya giden yön
        transform.position += direction * speed * Time.deltaTime; // Hareket
        RotateTowardsPlayer(direction); // Yönünü oyuncuya doğru çevir
    }

    // Oyuncuya doğru dönme
    void RotateTowardsPlayer(Vector3 direction)
    {
        Quaternion targetRotation = Quaternion.LookRotation(-direction); // Hedef rotayı hesapla
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime); // Yavaşça dön
    }

    // Saldırı fonksiyonu
    void AttackPlayer()
    {
        // Saldırı animasyonuna geçilebilir
        animator.SetBool("Attack",true);
        animator.SetBool("Running",false);

        // Karakterin canını azalt
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(attackDamage); // Karakterin canını azalt
        }

        // Bir süre saldırı durumu sürdürülebilir
        Invoke("ResetAttack", 1f); // 1 saniye sonra saldırıyı sıfırla
    }

    void ResetAttack()
    {
        isAttacking = false; // Saldırı bitti
    }
}
