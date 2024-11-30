using UnityEngine;

public class Animal : MonoBehaviour
{
      public string groupName; // Hayvanın ait olduğu grup
    public float moveSpeed = 3f; // Hayvanın hızını belirler
    public int damage = 10; // Oyuncuya verilen hasar
    public float attackRange = 1.5f; // Saldırı mesafesi

    private Transform player;
    private bool isAggressive;
    private bool isNearPlayer;

    private Animator animator;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // Oyuncuyu bul
        animator = GetComponent<Animator>(); // Animator bileşenini al
    }

    private void Update()
    {
        PlayerInteraction playerInteraction = FindObjectOfType<PlayerInteraction>();

        // Saldırganlık durumunu kontrol et
        foreach (AnimalGroup animalGroup in playerInteraction.animalGroups)
        {
            if (animalGroup.groupName == groupName)
            {
                isAggressive = animalGroup.isAggressive;
            }
        }

        // Eğer saldırganlık durumu aktifse koşma animasyonunu başlat
        animator.SetBool("isRunning", isAggressive);

        if (isAggressive)
        {
            // Oyuncuya doğru koş
            MoveTowardsPlayer();
        }

        // "E" tuşu ile ilişki puanını artır
        if (isNearPlayer && Input.GetKeyDown(KeyCode.Space))
        {
            foreach (AnimalGroup animalGroup in playerInteraction.animalGroups)
            {
                if (animalGroup.groupName == groupName)
                {
                    animalGroup.UpdateRelationshipPoints(5);
                    Debug.Log($"İlişki puanı arttı! Şu anki puan: {animalGroup.relationshipPoints}");
                }
            }
        }
    }

    private void MoveTowardsPlayer()
    {
        // Oyuncunun konumuna doğru hareket et
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance > attackRange)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;
        }
        else
        {
            isNearPlayer = true;
            AttackPlayer();
        }
    }

    private void AttackPlayer()
    {
        // Hasar ver
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage);
            Debug.Log($"Oyuncuya {damage} hasar verildi!");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Oyuncu uzaklaştığında tekrar saldırgan olmasın
        if (other.CompareTag("Player"))
        {
            isNearPlayer = false;
        }
    }
}
