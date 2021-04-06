using UnityEngine;

public class WorldObjectsGroupManager : MonoBehaviour
{
    [SerializeField] private GameObject world1Prefab;
    
    [SerializeField] private GameObject battle1Prefab;

    GameObject currGroup;

    public void InitWorld1()
    {
        if (currGroup != null) Destroy(currGroup);
        Instantiate(world1Prefab);
    }

    public void InitBattle1()
    {
        if (currGroup != null) Destroy(currGroup);
        Instantiate(battle1Prefab);
    }
}
