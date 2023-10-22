using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnWay : MonoBehaviour
{
    private GameController _gameController;
    public Transform platformCheck;
    public BoxCollider2D collider;

    // Start is called before the first frame update
    void Start()
    {
        _gameController = FindObjectOfType(typeof(GameController)) as GameController;
    }

    // Update is called once per frame
    void Update()
    {
        if(platformCheck.position.y < _gameController.posY){
            collider.enabled = true;
        }else{
            collider.enabled = false;
        }
    }
}
