#region Usings
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
#endregion
namespace UIView
{ 
    public class EventCell : MonoBehaviour
    {
        #region Fileds

        [SerializeField] private Button _play = default;
        [SerializeField] private Image _mainImageEvent = default;
        [SerializeField] private TMP_Text _maxScore = default;
        [SerializeField] private TMP_Text _nowScore = default;
        private DataCell _dataCell;
        private ViewController _controller;

        #endregion

        #region Action

        private Action<int> _getCount;

        private Action<int> _setLevel;
        //and e.t.c

        #endregion

        #region PublicMetods

        public void SetParameters(ViewController controller, DataCell dataCell)
        {
            _dataCell = dataCell;
            _controller = controller;
            _getCount += _controller.SetScore;
            _mainImageEvent.sprite = _dataCell._mainImage;
            _maxScore.text = $"{_dataCell.maxScore}";
            _nowScore.text = $"{_dataCell.nowScore}";
            _play.onClick.AddListener((() =>
            {
                if (_dataCell.nowScore == _dataCell.maxScore) return;
                _dataCell.nowScore = Mathf.Clamp(_dataCell.nowScore + 5, 0, _dataCell.maxScore);
                _nowScore.text = _dataCell.nowScore.ToString();
                _getCount?.Invoke(5);
            }));
        }

        #endregion
    }
}
