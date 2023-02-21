using _Source.Actors.Player.Equipment;
using _Source.Actors.Player.Scripts;
using _Source.Actors.Player.Weapons;
using _Source.Coordinators;
using UnityEngine;

public class WeaponSlot : MonoBehaviour
{
    [SerializeField] private Weapon activeWeapon;
    
    public Weapon ActiveWeapon
    {
        get => activeWeapon;
        set => activeWeapon = value;
    }
    private Transform meshOriginForWeapon;
    
    private void OnEnable()
    {
        InputReader.BaseAttackEvent += OnBaseAttackInput;
        InputReader.HeavyAttackEvent += OnHeavyAttackInput;
    }
    
    private void Awake()
    {
        activeWeapon = GetComponentInChildren<Weapon>();
    }

    private void Start()
    {
        EquipmentPanel.ResetSword += OnResetSword;
    }

    private void OnDestroy()
    {
        EquipmentPanel.ResetSword -= OnResetSword;
        InputReader.BaseAttackEvent -= OnBaseAttackInput;
        InputReader.HeavyAttackEvent -= OnHeavyAttackInput;
    }
    
    private void OnBaseAttackInput()
    {
        activeWeapon.BaseAttack();
    }
    
    private void OnHeavyAttackInput()
    {
        activeWeapon.HeavyAttack();
    }

    public void ToggleHitBoxFromAnimation(bool state)
    {
        activeWeapon.HitBox.ToggleHitBoxFromAnimation(state);
    }

    public void PlayWeaponSoundFromAnimation(int state)
    {
        activeWeapon.WeaponSounds.PlayAttackSoundFromAnimation(state);
    }
    
    private void OnResetSword()
    {
        activeWeapon.CanAttack = false;
        activeWeapon.GetComponent<DamageDealer>().BaseDamage = 0;
        activeWeapon.GetComponent<DamageDealer>().CritDamage = 0;
        
        activeWeapon.gameObject.SetActive(false);
    }

    public void SetStats(InventoryItem obj)
    {
        activeWeapon.gameObject.SetActive(true);
        
        activeWeapon.CanAttack = true;
        activeWeapon.GetComponent<DamageDealer>().BaseDamage = obj.Stats["Damage"];
        activeWeapon.GetComponent<DamageDealer>().CritDamage = obj.Stats["CriticalDamage"];
    }

    #region Animation Events
    public void OnAttackAnimationHit()
    {
       //..
    }
    #endregion
}