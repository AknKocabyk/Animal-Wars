using UnityEngine;

public class Animal : MonoBehaviour
{
    public AnimalGroup group;  // Hayvanın ait olduğu grup

    void Start()
    {
        // Hayvanın ait olduğu grubu bul
        if (group == null)
        {
            group = GetComponentInParent<AnimalGroup>();
        }
    }
}
