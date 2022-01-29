using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject _hitImage;
    [SerializeField] private GameStats _gameStats;
    [SerializeField] private PlayerStats _playerStats;
    [SerializeField] private float health;
    [SerializeField] private Text text;
    public float inputX;
    public float inputZ;
    
    void Start()
    {
        _playerStats.Health = 100;
        text.text = _playerStats.Health.ToString();
    }

    
    void Update()
    {
        Movement();
        FixHitScreen();
    }

    private void Movement()
    {
        if(_gameStats.isGameOver == false)
        {
            inputX = Input.GetAxis("Horizontal");
            inputZ = Input.GetAxis("Vertical");
            Vector3 direction = new Vector3(inputX, 0f, inputZ);
            transform.Translate(direction * _playerStats.Speed * Time.deltaTime);
            Run();
        }
        

    }

    public void GetDamage(float damage)
    {
        _playerStats.Health -= damage;
        text.text = _playerStats.Health.ToString();
        HitScreen();
    }

    private void RecoverHealth(float health)
    {
        _playerStats.Health += health;
        if (_playerStats.Health > 100)
        {
            _playerStats.Health = 100;
        }
        text.text = _playerStats.Health.ToString();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("healthboost"))
        {
            RecoverHealth(30);
            Destroy(collider.gameObject);
        }
    }
   
    private void HitScreen()
    {
        var hitColor = _hitImage.GetComponent<Image>().color;
        hitColor.a = 0.5f;
        _hitImage.GetComponent<Image>().color = hitColor;
    }

    private void FixHitScreen()
    {
        if (_hitImage.GetComponent<Image>().color.a > 0)
        {
            var hitColor = _hitImage.GetComponent<Image>().color;
            hitColor.a -= 0.02f;
                _hitImage.GetComponent<Image>().color = hitColor;
        } 
    }

    private void Run()
    {
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W))
        {
            _playerStats.Speed = 10f;
        }
        else
        {
            _playerStats.Speed = 6f;
        }
    }
   

}
