using UnityEngine;

public class Brick : MonoBehaviour
{
    public Renderer MainRenderer;
    public Vector2 Size = new Vector2(1, 1);


    public void SetTransparent(bool available)
    {
        if (available)
        {
            MainRenderer.material.color = new Color(0, 0.1f, 0, 0.1f);
        }
        else
        {
            MainRenderer.material.color = new Color(1, 0, 0, 0.1f);
        }
    }

    public void SetNormal()
    {
        MainRenderer.material.color = Color.white;
    }

    private void OnDrawGizmos()
    {
        for (int x = 0; x < Size.x; x++)
        {
            for (int y = 0; y < Size.y; y++)
            {
                if ((x + y) % 2 == 0) Gizmos.color = new Color(0.88f, 0f, 1f, 0.3f);
                else Gizmos.color = new Color(1f, 0.68f, 0f, 0.3f);

                Gizmos.DrawCube(transform.position, new Vector3(1, .1f, 1));
            }
        }
    }

    
    
}