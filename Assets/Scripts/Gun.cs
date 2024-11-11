using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour
{
    event OnDeath PlayerGetExp;

    public float speed = 1500f;
    public float fireRate = 0.1f;
    public float reloadTime = 2f;
    public int maxAmmo = 30;
    public float gunDamage = 40;
    public int fireRatePercent = 0;
    public AudioSource audioSource;
    [SerializeField]
    private int ammo;
    [SerializeField]
    private GameObject BulletPrefab;
    [SerializeField]
    private GameObject BulletPoint;
    private Player player;
    private Camera playerCamera;

    private bool isFiring;
    private bool isReloading;

    public bool HasAmmo => ammo > 0;
    public int GetAmmo => ammo;
    public int GetMaxAmmo => maxAmmo;

    private Animator animator; // Reference to the Animator component

    private void Awake()
    {
        animator = GetComponent<Animator>(); // Initialize the Animator component
    }


    void Start()
    {
        playerCamera = Camera.main;
        ammo = maxAmmo;
        player = GetComponentInParent<Player>();
        PlayerGetExp += player.GainExp;
    }

    void Update()
    {
        bool isInv = IsInvoking(nameof(Fire));

        if (isFiring && !isInv && !isReloading)
        {
            InvokeRepeating(nameof(Fire), 0f, fireRate - fireRate * fireRatePercent / 100);
        }
        else if ((!isFiring || isReloading) && isInv)
        {
            CancelInvoke(nameof(Fire));
        }
    }

    public void OnFire()
    {
        isFiring = !isFiring;
    }

    private void Fire()
    {
        if (!HasAmmo) return;
        ammo--;
        audioSource.Play();
        // Trigger recoil animation
        animator.SetTrigger("Recoil");

        Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.GetPoint(1000);
        }
        Vector3 direction = (targetPoint - BulletPoint.transform.position).normalized;

        GameObject bullet = Instantiate(BulletPrefab, BulletPoint.transform.position, BulletPoint.transform.rotation);
        bullet.GetComponent<Bullet>().SetParams(CalDamage(), PlayerGetExp);
        bullet.GetComponent<Rigidbody>().AddForce(direction * speed);

        if (!HasAmmo) StartCoroutine(Reload());
    }

    public void OnReload()
    {
        if (!isReloading && ammo != maxAmmo)
        {
            StartCoroutine(Reload());
        }
    }

    private IEnumerator Reload()
    {
        Debug.Log("Rozpoczato przeladowanie");
        isReloading = true;
        animator.SetTrigger("Reload");
        yield return new WaitForSeconds(reloadTime);
        ammo = maxAmmo;
        isReloading = false;
        Debug.Log("Zakonczono przeladowanie");
    }

    private float CalDamage()
    {
        Statistics playerStats = player.GetStats;
        return (playerStats.GetDamage + gunDamage) * (100 + playerStats.GetDamagePercent) / 100;
    }
}
