using TMPro;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager Instance;

    [SerializeField] TMP_Text ammoCnt;
    [SerializeField] Transform weaponSlot;
    [SerializeField] GameObject[] weapons;

    GameObject currentWeapon;

    public int[] ammo;
    public int weaponIndex;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
            return;
        }
        Instance = this;


        ammo = new int[3];
        ammo[0] = 100;
        ammo[1] = 0;
        ammo[2] = 0;
    }
    public void Equip(int index)
    {
        if (currentWeapon != null)
            Destroy(currentWeapon);
        currentWeapon = Instantiate(weapons[index], weaponSlot);
        currentWeapon.transform.localPosition = Vector3.zero;
        currentWeapon.transform.localRotation = Quaternion.identity;
    
        weaponIndex = index;
        AdjAmmoUI();
    }

    public int Ammo()
    {
        return ammo[weaponIndex];
    }

    public void UseAmmo()
    {
        if (weaponIndex == 0) return;
        ammo[weaponIndex]--;
        AdjAmmoUI();
    }

    public void GetShot()
    {
        ammo[1] += 5;
    }

    public void GetNade()
    {
        ammo[2] += 5;
    }

    void AdjAmmoUI()
    {
        ammoCnt.text = ammo[weaponIndex].ToString("N0");
    }
}
