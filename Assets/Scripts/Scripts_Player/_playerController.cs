using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(CharacterController))]
public class _playerController : MonoBehaviour
{

    [Header("References")]
    GameManager manager;
    _sceneController scene;
    CharacterController _controller;
    Camera _playerCamera;
    public GameObject Player;
    public _effectController effect;
    public Transform groundCheck;
    [Header("Movement Stats")]
    float _horizontal, _vertical;
    public Vector3 _movementVector, _move;
    [SerializeField] private float _moveSpeed;
    public bool _running = false;
    bool changeDir;
    public bool isInteracting;
    [Header("Dash")]
    float _distance = 3f;
    float _duration = .5f;
    float _dashCooldown = 2f;
    public bool _dashing = false;
    bool _dashAvailable = true;
    Vector3 _direction;
    float _cooldownTimer, _timer;

#region Unity Monobehaviors
    private void Awake()
    {
        _playerCamera = Camera.main;
        _timer = _duration;
        _cooldownTimer = _dashCooldown; 
        _controller = GetComponent<CharacterController>();
        effect = Camera.main.GetComponent<_effectController>();
        changeDir = false;
        isInteracting = false;
    }
    void Start()
    {
        manager = GameManager.instance;
        scene = _sceneController.instance;
    }
    private void Update()
    {
        if(GetComponent<_attackController>()._attacking == false| GetComponent<_attackController>()._cast) Movement();
        if(GetComponent<_attackController>()._attacking   || GetComponent<_attackController>()._special ) GetComponent<_attackController>().currWeapon.GetComponent<MeshRenderer>().enabled = true;
        else  GetComponent<_attackController>().currWeapon.GetComponent<MeshRenderer>().enabled = true;
        Dash(); 
        Interact();
    }

