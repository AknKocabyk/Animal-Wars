using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Takip edilecek nesne (örn: Karakter)
    public float smoothSpeed = 0.125f; // Kameranın yumuşaklık seviyesi
    public Vector3 offset; // Kameranın hedefe göre pozisyon farkı

    private void LateUpdate()
    {
        // Hedef pozisyonu hesapla
        Vector3 desiredPosition = target.position + offset;

        // Kamerayı yumuşak bir şekilde hareket ettir
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Kamerayı yeni pozisyona taşı
        transform.position = smoothedPosition;

        // Kamerayı hedefe döndür (isteğe bağlı)
        transform.LookAt(target);
    }
}
