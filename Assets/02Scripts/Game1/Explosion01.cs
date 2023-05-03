using System.Runtime.CompilerServices;
using UnityEngine;

public class Explosion01 : MonoBehaviour
{
    public AnimateG1 start;
    public AnimateG1 middle;
    public AnimateG1 end;

    public void SetActiveRenderer(AnimateG1 renderer)
    {
        //UnityEngine.Debug.Log("EX");
        start.enabled = renderer == start;
        middle.enabled = renderer == middle;
        end.enabled = renderer == end;
    }

    public void SetDirection(Vector2 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x);
        transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);
    }

    public void DestroyAfter(float seconds)
    {
        Destroy(gameObject, seconds);
    }
}
