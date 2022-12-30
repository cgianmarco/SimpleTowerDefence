using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using static UnityEngine.GraphicsBuffer;



//public abstract class TargetState
//{
//    public virtual void Update() { }
//}
//public class TargetAcquiredState : TargetState
//{
//    Aim aim;
//    public TargetAcquiredState(Aim aim)
//    {
//        this.aim = aim;
//    }



//    public override void Update()
//    {
//        Vector3 direction = (aim.target.transform.position - aim.cannon.transform.position).normalized;
//        Quaternion startAngle = aim.cannon.transform.rotation;
//        Quaternion destinationAngle = Quaternion.LookRotation(direction);

//        if (Quaternion.Angle(startAngle, destinationAngle) > 30)
//            aim.cannon.rotation = Quaternion.Slerp(startAngle, destinationAngle, aim.turnSpeed * Time.deltaTime);
//        else
//            aim.cannon.LookAt(aim.target.transform);
//    }

//}

//public class NoTargetState : TargetState
//{
    
//}



public class Aim : MonoBehaviour
{

    private Transform cannon;
    private Unit target;

    public event Action OnTargetAcquired;
    public event Action OnTargetLost;


    [SerializeField] float updateTime = 0.1f;

    private float turnSpeed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        cannon = GetComponentInChildren<Cannon>().transform;

        StartCoroutine(FindTarget());
    }

    // Update is called once per frame
    void Update()
    {
        if (target is null) return;

        Vector3 direction = (target.transform.position - cannon.transform.position).normalized;
        Quaternion startAngle = cannon.transform.rotation;
        Quaternion destinationAngle = Quaternion.LookRotation(direction);

        if (Quaternion.Angle(startAngle, destinationAngle) > 30)
            cannon.rotation = Quaternion.Slerp(startAngle, destinationAngle, turnSpeed * Time.deltaTime);
        else
            cannon.LookAt(target.transform);

    }


    void onTargetDestroyed(Unit unit)
    {
        OnTargetLost?.Invoke();
        target = null;
    }

    IEnumerator FindTarget()
    {
        while (true)
        {
            target = UnitManager.Instance.FindClosest<Enemy>(GetComponent<Turret>());

            if (target is not null)
            {
                OnTargetAcquired?.Invoke();
                target.onDestroyed += onTargetDestroyed;
            }


            yield return new WaitForSeconds(0.1f);
        }


    }

}
