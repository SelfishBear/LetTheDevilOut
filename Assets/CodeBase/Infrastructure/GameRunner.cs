using UnityEngine;

namespace CodeBase.Infrastructure
{
  public class GameRunner : MonoBehaviour
  {
    public GameBootstrapper BootstrapperPrefab;
    private void Awake()
    {
      GameBootstrapper bootstrapper = FindFirstObjectByType<GameBootstrapper>();
      
      if(bootstrapper != null) return;

       Instantiate(BootstrapperPrefab);
    }
  }
}