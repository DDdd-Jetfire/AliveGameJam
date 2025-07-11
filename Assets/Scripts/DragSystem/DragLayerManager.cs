using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DragLayerManager : MonoBehaviour
{
    #region Instance

    

    
    // 单例实例引用
    private static DragLayerManager _instance;
    
    // 全局访问点
    public static DragLayerManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<DragLayerManager>();
                
                // 若场景中没有实例，则创建新实例
                if (_instance == null)
                {
                    GameObject obj = new GameObject("DragLayerManager");
                    _instance = obj.AddComponent<DragLayerManager>();
                }
            }
            return _instance;
        }
    }
    
    // 防止外部直接创建实例
    private DragLayerManager() { }
    
    // 初始化时检查重复实例
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
        // foreach (var VARIABLE in GameObject.FindObjectsOfType<DragAndLimit>())
        // {
        //     _dragObjects.Add(VARIABLE.gameObject);
        // }
    }
    
    #endregion
    private List<GameObject> _dragObjects = new List<GameObject>();

    public void RegisterDragObject(GameObject dragObject)
    {
        _dragObjects.Add(dragObject);
    }

    public void UnregisterDragObject(GameObject dragObject)
    {
        _dragObjects.Remove(dragObject);
    }

    private void SortLayers()
    {
        List<SpriteRenderer> renderers = _dragObjects.Where(obj => obj != null).Select(obj => obj.GetComponent<SpriteRenderer>()).ToList();
        for (int i = 0; i < renderers.Count; i++)
        {
            renderers[i].sortingOrder = i;
        }
    }

    public void UpChooseDragObject(GameObject dragObject)
    {
        if (_dragObjects.Contains(dragObject))
        {
            _dragObjects.Remove(dragObject);
            _dragObjects.Add(dragObject);
            SortLayers();
        }
    }
    
}