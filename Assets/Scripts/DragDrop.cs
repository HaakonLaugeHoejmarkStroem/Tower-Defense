using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    Vector3 offset;
    CanvasGroup canvasGroup;
    public GameObject turret;

    [SerializeField] Tilemap map;
    [SerializeField] GameObject heldObjectSprite;
    GameObject temp;

    void Awake()
    {
        if (gameObject.GetComponent<CanvasGroup>() == null)
            gameObject.AddComponent<CanvasGroup>();
        canvasGroup = gameObject.GetComponent<CanvasGroup>();
    }

    void Update()
    {
        if(temp != null)
        {
            if (temp.GetComponent<CollisionCheck>().onMap)
            {
                print("Can't");
            }
        }
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        temp.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0,0,10);

        
    }

    public void OnPointerDown(PointerEventData eventData)
    {

        temp = Instantiate(heldObjectSprite, Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 10), Quaternion.identity);
        canvasGroup.alpha = 0.5f;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Vector3Int position = new Vector3Int(Mathf.FloorToInt(temp.transform.position.x), Mathf.FloorToInt(temp.transform.position.y), Mathf.FloorToInt(temp.transform.position.z));

        if (!map.HasTile(position) && !temp.GetComponent<CollisionCheck>().onMap)
        {
            Instantiate(turret, temp.transform.position, Quaternion.identity);
            canvasGroup.alpha = 1;
            Destroy(temp.gameObject);
        }
        else
        {
            Destroy(temp.gameObject);
        }
        
    }
}
