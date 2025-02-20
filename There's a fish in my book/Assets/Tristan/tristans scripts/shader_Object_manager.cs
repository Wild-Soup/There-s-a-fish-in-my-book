using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shader_Object_manager : MonoBehaviour
{
    private Renderer _renderer;
    private MaterialPropertyBlock _propertyBlock;

    public string targetTag = "hand";
    public float range=2f;
    public float thicknessModifer = 10f;
    public float maxthickness = 0.025f;

    // Start is called before the first frame update
    void Start()
    {
        _renderer = GetComponent<MeshRenderer>();
        _propertyBlock = new MaterialPropertyBlock();

        _renderer.GetPropertyBlock(_propertyBlock);
        _propertyBlock.SetFloat("_Outline_thickness", 0f);
    }

    // Update is called once per frame
    void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, range);

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag(targetTag)) 
            {
                Debug.Log("in range");
                float distance = Vector3.Distance(transform.position, collider.transform.position);
                _propertyBlock.SetFloat("_Outline_thickness", Mathf.Clamp((range - distance) / thicknessModifer,0, maxthickness));
            }

        }
        _renderer.SetPropertyBlock(_propertyBlock);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
