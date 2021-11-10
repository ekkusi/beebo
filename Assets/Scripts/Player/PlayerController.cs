using System;
using Photon.Pun;
using UnityEngine;

[RequireComponent(typeof(PlayerStateManager), typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour, IPunInstantiateMagicCallback
{
    private Rigidbody2D rigidBody;
    private PlayerStateManager stateManager;
    private PlayerStatsManager statsManager;
    private PlayerWeaponController weaponController;
    private PlayerInteraction playerInteraction;
    private SpriteRenderer spriteRenderer;
    private PhotonView view;
    private float immortalTimer = 0f;
    public bool canTakeDamage { get; private set; } = true;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Player start");
        rigidBody = GetComponent<Rigidbody2D>();
        stateManager = GetComponent<PlayerStateManager>();
        statsManager = GetComponent<PlayerStatsManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        weaponController = GetComponentInChildren<PlayerWeaponController>();
        playerInteraction = GetComponent<PlayerInteraction>();
        view = GetComponent<PhotonView>();

        // GeneralManager generalManager = GameObject.Find("DDOL").GetComponent<GeneralManager>();
        // generalManager.InitializePlayer(gameObject);
    }


    void Update()
    {
        if (view.IsMine)
        {

            if (!stateManager.isStateLocked)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    if (playerInteraction.interactionTarget != null)
                    {
                        playerInteraction.interactionTarget.Interact();
                    }
                    else
                    {
                        Attack();
                    }
                }
            }
            if (immortalTimer >= 0)
            {
                immortalTimer -= Time.deltaTime;
            }
            else if (!canTakeDamage)
            {
                canTakeDamage = true;
                spriteRenderer.color = Color.white;
                LayerUtils.SetLayerRecursively(gameObject, LayerMask.NameToLayer("CombatLayer"));
            }
        }
    }
    // Update by physics changes is called once per frame
    void FixedUpdate()
    {
        if (view.IsMine)
        {
            rigidBody.velocity = Vector2.zero;
            if (!stateManager.isStateLocked)
            {
                Vector3 change = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
                if (change != Vector3.zero)
                {
                    MovePlayer(change);
                    stateManager.ChangeState(PlayerState.HeroWalk);
                }
                else
                {
                    stateManager.ChangeState(PlayerState.HeroIdle);
                }
            }

        }
    }

    [PunRPC]
    private void PlayerLoadedLevel(Photon.Realtime.Player player, string previousScene, string newScene)
    {
        Debug.Log("Recevied from player: " + player.UserId);
        Debug.Log("Recevied RPC PlayerLoadedLevel, prev scene: " + previousScene + ", new scene: " + newScene);
        GameObject playerObj = (GameObject)player.TagObject;
        if (PhotonNetwork.IsMasterClient)
        {
            Debug.Log("Is master client");
            if (playerObj != null)
            {
                Debug.Log("Matching player obj found: !" + playerObj.name);
                if (newScene == gameObject.scene.name)
                {
                    // PhotonNetwork.Instantiate("Player", playerObj.transform.position, playerObj.transform.rotation);
                }
                else
                {
                    PhotonNetwork.Destroy(playerObj);
                }
            }
        }
    }

    void MovePlayer(Vector3 change)
    {
        rigidBody.MovePosition(transform.position + Vector3.Normalize(change) * statsManager.movementSpeed.value * Time.deltaTime);

        // Rotate player based on movement
        transform.eulerAngles = MathUtils.DirectionInEuler(change);
    }

    public void TakeDamage(float damage, GameObject source)
    {
        canTakeDamage = false;
        immortalTimer = 2f;
        spriteRenderer.color = Color.black;
        statsManager.TakeDamage(damage, source);
        LayerUtils.SetLayerRecursively(gameObject, LayerMask.NameToLayer("IgnoreCombatLayer"));
    }

    public void Attack()
    {
        float attackSpeed = statsManager.GetAttackSpeed();
        weaponController.ToggleWeaponHitBox(true, attackSpeed);
        stateManager.TriggerStateForLength(PlayerState.HeroHit, attackSpeed);
    }

    void IPunInstantiateMagicCallback.OnPhotonInstantiate(PhotonMessageInfo info)
    {
        gameObject.name = info.Sender.UserId;
        info.Sender.TagObject = gameObject;
    }
}
