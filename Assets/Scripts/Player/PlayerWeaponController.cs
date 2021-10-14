using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{
    private PlayerEquipmentManager equipmentManager;
    // Start is called before the first frame update
    private SpriteRenderer spriteRenderer;
    public Transform noWeaponHitBox { get; private set; }
    public Transform weaponHitBox { get; private set; }
    private bool isWeaponActive = false;
    private float activeTimer = 0f;
    void Start()
    {
        equipmentManager = transform.parent.GetComponent<PlayerEquipmentManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        noWeaponHitBox = transform.Find("HitBoxWeapon");
        weaponHitBox = transform.Find("HitBoxNoWeapon");
        ToggleWeaponHitBox(false);

        // Change sprite according to equipped weapon
        WeaponObject playerWeapon = (WeaponObject)equipmentManager.GetEquippedItem(EquipmentSlot.WeaponMainHand);
        if (playerWeapon != null)
        {
            spriteRenderer.sprite = playerWeapon.sprite;
        }
        else
        {
            spriteRenderer.sprite = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (activeTimer > 0)
        {
            activeTimer -= Time.deltaTime;
        }
        else if (isWeaponActive && activeTimer <= 0)
        {
            ToggleWeaponHitBox(false);
        }
    }
    public void ToggleWeaponHitBox(bool isActive, float length = 0f)
    {
        // First ensure that all are inactive
        isWeaponActive = isActive;
        weaponHitBox.gameObject.SetActive(false);
        noWeaponHitBox.gameObject.SetActive(false);
        if (!isActive) return;
        activeTimer = length;
        WeaponObject playerWeapon = (WeaponObject)equipmentManager.GetEquippedItem(EquipmentSlot.WeaponMainHand);
        if (playerWeapon != null)
        {
            noWeaponHitBox.gameObject.SetActive(true);
        }
        else
        {
            weaponHitBox.gameObject.SetActive(true);
        }

    }
}
