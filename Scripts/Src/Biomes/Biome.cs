using UnityEngine;

public abstract class Biome : MonoBehaviour
{
    public abstract void SetBounds(Vector2 cellBounds);
    public abstract void SetOrigin(Vector3 origin);
    public abstract void SetParent(string parent);
    public abstract void Render();
}
