using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private PlayerController _playerController;
    public float posY;
    [SerializeField]
    private int ammo;

    // Start is called before the first frame update
    void Start()
    {
       _playerController = FindObjectOfType(typeof(PlayerController)) as PlayerController;
       // Posso deixar public e arrastar o Player na unity para esse objeto
    }

    // Update is called once per frame
    void Update()
    {
        posY = _playerController.transform.position.y;
    }

    public void changeAmmo(int quantity){
        ammo += quantity;
    }

    public int getAmmo(){
        return ammo;
    }
}
