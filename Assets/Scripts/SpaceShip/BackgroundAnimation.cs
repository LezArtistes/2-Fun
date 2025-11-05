using UnityEngine;

public class BackgroundAnimation : MonoBehaviour
{
    public float scrollSpeed = 2f;
    public float imageHeight = 10.8f;

    private Vector2 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        transform.Translate(Vector2.down * scrollSpeed * Time.deltaTime);

        if (transform.position.y <= -imageHeight)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + 2 * imageHeight);
        }
    }
}
