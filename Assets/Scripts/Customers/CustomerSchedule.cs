using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;

public class CustomerSchedule : MonoBehaviour
{
    [SerializeField] private Transform _selectPoint;
    [SerializeField] private Vector3 _direction;

    [SerializeField] private List<SchedulePoint> _schedulePoints = new List<SchedulePoint>();

    public int Count => _schedulePoints.Count;
    
    [System.Serializable]
    private class SchedulePoint
    {
        public Vector3 Pos;
        [SerializeField] private Customer _customer;
        private Tween _moveTween;

        public Customer Customer
        {
            get
            {
                return _customer;
            }
            set
            {
                _customer = value;
                if (value != null)
                    _moveTween = _customer.transform.DOMove(Pos, 1);
            }
        }

        public void RemoveCustomer()
        {
            _moveTween.Kill();
            _customer = null;
        }

    }

    public Customer GetFirstUnit()
    {
        if (_schedulePoints.Count == 0)
            return null;

        var choosedElement = _schedulePoints.First().Customer;

        _schedulePoints.First().RemoveCustomer();
        // UpdateSchedule();
        return choosedElement;
    }

    public void AddCustomer(Customer data)
    {
        AddSchedulePoint();

        SetUnit(data);
    }

    private void AddSchedulePoint()
    {
        var position = _selectPoint.position + (_direction * _schedulePoints.Count);
        
        var point = new SchedulePoint
        {
            Pos = position
        };
        
        _schedulePoints.Add(point);
    }

    public void UpdateSchedule()
    {
        for (int i = 0; i < _schedulePoints.Count - 1; i++)
        {
            if (!_schedulePoints[i].Customer && _schedulePoints[i + 1].Customer)
            {
                var point = _schedulePoints[i + 1];
                _schedulePoints[i].Customer = point.Customer;
                point.RemoveCustomer();
            }
        }
    }

    public void SetUnit(Customer unit)
    {
        foreach (var t in _schedulePoints)
        {
            if (t.Customer) 
                continue;
            
            t.Customer = unit;
            break;
        }
    }
}
