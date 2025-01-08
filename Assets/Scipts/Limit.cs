using UnityEngine;

public class Limit : MonoBehaviour
{



    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag(TagConst.PLATFORM))
            Destroy(collision.gameObject);
    }
}