    private void OnTriggerEnter(Collider _col)
    {
        if(_col.CompareTag("Portal"))
        {
            effect.PortalShake();
            scene.StartLevel();
        }
        if(_col.CompareTag("Audio"))
        {
            _col.GetComponent<AudioSource>().Play();
            _col.enabled = false;
            StartCoroutine(WaitInteract(8f));

        }
        if(_col.CompareTag("CameraSwitch"))
        {
            Vector3 eulerAngles = _playerCamera.transform.eulerAngles;
            eulerAngles.y += 90f;
            Quaternion targetRotation = Quaternion.Euler(eulerAngles);
            _playerCamera.GetComponent<_camera>()._offset = new Vector3(-4,3,0);
            _playerCamera.transform.rotation = Quaternion.Slerp(_playerCamera.transform.rotation, targetRotation,1000f);
            changeDir = true;
            _col.enabled = false;
        }
        if(_col.CompareTag("Enemy Trigger"))
        {
            manager.SpawnEnemy(_col.GetComponent<SpawnPoints>().amount, _col.GetComponent<SpawnPoints>().spawnPosition, _col.GetComponent<SpawnPoints>().enemytype);
            _col.enabled = false;
        }
         if(_col.CompareTag("Enemy Weapon"))
        {
            GetComponent<_playerStats>().TakeDamage(5f);
        }
        if(_col.CompareTag("envi"))
        {
            for(int i = 0; i < manager.characters.Count; i++)
            {
                manager.characters[i].SetActive(true);
            }
        }
    }
#endregion

private void Movement()
{
    _horizontal = Input.GetAxisRaw("Horizontal");
    _vertical = Input.GetAxisRaw("Vertical");
    bool isGrounded = GroundCheck() ;

    // if(isGrounded == true)
    // {
    // } 
    if(isGrounded == false)
    {
    _movementVector = new Vector3(_horizontal * _moveSpeed * Time.deltaTime, -9.81f * Time.deltaTime, _vertical * _moveSpeed * Time.deltaTime);
    } if(changeDir == true ) _movementVector = new Vector3(_vertical * _moveSpeed * Time.deltaTime, -9.81f * Time.deltaTime, -1f * _horizontal * _moveSpeed * Time.deltaTime);
    else {
    _movementVector = new Vector3(_horizontal * _moveSpeed * Time.deltaTime, 0, _vertical * _moveSpeed * Time.deltaTime);
    }

    _move = _movementVector;
    if(_horizontal != 0 || _vertical != 0) _running = true; 
    else _running = false; 

    
    _move.Normalize();
    if(_movementVector != Vector3.zero)
    {
        _move.y = 0;
      transform.LookAt(transform.position + _move);
    }
    _controller.Move(_movementVector);
}

void Dash()
{
    if(_dashAvailable && Input.GetButtonDown("Dash"))
    {
        if(changeDir == true ) _direction = new Vector3(Input.GetAxis("Vertical"), 0f,-1f * Input.GetAxis("Horizontal")).normalized;
        else _direction = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical")).normalized;
         
        _dashing = true;
        _dashAvailable = false;
        _timer = 0f;
        _cooldownTimer = 0f;
       
    }

    if(_dashing)
    {
        _controller.Move(_direction * _distance / _duration * Time.deltaTime);
        _timer += Time.deltaTime;
        if(_timer >= _duration) 
        {
            _dashing = false;
            _cooldownTimer = 0f;
        } else if(!_dashAvailable)
        {
            StartCoroutine(ResetDash());
        }
    }
}

bool GroundCheck()
{
   RaycastHit ground;
        if(Physics.Raycast(groundCheck.position, Vector3.down, out ground, 1f))
        {
            if(ground.collider.CompareTag("Ground"))
                return true;
            else return false;
        } else return false;
}

IEnumerator ResetDash()
{
    yield return new WaitForSeconds(_dashCooldown);
    _dashAvailable = true;
}
void Interact()
{
    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    RaycastHit hit;

    Physics.Raycast(ray, out hit);
    if(Input.GetKeyDown(KeyCode.E))
    {
        StartCoroutine(WaitInteract(4f));
        if(hit.collider.gameObject.CompareTag("Weapon"))
        {   
            float amount = hit.collider.gameObject.GetComponent<_weaponController>().purchaseAmount;
            if(amount <= GetComponent<_playerStats>().coins && !hit.collider.gameObject.GetComponent<_weaponController>().isPurchased)
            {
                GetComponent<_playerStats>().coins -= amount;
                hit.collider.gameObject.GetComponent<_weaponController>().isPurchased = true;
                // GetComponent<_attackController>().Sword = hit.collider.gameObject.gameObject;
                if(hit.collider.gameObject.name != GetComponent<_attackController>().currWeapon.name )
                {
                   if(hit.collider.gameObject.name == GetComponent<_attackController>().Pole.name )
                   {
                    GetComponent<_attackController>().Pole.SetActive(true);
                    GetComponent<_attackController>().Pole.GetComponent<_weaponController>().isEquipped = true;
                    GetComponent<_attackController>().currWeapon.SetActive(false);
                    GetComponent<_attackController>().currWeapon = GetComponent<_attackController>().Pole;

                   }
                   if(hit.collider.gameObject.name == GetComponent<_attackController>().Sword.name )
                   {
                    GetComponent<_attackController>().Sword.SetActive(true);
                    GetComponent<_attackController>().Sword.GetComponent<_weaponController>().isEquipped = true;
                    GetComponent<_attackController>().currWeapon.SetActive(false);
                    GetComponent<_attackController>().currWeapon = GetComponent<_attackController>().Sword;

                   }
                   if(hit.collider.gameObject.name == GetComponent<_attackController>().Scythe.name )
                   {
                    GetComponent<_attackController>().Scythe.SetActive(true);
                    GetComponent<_attackController>().Scythe.GetComponent<_weaponController>().isEquipped = true;
                    GetComponent<_attackController>().currWeapon.SetActive(false);

                    GetComponent<_attackController>().currWeapon = GetComponent<_attackController>().Scythe;

                   }
                   if(hit.collider.gameObject.name == GetComponent<_attackController>().Spear.name )
                   {
                    GetComponent<_attackController>().Spear.SetActive(true);
                    GetComponent<_attackController>().Spear.GetComponent<_weaponController>().isEquipped = true;
                    GetComponent<_attackController>().currWeapon.SetActive(false);
                    GetComponent<_attackController>().currWeapon = GetComponent<_attackController>().Spear;

                   }
                }
                

            } else if(amount >= GetComponent<_playerStats>().coins){
                    Debug.Log("Not enough coins");
            } else if(hit.collider.gameObject.GetComponent<_weaponController>().isPurchased && !hit.collider.gameObject.GetComponent<_weaponController>().isEquipped)
            {
                GetComponent<_attackController>().Sword = hit.collider.gameObject;
                GetComponent<_attackController>().Sword.SetActive(false);

            }
        } 

        if(hit.collider.CompareTag("Guide"))
        {
        hit.collider.GetComponent<DialogueController>().Interact();
        }
        if(hit.collider.CompareTag("Gatekeeper"))
        {
        hit.collider.GetComponent<DialogueController>().Interact();
        }
    }

}
IEnumerator WaitInteract(float time)
{
    _running = false;
    isInteracting = true;
    this.enabled =false;
    yield return new WaitForSeconds(time);
    this.enabled =true;
    isInteracting = false;
}
}
