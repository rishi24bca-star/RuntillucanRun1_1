using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Slider = UnityEngine.UI.Slider;

public class UIPlayViewScript : MonoBehaviour
{
    [SerializeField] private Health healthScript;
    [SerializeField] private Canvas canvas;
   
    [SerializeField] private Slider _slider;
    [SerializeField]private TextMeshProUGUI  _text;
    [SerializeField] private TextMeshProUGUI _BulletText;
   int count = 0;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   

    void Start()
    {
        
        healthScript.OnPlayerTakenDmage += Onchange;
        PlayerAttack.OnPlayerBulletCountChanged += OnBulletCountChanged;
      _text =  canvas.GetComponentInChildren<TextMeshProUGUI>();

    }
    // Update is called once per frame
        

    void Onchange(int Max,int current )
    {
        
        Debug.Log(count++);
        float percentage = (float)current / (float)Max * 100 ;
        Debug.Log(percentage);
        _slider.value = percentage;
        _text.text = percentage.ToString();
    }

    void OnBulletCountChanged(int CurrentBullet_Count, int Bullet_Count)
    {
        _BulletText.text = CurrentBullet_Count.ToString() +  "/" + Bullet_Count.ToString();
    }
}
