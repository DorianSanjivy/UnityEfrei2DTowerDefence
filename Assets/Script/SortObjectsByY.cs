using System.Linq;
using UnityEngine;

public class SortObjectsByY : MonoBehaviour
{
    // Specify the layer to filter objects
    public string targetLayerName = "YourLayerName";

    void Update()
    {
        SortObjects();
    }

    void SortObjects()
    {
        // Get all objects in the scene
        GameObject[] allObjects = FindObjectsOfType<GameObject>();

        // Filter objects based on the target layer
        var objectsInLayer = allObjects
            .Where(obj => obj.layer == LayerMask.NameToLayer(targetLayerName))
            .ToList();

        // Sort objects based on the y position of their parent (if they have one) or their own y position
        objectsInLayer = objectsInLayer
            .OrderBy(obj =>
            {
                Transform parent = obj.transform.parent;
                return parent != null ? parent.position.y : obj.transform.position.y;
            })
            .ToList();

        // Assign sorting order based on the sorted list
        for (int i = 0; i < objectsInLayer.Count; i++)
        {
            SpriteRenderer spriteRenderer = objectsInLayer[i].GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                spriteRenderer.sortingOrder = i; // Assign sorting order based on index
            }
        }
    }
}
