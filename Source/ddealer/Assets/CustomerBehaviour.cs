using UnityEngine;

public class CustomerBehaviour : MonoBehaviour
{
    private const float MIN_X_SPAWN = 21F;
    private const float MAX_X_SPAWN = 35F;
    private const float MIN_Y_SPAWN = -2F;
    private const float MAX_Y_SPAWN = -4.5F;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var clickPos = Input.mousePosition;
            RaycastHit2D hit = Physics2D.Raycast(new Vector2(Camera.main.ScreenToWorldPoint(clickPos).x, Camera.main.ScreenToWorldPoint(clickPos).y), Vector2.zero, 0);
            if (hit)
            {
                if (hit.collider.CompareTag("Customer"))
                {
                    Debug.Log("Clickato sul Customer");
                }
            }
        }
    }
}
