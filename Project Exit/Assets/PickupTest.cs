using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;

public class PickupTest : MonoBehaviour
{
    // Start is called before the first frame update
    InventoryManager InvManager;
    public Camera MainCamera;
    void Start()
    {
        MainCamera = Camera.main;
        InvManager = this.GetComponent<InventoryManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (MainCamera == null)
        {
            MainCamera = Camera.main;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = MainCamera.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction * Mathf.Infinity, Color.green);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
            {
                if (hit.collider.gameObject.GetComponent<KeyItem>())
                {
                    InventoryManager.Instance.AddItem(hit.collider.gameObject.GetComponent<KeyItem>());
                }
                if (hit.collider.gameObject.GetComponent<InteractableObject>())
                {
                    hit.collider.gameObject.GetComponent<InteractableObject>().Interact();
                }

            }
        }
    }
}
