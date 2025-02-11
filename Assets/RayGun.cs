using Meta.WitAi;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayGun : MonoBehaviour
{
    public LayerMask LayerMask;
    public OVRInput.RawButton shootingButton;
    public LineRenderer linePrefab;
    public GameObject RayImpactPrefab;
    public Transform shootingPoint;
    public float maxLineDistance = 5;
    public float lineShowTimer = 0.3f;
    public AudioSource source;
    public AudioClip shootingAudioclip;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetDown(shootingButton))
        {
            Shoot();
        }
    }

    public void Shoot() 
    {
        source.PlayOneShot(shootingAudioclip);

        //tambahan code
        //Instantiate(linePrefab, shootingPoint.position, shootingPoint.rotation);

        // Debug.Log("nembak boss");
        Ray ray = new Ray(shootingPoint.position,shootingPoint.forward);
        bool hasHit = Physics.Raycast(ray,out RaycastHit hit, maxLineDistance,LayerMask);

        Vector3 endPoint = Vector3.zero;

        if (hasHit)
        {
            //stop
            endPoint = hit.point;
            //Debug.Log("ketembak");

            Mummy mummy = hit.transform.GetComponentInParent<Mummy>();

            if (mummy)
            {
               // mummy.Kill();
                mummy.Destory();
            }
            else {
                Quaternion rayImpactRotation = Quaternion.LookRotation(-hit.normal);

                GameObject rayImpact = Instantiate(RayImpactPrefab, hit.point, rayImpactRotation);

                Destroy(rayImpact, 1);
            }
            

        }
        else
        {
           endPoint = shootingPoint.position + shootingPoint.forward * maxLineDistance;

           
        }


        LineRenderer line = Instantiate(linePrefab);
        line.positionCount = 2;
        line.SetPosition(0,shootingPoint.position);

  
        
        line.SetPosition(1,endPoint);
        Destroy(line.gameObject,lineShowTimer);
    }
}
