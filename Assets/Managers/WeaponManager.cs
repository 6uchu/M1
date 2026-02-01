using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] Transform weaponSlot;
    [SerializeField] GameObject[] weapons;

    GameObject currentWeapon;

    public void Equip(int index)
    {
        if (currentWeapon != null)
            Destroy(currentWeapon);
        currentWeapon = Instantiate(weapons[index], weaponSlot);
        currentWeapon.transform.localPosition = Vector3.zero;
        currentWeapon.transform.localRotation = Quaternion.identity;
    }
}
