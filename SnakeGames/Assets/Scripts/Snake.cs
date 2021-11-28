using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    private Vector2 _direction = Vector2.right;
    private List<Transform> _tailList = new List<Transform>();

    public Transform _tailPrefab;
    public int initialSize = 4;

    // Start is called before the first frame update
    void Start()
    {
        ResetGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && _direction != Vector2.down)
        {
            _direction = Vector2.up;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && _direction != Vector2.left)
        {
            _direction = Vector2.right;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && _direction != Vector2.up)
        {
            _direction = Vector2.down;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && _direction != Vector2.right)
        {
            _direction = Vector2.left;
        }
    }

    private void FixedUpdate()
    {
        // _tailList'teki elemanlari sondan baslayarak her seferinde bir onceki elemanin konumuna atiyoruz.
        for (int i = _tailList.Count - 1; i > 0; i--)
        {
            _tailList[i].position = _tailList[i - 1].position;
        }

        // Edit > Project Settings > Time > Fixed Timestamp > 0.07
        // Yilanin istenilen yone gitmesini saglaniyor.
        this.transform.position = new Vector3(
            Mathf.Round(this.transform.position.x) + _direction.x,
            Mathf.Round(this.transform.position.y) + _direction.y,
            0.0f
            );
    }

    private void Grow()
    {
        // tail nesnesine tailPrefab nesnesini ornek aliyor.
        Transform tail = Instantiate(_tailPrefab);
        // ve konumunu _tailList listesindeki son elemanin posizyonuna assign ediyoruz.
        tail.position = _tailList[_tailList.Count - 1].position;
        _tailList.Add(tail);
    }

    // Food tagina sahip nesne tetiklenirse;
    // Su sekilde tetikleniyor: Box Collider > Is Trigger > Select
    // Bunu yapinca tetiklenince olacak seyi bu fonksiyonda belirtiyoruz.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Food")
        {
            Grow();
        }
        else if (collision.tag == "Obstacle")
        {
            ResetGame();
        }
    }

    private void ResetGame()
    {
        for (int i = 1; i < _tailList.Count; i++)
        {
            Destroy(_tailList[i].gameObject);
        }
        _tailList.Clear();
        _tailList.Add(transform);
        for (int i = 1; i < initialSize; i++)
        {
            _tailList.Add(Instantiate(_tailPrefab));
        }
        transform.position = Vector3.zero;
    }
}
