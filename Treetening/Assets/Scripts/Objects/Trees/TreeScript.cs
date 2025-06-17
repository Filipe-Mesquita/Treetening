using Unity.Collections;
using UnityEngine;
using UnityEngine.Animations;

public class TreeScript : MonoBehaviour
{
    [Header("Params")]
    [SerializeField] private float hp;
    [SerializeField] private float rootValue;
    [SerializeField] private string treeName;
    private bool fallenTree;

    void Start()
    {
        FreezeTree();
        fallenTree = false;
    }

    void FreezeTree()
    {
        Rigidbody treeRB = GetComponent<Rigidbody>();
        if (treeRB != null)
            treeRB.freezeRotation = true;
        else
            Debug.LogWarning("Unable to reference root's RB to freeze.");
    }

    public void UnfreezeTree()
    {
        Rigidbody treeRB = GetComponent<Rigidbody>();
        if (treeRB != null)
            treeRB.freezeRotation = false;
        else
            Debug.LogWarning("Unable to reference root's RB to freeze.");
    }

    public bool getFallenTree()
    {
        return fallenTree;
    }

    public void setFallenTree(bool fallenTree)
    {
        this.fallenTree = fallenTree;
    }

    public bool takeDamage(float dmg)
    {
        hp = hp - dmg;
        Debug.Log($"Tree hp = {hp}");

        if (hp <= 0f)
        {
            UnfreezeTree();
            return true;
        }
        else
        {
            return false;
        }
    }

    public float getRootValue()
    {
        return rootValue;
    }
}
