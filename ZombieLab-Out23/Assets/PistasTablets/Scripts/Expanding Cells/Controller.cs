using UnityEngine;
using EnhancedUI;
using EnhancedUI.EnhancedScroller;

namespace EnhancedScrollerDemos.ExpandingCells
{
	public class Controller : MonoBehaviour, IEnhancedScrollerDelegate
    {
        public Enigma enigma;

        private SmallList<DataPista> _data;
        private bool _lastPadderActive;
        private float _lastPadderSize;
        public EnhancedScroller scroller;
        public EnhancedScrollerCellView cellViewPrefab;
        public void InitList()
        {
            scroller.Delegate = this;

            scroller.lookAheadBefore = 1000f;
            scroller.lookAheadAfter = 1000f;

            LoadData();
        }

        private void LoadData()
        {

            _data = new SmallList<DataPista>();

            foreach (Pista pista in enigma.Pistas)
            {
                if (pista.image != null)
                {
                    _data.Add(new DataPista()
                    {
                        headerText = pista.PistaName,
                        descriptionText = pista.TextRich,
                        imageOptional = pista.image,
                        isExpanded = false,
                        expandedSize = pista.sizeText,
                        collapsedSize = 60f,
                        tweenType = Tween.TweenType.easeInOutSine,
                        tweenTimeExpand = 0.5f,
                        tweenTimeCollapse = 0.5f
                    });
                }
                else
                {
                    _data.Add(new DataPista()
                    {
                        headerText = pista.PistaName,
                        descriptionText = pista.TextRich,
                        isExpanded = false,
                        expandedSize = pista.sizeText,
                        collapsedSize = 60f,
                        tweenType = Tween.TweenType.easeInOutSine,
                        tweenTimeExpand = 0.5f,
                        tweenTimeCollapse = 0.5f
                    });
                }
            }

            scroller.ReloadData();
        }

        private void InitializeTween(int dataIndex, int cellViewIndex)
        {
            _data[dataIndex].isExpanded = !_data[dataIndex].isExpanded;

            for (var i = 0; i < _data.Count; i++)
            {
                if (i != dataIndex)
                {
                    if (((dataIndex + 2) % 3 == 0) || ((i + 2) % 3 == 0))
                    {
                        _data[i].isExpanded = false;
                    }
                }
            }

            var cellPosition = scroller.GetScrollPositionForCellViewIndex(cellViewIndex, EnhancedScroller.CellViewPositionEnum.Before);

            var tweenCellOffset = cellPosition - scroller.ScrollPosition;

            scroller.IgnoreLoopJump(true);

            scroller.ReloadData();

            cellPosition = scroller.GetScrollPositionForCellViewIndex(cellViewIndex, EnhancedScroller.CellViewPositionEnum.Before);

            scroller.SetScrollPositionImmediately(cellPosition - tweenCellOffset);

            scroller.IgnoreLoopJump(false);

            if (_data[dataIndex].tweenType == Tween.TweenType.immediate)
            {
                return;
            }

            _lastPadderActive = scroller.LastPadder.IsActive();
            _lastPadderSize = scroller.LastPadder.minHeight;

            if (_data[dataIndex].isExpanded)
            {
                scroller.LastPadder.minHeight += _data[dataIndex].SizeDifference;
            }
            else
            {
                scroller.LastPadder.minHeight -= _data[dataIndex].SizeDifference;
            }

            scroller.LastPadder.gameObject.SetActive(true);

            var cellViewTween = scroller.GetCellViewAtDataIndex(dataIndex) as CellView;

            cellViewTween.BeginTween();
        }

        private void TweenUpdated(int dataIndex, int cellViewIndex, float newValue, float delta)
        {
            scroller.LastPadder.minHeight -= delta;
        }

        private void TweenEnd(int dataIndex, int cellViewIndex)
        {
            scroller.LastPadder.gameObject.SetActive(_lastPadderActive);

            scroller.LastPadder.minHeight = _lastPadderSize;
        }

        #region EnhancedScroller Handlers

        public int GetNumberOfCells(EnhancedScroller scroller)
        {
            return _data.Count;
        }

        public float GetCellViewSize(EnhancedScroller scroller, int dataIndex)
        {
            return _data[dataIndex].Size;
        }


        public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
        {

            CellView cellView = scroller.GetCellView(cellViewPrefab) as CellView;

            cellView.name = "Cell " + dataIndex.ToString();

            cellView.SetData(_data[dataIndex], dataIndex, _data[dataIndex].collapsedSize, _data[dataIndex].expandedSize, InitializeTween, TweenUpdated, TweenEnd);

            return cellView;
        }

        #endregion
    }
}
