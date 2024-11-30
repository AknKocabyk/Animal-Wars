using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public AnimalGroup[] animalGroups;

    private void Start()
    {
        // 4 farklı hayvan grubu oluştur
        animalGroups = new AnimalGroup[4];
        animalGroups[0] = new AnimalGroup("Cow");
        animalGroups[1] = new AnimalGroup("Dog");
        animalGroups[2] = new AnimalGroup("Grup 3");
        animalGroups[3] = new AnimalGroup("Grup 4");
    }

    private void OnTriggerEnter(Collider other)
    {
        // Hayvanlarla etkileşim
        if (other.CompareTag("Animal"))
        {
            string group = other.GetComponent<Animal>().groupName;

            // İlgili grup puanını artır
            foreach (AnimalGroup animalGroup in animalGroups)
            {
                if (animalGroup.groupName == group)
                {
                    animalGroup.UpdateRelationshipPoints(5); // Örneğin, 5 puan ekle
                    Debug.Log($"{group} ile ilişkiniz iyileşti! Puan: {animalGroup.relationshipPoints}");
                }
            }
        }
    }
}
