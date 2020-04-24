using UnityEngine;

public class CustomerBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CheckClickOnCustomer();
    }

    /// <summary>
    /// TO-DO: find a way to attach the script to the Customer object?
    /// </summary>
    private void CheckClickOnCustomer()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var clickPos = Input.mousePosition;
            Vector3 worldPointClick = Camera.main.ScreenToWorldPoint(clickPos);

            RaycastHit2D hit = Physics2D.Raycast(new Vector2(worldPointClick.x, worldPointClick.y), Vector2.zero, 0);
            if (hit)
            {
                Debug.Log(hit.collider.tag);
                if (hit.collider.CompareTag("Customer"))
                {
                    Destroy(hit.collider.gameObject);
                    GameController.Instance.AddScore();
                }
            }
        }
    }
}
