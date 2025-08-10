using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformManager : MonoBehaviour
{
    [SerializeField] private Transform HMD;
    [SerializeField] private Transform phone;
    private PhotonView pv;
    // Start is called before the first frame update
    void Start()
    {
        pv = PhotonView.Get(this);
        if (!pv.IsMine)
            pv.RequestOwnership();
    }

    private void Update()
    {
        UpdateDeltaTransform();
    }

    private void UpdateDeltaTransform()
    {
        //transform.position = phone.position - HMD.position;
        //transform.rotation = phone.rotation * Quaternion.Inverse(HMD.rotation);

        transform.position = HMD.InverseTransformPoint(phone.position);
        transform.rotation = Quaternion.Inverse(HMD.rotation) * phone.rotation;
        //deltaRot = phone.rotation;
    }
}
