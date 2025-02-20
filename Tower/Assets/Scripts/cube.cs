using UnityEngine;

public class cube : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) // Kule kurmak için TowerArea objesine týklayýnca açýlan panel
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit) && hit.transform == transform)
            {
                Transform panel = transform.Find("TowerPanel"); // Kendi panelini bul
                if (panel != null)
                    panel.gameObject.SetActive(!panel.gameObject.activeSelf); // Açýk/kapalý toggle
            }
        }
    }
}
