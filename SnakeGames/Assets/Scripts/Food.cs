using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    // food'un uretilecegi alani belirtiyor.
    public BoxCollider2D gameArea;

    private void RandomizePosition()
    {
        // GameArea'nin sinirlari alindi.
        Bounds bounds = this.gameArea.bounds;

        // GameArea'nin max ve min sinirlari icerisinde bir deger uretildi
        // ve virgullu deger olmamasi icin yuvarlandi.
        float x = Mathf.Round(Random.Range(bounds.min.x, bounds.max.x));
        float y = Mathf.Round(Random.Range(bounds.min.y, bounds.max.y));

        // Uretilen deger atandi.
        this.transform.position = new Vector3(x, y, 0.0f);
    }

    // Player tagina sahip nesne tetiklenirse;
    // Su sekilde tetikleniyor: Box Collider > Is Trigger > Select
    // Bunu yapinca tetiklenince olacak seyi bu fonksiyonda belirtiyoruz.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            RandomizePosition();
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        RandomizePosition();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
