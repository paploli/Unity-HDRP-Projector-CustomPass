using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class Projector : MonoBehaviour
{
    [Header("Camera Properties")]
    [SerializeField]private float m_nearClipPlane = 0.1f;
    public float nearClipPlane {
        get {
            if(m_nearClipPlane < 0) {
                m_nearClipPlane = 0.00001f;
            }
            if(m_farClipPlane < m_nearClipPlane) {
                m_farClipPlane = m_nearClipPlane + 0.1f;
            }
            return m_nearClipPlane;
        }
        set {
            m_nearClipPlane = value;
        }
    }
    [SerializeField] private float m_farClipPlane = 50;
    public float farClipPlane {
        get {
            if(m_farClipPlane < 0) {
                m_farClipPlane = 0.01f;
            }
            if(m_farClipPlane < m_nearClipPlane) {
                m_nearClipPlane = m_farClipPlane - 0.1f;
            }
            return m_farClipPlane;
        }
        set {
            m_farClipPlane = value;
        }
    }
    [SerializeField,Range(0.001f,179)] private float m_fieldOfView = 30;
    public float fieldOfView {
        get {
            if(m_fieldOfView < 1) {
                m_fieldOfView = 1;
            } else if(m_fieldOfView > 179) {
                m_fieldOfView = 179;
            }
            return m_fieldOfView;
        }
        set {
            m_fieldOfView = value;
        }
    }
    [SerializeField] private float m_aspectRatio = 1;
    public float aspectRatio {
        get {
            return m_aspectRatio;
        }
        set {
            m_aspectRatio = value;
        }
    }

    [Header("Projector Pass Setting")]
    public Material material = null;
    public Texture projectionImage = null;

    private void Awake() {
#if UNITY_EDITOR
        if(!material) {
            Debug.LogWarning($"{GetType()} : Material not set.", gameObject);
        }
#endif
    }

    private void Update() {
        if(!material) return;
        
        /*Projector Matrix (Perspective for Image Projector)*/
        //MV  Convert Pixels from World space to View space. (TRS = Camera(View) Position => World Position)
        //P   Calculate Custom Projection Matrix.
        //MVP MV * P.
        var world2View = Matrix4x4.TRS(transform.position, transform.rotation, new Vector3(aspectRatio, 1.0f, 1.0f)).inverse;
        var projection = Matrix4x4.Perspective(fieldOfView, 1.0f, nearClipPlane, farClipPlane);

        //https://docs.unity.cn/cn/current/ScriptReference/Matrix4x4.Perspective.html
        //返回矩阵嵌入了 z 翻转操作，其用途是取消摄像机视图矩阵执行的 z 翻转。
        //如果视图矩阵是单位矩阵或某种不执行 z 翻转的自定义矩阵，
        //请考虑将投影矩阵 的第三列（即 m02、m12、m22 和 m32）乘以 - 1。
        for (int i = 0; i < 4; i++)
        {
            projection[i, 2] = -projection[i, 2];
        }

        var projectorMatrix = projection * world2View;


        /*Projector Clip Matrix (Ortho for Image Attenuate)*/
        //P   Calculate Ortho Matrix. (Filp Z)
        //MVP MV * P.
        var orthoClipMatrix = Matrix4x4.Ortho(-1, 1, -1, 1, nearClipPlane, farClipPlane);
        orthoClipMatrix[2, 2] = -orthoClipMatrix[2, 2];
        var projectorClipMatrix = orthoClipMatrix * world2View;
        
        //Apply
        material.SetTexture("_MainTex", projectionImage);
        material.SetMatrix("_ProjectionMatrix", projectorMatrix );
        material.SetMatrix("_ProjectionClipMatrix",projectorClipMatrix);
    }

#if UNITY_EDITOR
    private void OnDrawGizmos() {
        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, new Vector3(aspectRatio, 1.0f, 1.0f));
        Gizmos.DrawFrustum(Vector3.zero, fieldOfView, farClipPlane, nearClipPlane, 1.0f);
    }
#endif
}
