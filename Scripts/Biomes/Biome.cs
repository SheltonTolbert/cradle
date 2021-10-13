using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Biome : MonoBehaviour
{
    public abstract void SetBounds(Vector2 cellBounds);
    public abstract void SetOrigin(Vector3 origin);
    public abstract void Render();
}
