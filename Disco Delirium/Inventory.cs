using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // scripts
    [SerializeField] private Weapons thisWeapon;
    private PlayerAttacking playerAttacking;
    public GameObject[] inventory;
    public bool[] _isFull;


    public int _selectedSlot;

    private void Start()
    {
        playerAttacking = GetComponent<PlayerAttacking>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            playerAttacking.weapons = Weapons.Empty;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (inventory[0].GetComponent<Slot>().item != null)
            {
                playerAttacking.weapons = inventory[0].GetComponent<Slot>().item;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (inventory[1].GetComponent<Slot>().item != null)
            {
                playerAttacking.weapons = inventory[1].GetComponent<Slot>().item;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (inventory[2].GetComponent<Slot>().item != null)
            {
                playerAttacking.weapons = inventory[2].GetComponent<Slot>().item;
            }
        }

    }
}
