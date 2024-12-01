using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    public TMP_Text timerText;  // Zamanın görüneceği Text UI bileşeni
    private float timeElapsed = 0f;  // Geçen zaman (saniye cinsinden)

    void Start()
    {
        // Başlangıçta herhangi bir işlem yapmamıza gerek yok, timeElapsed zaten 0
    }

    void Update()
    {
        // Zamanı artır
        timeElapsed += Time.deltaTime;

        // Zamanı formatla ve UI'da göster
        DisplayTime(timeElapsed);
    }

    // Zamanı "dakika:saniye" formatında ekranda göster
    void DisplayTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);  // Dakika kısmı
        int seconds = Mathf.FloorToInt(time % 60);  // Saniye kısmı

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
