using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
    public float interactionRange = 3f;  // Hayvana yaklaşmak için mesafe
    public KeyCode interactKey = KeyCode.E;  // Etkileşim tuşu
    private Animal nearestAnimal;  // En yakın hayvan

    public Slider dogSlider;  // Köpek grubunun ilişki puanını gösteren Slider
    public Slider cowSlider;  // İnek grubunun ilişki puanını gösteren Slider

    public AnimalGroup dogGroup;  // Köpek grubu referansı
    public AnimalGroup cowGroup;  // İnek grubu referansı

    void Start()
    {
        // Sliderların maksimum değerini ayarla
        if (dogSlider != null) dogSlider.maxValue = 100;
        if (cowSlider != null) cowSlider.maxValue = 100;

        // Başlangıç değerlerini sıfırla
        if (dogGroup != null) dogSlider.value = dogGroup.relationshipPoints;
        if (cowGroup != null) cowSlider.value = cowGroup.relationshipPoints;
    }

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

        // Slider değerlerini grupların ilişki puanlarına göre güncelle
        UpdateSliders();
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

    void UpdateSliders()
    {
        // DogGroup Slider'ı güncelle
        if (dogGroup != null && dogSlider != null)
        {
            dogSlider.value = dogGroup.relationshipPoints;
        }

        // CowGroup Slider'ı güncelle
        if (cowGroup != null && cowSlider != null)
        {
            cowSlider.value = cowGroup.relationshipPoints;
        }
    }
}
