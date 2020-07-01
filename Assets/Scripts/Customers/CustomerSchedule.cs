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
    private struct SchedulePoint
    {
        public Vector3 PointPosition;
        private Tween _moveTween;

        public Customer Customer;
        // {
            // get
            // {
            //     return _customer;
            // }
            // set
            // {
            //     _customer = value;
            //     if (value != null)
            //         _moveTween = _customer.transform.DOMove(Pos, 1);
            // }
        // }

        // public void RemoveCustomer()
        // {
        //     _moveTween.Kill();
        //     Customer = null;
        // }

    }

    public Customer GetFirstUnit()
    {
        if (_schedulePoints.Count == 0)
            return null;

        var choosedElement = _schedulePoints.First().Customer;

        _schedulePoints.RemoveAt(0);
        
        UpdateSchedule();

        return choosedElement;
    }

    public void AddCustomer(Customer data)
    {
        AddSchedulePoint(data);
    }

    private void AddSchedulePoint(Customer data)
    {
        var position = GetVacantPosition();
        
        var point = new SchedulePoint
        {
            PointPosition = position,
            Customer = data
        };
        
        data.transform.position = point.PointPosition;
        
        _schedulePoints.Add(point);
    }

    private Vector3 GetVacantPosition()
    {
        return _selectPoint.position + (_direction * _schedulePoints.Count);
    }

    public void UpdateSchedule()
    {
        List<SchedulePoint> cache = new List<SchedulePoint>(_schedulePoints) ;

        _schedulePoints.Clear();
        
        for (int i = 0; i < cache.Count; i++)
        {
            SchedulePoint point = new SchedulePoint()
            {
               PointPosition = GetVacantPosition(),
               Customer = cache[i].Customer
            };

            point.Customer.MoveTo(point.PointPosition, point.Customer.transform.rotation.eulerAngles, 1);

            
            _schedulePoints.Add(point);
        }
    }

    public void SetUnit(Customer unit)
    {
        for (int i = 0; i < _schedulePoints.Count; i++)
        {
            if(_schedulePoints[i].Customer)
                continue;
            
            // _schedulePoints[i].SetCustomer(unit);
            break;
        }
        
        // foreach (var t in _schedulePoints)
        // {
        //     if (t.Customer != null) 
        //         continue;
        //     
        //     t.SetCustomer(unit);
        //     break;
        // }
    }
}
