using System;
using UnityEngine;
using System.Collections.Generic;

public class AnimalGroup : MonoBehaviour
{
    public string groupName;  // Grubun ismi (inek ya da köpek)
    public int relationshipPoints = 0;  // Grup için başlangıç ilişki puanı
    public List<Animal> animalsInGroup;  // Bu gruptaki hayvanlar
    public float decreaseRate = 1f;  // Puanın her saniye azalacağı oran

    private float timeSinceLastDecrease = 0f;  // Son azalmanın üzerinden geçen sür

    void Start()
    {
        // Hayvanları grup içinde otomatik olarak eklemek için
        animalsInGroup = new List<Animal>(GetComponentsInChildren<Animal>());
    }

    private void Update()
    {
        // Zamanla ilişki puanını azalt
        timeSinceLastDecrease += Time.deltaTime;

        if (timeSinceLastDecrease >= 1f)  // Her saniye puan azalt
        {
            DecreaseRelationshipPoints(1);  // Puanı 1 azalt
            timeSinceLastDecrease = 0f;  // Zaman sayacını sıfırla
        }
    }

    public void IncreaseRelationshipPoints(int amount)
    {
        relationshipPoints += amount;
        relationshipPoints = Mathf.Clamp(relationshipPoints, 0, 100);  // Puanları 0-100 arası sınırla
        Debug.Log($"{groupName} grubunun ilişki puanı: {relationshipPoints}");
    }
    
    public void DecreaseRelationshipPoints(int amount)
    {
        relationshipPoints -= amount;
        relationshipPoints = Mathf.Clamp(relationshipPoints, 0, 100);  // Puanları 0-100 arası sınırla
        Debug.Log($"{groupName} grubunun ilişki puanı: {relationshipPoints}");
    }
}
