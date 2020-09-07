using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMovement : MonoBehaviour
{
    public GameObject[] backgroundItems;
    private Camera mainCamera;
    private Vector2 screenLimits;
    public float distanceBetweenSprites;

    private void Start()
    {
        mainCamera = gameObject.GetComponent<Camera>();
        screenLimits = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
        foreach (GameObject obj in backgroundItems)
        {
            loadBackgroundSprites(obj);
        }
    }
    void loadBackgroundSprites(GameObject obj)
    {
        //Horizontal value (width)
        float objWidth = obj.GetComponent<SpriteRenderer>().bounds.size.x - distanceBetweenSprites;
        //figure out how many clones we need to fill the screen
        int childsNeeded = (int) Mathf.Ceil(screenLimits.x * 2 / objWidth);
        //Instantiate each object
        GameObject clone = Instantiate(obj) as GameObject;
        for(int i = 0; i <= childsNeeded; i++)
        {
            GameObject anotherClone = Instantiate(clone) as GameObject;
            anotherClone.transform.SetParent(obj.transform);
            //change position in X only by its width size
            anotherClone.transform.position = new Vector3(objWidth * i, obj.transform.position.y, obj.transform.position.z);
            //To keep it clean, make the clone have the same name as parent +i
            anotherClone.name = obj.name + " " + i;
        }
        for (int i = 1; i <= childsNeeded; i++)
        {
            GameObject anotherClone2 = Instantiate(clone) as GameObject;
            anotherClone2.transform.SetParent(obj.transform);
            //change position in X only by its width size
            anotherClone2.transform.position = new Vector3(objWidth * -i, obj.transform.position.y, obj.transform.position.z);
            //To keep it clean, make the clone have the same name as parent -i
            anotherClone2.name = obj.name + " " + -i;
        }
        Destroy(clone);
        Destroy(obj.GetComponent<SpriteRenderer>());
    }
    void repositionBackgroundChildObjects(GameObject obj)
    {
        Transform[] children = obj.GetComponentsInChildren<Transform>();
        if(children.Length > 1)
        {   
            // index 0 is parent object, so 1 is first child
            GameObject firstChild = children[1].gameObject;
            GameObject lastChild = children[children.Length - 1].gameObject;

            //The "transform" is in the center of the object, so we need to get half width
            float halfWidth = lastChild.GetComponent<SpriteRenderer>().bounds.extents.x - distanceBetweenSprites;

            if (transform.position.x + screenLimits.x > lastChild.transform.position.x + halfWidth) {
                firstChild.transform.SetAsLastSibling();
                firstChild.transform.position = new Vector3(lastChild.transform.position.x + halfWidth * 2, lastChild.transform.position.y, lastChild.transform.position.z);
            } else if(transform.position.x - screenLimits.x < firstChild.transform.position.x - halfWidth)
            {
                lastChild.transform.SetAsFirstSibling();
                lastChild.transform.position = new Vector3(firstChild.transform.position.x - halfWidth * 2, firstChild.transform.position.y, firstChild.transform.position.z);
            }
        }
    }

    //This is called one frame after update
    private void LateUpdate()
    {
        foreach(GameObject obj in backgroundItems)
        {
            repositionBackgroundChildObjects(obj);
        }
    }
}
