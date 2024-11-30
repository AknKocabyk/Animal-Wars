using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float interactionRange = 3f;  // Hayvana yaklaşmak için mesafe
    public KeyCode interactKey = KeyCode.E;  // Etkileşim tuşu
    private Animal nearestAnimal;  // En yakın hayvan

    void Update()
    {
        // En yakın hayvanı bul
        FindNearestAnimal();

        if (nearestAnimal != null)
        {
            // Eğer 'E' tuşuna basılırsa ve hayvana yaklaşıldıysa
            if (Vector3.Distance(transform.position, nearestAnimal.transform.position) <= interactionRange && Input.GetKeyDown(interactKey))
            {
                PetAnimal(nearestAnimal);
            }
        }
    }

    void FindNearestAnimal()
    {
        // En yakın hayvanı bulmak için tüm hayvanları tarıyoruz
        Animal[] allAnimals = FindObjectsOfType<Animal>();
        float closestDistance = Mathf.Infinity;
        nearestAnimal = null;

        foreach (Animal animal in allAnimals)
        {
            float distance = Vector3.Distance(transform.position, animal.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                nearestAnimal = animal;
            }
        }
    }

    void PetAnimal(Animal animal)
    {
        // Hayvanı sevdiğimizde, grubun ilişki puanını artırıyoruz
        if (animal.group != null)
        {
            int pointsToIncrease = animal.group.groupName == "Dog" ? 10 : 5;  // Köpekler için daha fazla, inekler için daha az puan
            animal.group.IncreaseRelationshipPoints(pointsToIncrease);  
        }
    }
}
