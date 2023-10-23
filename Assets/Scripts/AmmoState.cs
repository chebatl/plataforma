using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoState : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider) {
        if(collider.CompareTag(TagsType.GROUND.ToString())){
            Destroy(this.gameObject);
        }
    }
}
