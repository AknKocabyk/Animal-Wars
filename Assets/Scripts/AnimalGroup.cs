using UnityEngine;

public class AnimalGroup 
{
    public string groupName;
    public int relationshipPoints;
    public bool isAggressive;
    

    public AnimalGroup(string name)
    {
        groupName = name;
        relationshipPoints = 0; // Başlangıç puanı
        isAggressive = true; // Başlangıçta saldırgan
    }

    // İlişki puanı güncelleme metodu
    public void UpdateRelationshipPoints(int amount)
    {
        relationshipPoints += amount;

        // Eğer puan belli bir değeri aşarsa saldırganlık durumu değişir
        if (relationshipPoints >= 10)
        {
            isAggressive = false; // Artık saldırgan değil
        }
    }
}
