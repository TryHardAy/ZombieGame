using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private int level = 1;
    private float exp = 0;
    private float maxExp = 100;
    private float totalExp = 0; // Track total XP gained
    private Statistics stats;
    private bool isPaused = false;
    public PlayerUI playerUI;

    // Reference to UIManager
    public UIManager uiManager; // Ensure this is assigned in the Inspector

    // Gun actions
    private Gun gun;
    public Actions actions;
    private InputAction fire;
    private InputAction reload;
    private InputAction pause;

    // Items
    [SerializeField] private Transform interactionPoint;
    [SerializeField] private float interactionPointRadius = 2f;
    [SerializeField] private LayerMask itemsMask;

    private readonly Collider[] colliders = new Collider[3];
    [SerializeField] private int numFound;

    public Statistics GetStats => stats;

    // Start is called before the first frame update
    void Start()
    {
        stats = new Statistics();
        gun = gameObject.GetComponentInChildren<Gun>();
        gun.maxAmmo = stats.GetMaxAmmo;

        StartCoroutine(RegenHeatlh());
    }

    // Update is called once per frame
    void Update()
    {
        playerUI.UpdateAmmo(gun.GetAmmo, gun.GetMaxAmmo);
        playerUI.UpdateHealth(stats.GetHealth, stats.GetMaxHealth);
        playerUI.UpdateExp(exp, maxExp);

        gun.maxAmmo = stats.GetMaxAmmo;
        gun.fireRatePercent = stats.GetFireRatePercent;

        numFound = Physics.OverlapSphereNonAlloc(interactionPoint.position, interactionPointRadius, colliders, itemsMask);

        if (numFound > 0)
        {
            var pickup = colliders[0].GetComponent<IPickAble>();

            if (pickup != null) 
            {
                pickup.PickUp(stats);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(interactionPoint.position, interactionPointRadius);
    }

    private void Awake()
    {
        actions = new Actions();
    }

    private void OnEnable()
    {
        fire = actions.Player.Fire;
        fire.Enable();
        fire.performed += Fire;

        reload = actions.Player.Reload;
        reload.Enable();
        reload.performed += Reload;

        pause = actions.Player.Pause;
        pause.Enable();
        pause.performed += Pause;
    }

    private void OnDisable()
    {
        fire.Disable();
        reload.Disable();
        pause.Disable();
    }

    public void Reload(InputAction.CallbackContext context)
    {
        if (isPaused || stats.IsDead()) return;

            gun.OnReload();

    }

    public void Fire(InputAction.CallbackContext context)
    {
        if (isPaused || stats.IsDead()) return;
        gun.OnFire();
    }

    public void Pause(InputAction.CallbackContext context)
    {
        if (stats.IsDead()) return;

        isPaused = !isPaused;
        uiManager.ShowGameOver((int)totalExp);
    }

    void CheckForLevelUp()
    {
        if (HasEnoughtExp())
        {
            exp -= maxExp;
            LevelUp();
            maxExp *= 1.1f;
        }
    }

    bool HasEnoughtExp()
    {
        return exp >= maxExp;
    }

    public void GainExp(float exp)
    {
        this.exp += exp;
        this.totalExp += exp; // Update total XP
        CheckForLevelUp();
    }

    void LevelUp()
    {
        level++;
        stats.AddHealthRegen(2);
        stats.AddArmor(1);
        stats.AddMaxAmmo(20);
        stats.AddMaxHealth(50);
        Debug.Log($"Osiągnięto poziom {level}!");
    }

    private IEnumerator RegenHeatlh()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            stats.RegenHealth();
        }
    }

    // Method to handle damage taken from zombies or other sources
    public void TakeDamage(float damage)
    {
        stats.TakeDamage(damage);

        if (stats.IsDead())
        {
            Debug.Log("Player is dead!");
            // Show game over screen when player dies
            uiManager.ShowGameOver((int)totalExp); // Use total XP
        }
    }
}
