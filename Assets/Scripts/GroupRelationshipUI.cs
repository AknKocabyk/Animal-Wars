using UnityEngine;
using UnityEngine.UI;

public class GroupRelationshipUI : MonoBehaviour
{
    public Slider relationshipSlider; // Ekrandaki slider
    public AnimalGroup activeGroup; // Şu anda takip edilen grup

    void Start()
    {
        if (relationshipSlider != null)
        {
            relationshipSlider.maxValue = 100; // Slider'ın maksimum değerini ayarla
        }
    }

    void Update()
    {
        if (activeGroup != null)
        {
            // Slider değerini grubun ilişki puanına göre ayarla
            relationshipSlider.value = activeGroup.relationshipPoints;
        }
    }

    public void SetActiveGroup(AnimalGroup group)
    {
        activeGroup = group; // Takip edilecek grubu ayarla
    }
}
