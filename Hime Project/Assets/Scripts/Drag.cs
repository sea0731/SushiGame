using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour
{
    public GameObject table;
    public GameObject startUP, startDown;
    public float speed = 0.02f;

    Collider2D _collider;
    Vector2 moveSpeed;
    int ScreenWidth;

    bool isDrag = false;

    
    public void OnMouseDrag()
    {
        isDrag = true;
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mousePosition.x, mousePosition.y, transform.position.z);
    }

    private void OnMouseUp()
    {
        isDrag = false;
        if (_collider.IsTouching(table.GetComponent<Collider2D>()))
        {
            transform.position = table.transform.position;
            StartCoroutine(move());
        }
    }

    IEnumerator move()
    {
        moveSpeed = new Vector2(speed, 0);

        while (!isDrag)
        {
            transform.Translate(moveSpeed* Time.fixedDeltaTime);
            yield return null;
        }
        Debug.Log("Don't move");
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 8)
        {
            transportToTheOtherStart(collision.gameObject);
            moveSpeed = -moveSpeed;
        }
    }

    private void transportToTheOtherStart(GameObject end)
    {
        print(end.name);
        if (end.name == "endUP")
            transform.position = startDown.transform.position;
        else if (end.name == "endDown")
            transform.position = startUP.transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        ScreenWidth = Screen.width;
        print(ScreenWidth);
        _collider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
