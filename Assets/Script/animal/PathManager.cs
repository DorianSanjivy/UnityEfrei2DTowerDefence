using UnityEngine;

public class PathManager : MonoBehaviour
{
    // test comment github
    public Transform[] nodes;

    private void OnDrawGizmos()
    {
        // Draw lines between nodes for visualization in the editor
        Gizmos.color = Color.green;
        for (int i = 0; i < nodes.Length - 1; i++)
        {
            Gizmos.DrawLine(nodes[i].position, nodes[i + 1].position);
        }
    }
}
