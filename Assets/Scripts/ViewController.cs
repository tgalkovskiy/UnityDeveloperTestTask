

using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ViewController : MonoBehaviour
{
    [SerializeField] private List<DataCell> _data = new List<DataCell>();
    [SerializeField] private EventCell _prefabCell = default;
    [SerializeField] private TMP_Text _scoreText = default;
    [SerializeField] private GameObject _star = default;
    [SerializeField] private Button _rightList;
    [SerializeField] private Scrollbar _scrollBar;
    [SerializeField] private Button _leftList;
    private float _scoreNow;
    private void Awake()
    {
        _rightList.onClick.AddListener(()=> {
            _scrollBar.value+=0.2f;
        });
        _leftList.onClick.AddListener(()=> {
            _scrollBar.value-=0.2f;
        });
        _scrollBar.onValueChanged.AddListener(ChangeHorizontalBar);
        InitCells();
    }

    private void InitCells()
    {
        foreach (var t in _data)
        { 
            Instantiate(_prefabCell, transform).SetParameters(this, t);
            _scoreNow += t.nowScore;
            _scoreText.text =$"{_scoreNow}";
        }
        transform.GetChild(0).SetSiblingIndex(_data.Count+1);
    }
    
    public void SceneLoader(int index)
    {
        SceneManager.LoadScene(index);
    }
    public void SetScore(int score)
    {
        LeanTween.value(gameObject, _scoreNow, score + _scoreNow, 1).setEaseOutQuad().setOnUpdate(UpdateNewValue);
        LeanTween.scale(_scoreText.gameObject, Vector3.one * 1.5f, 0.5f).setEaseOutBack().setOnComplete((() =>
        {
            LeanTween.scale(_scoreText.gameObject, Vector3.one * 1, 0.5f).setEaseOutBack();
        }));
        
    }
    private  void UpdateNewValue(float value)
    {
        _scoreNow = value;
        _star.transform.DORotateQuaternion(   _star.transform.rotation*Quaternion.Euler(0, 0, 180), 1);
        _scoreText.text =$"{((int)_scoreNow)}" ;
    }
    private void ChangeHorizontalBar(float value)
    {
        if (value < 0.1f)
        {
            _leftList.gameObject.SetActive(false);
        }
        if (value > 0.9f)
        {
            _rightList.gameObject.SetActive(false);
        }
        if (value > 0.1f && value < 0.9f)
        {
            _leftList.gameObject.SetActive(true);
            _rightList.gameObject.SetActive(true);
        }
    }
}
