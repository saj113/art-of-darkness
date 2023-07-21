using Core.Prefabs;
using Core.Provider;
using GUIScripts.Messengers;
using Level;
using UnityEngine;

namespace Core.UnityFramework
{
    [RequireComponent(typeof(UnityUpdateEvents))]
    public class ComponentContainerInitializer : MonoBehaviour
    {
        [SerializeField]
        private SkillUseFailedMessenger _skillUseFailedMessenger;
        void Awake()
        {
            InstanceContainer.Instance.Set<ISkillUseFailedMessenger>(_skillUseFailedMessenger);
            InstanceContainer.Instance.Set<IUnityUpdateEvents>(GetComponent<UnityUpdateEvents>());
            InstanceContainer.Instance.Set<IUnitSkillPrefabProvider>(new UnitSkillPrefabProvider());
            InstanceContainer.Instance.Set<ISkillSoundPrefabProvider>(new SkillSoundPrefabProvider());
            var unitRegistry = new UnitProvider();
            InstanceContainer.Instance.Set<IUnitRegistry>(unitRegistry);
            InstanceContainer.Instance.Set<ITargetUnitProvider>(unitRegistry);


            var levelService = new LevelService();
            InstanceContainer.Instance.Set<ILevelPreferences>(levelService);
            InstanceContainer.Instance.Set<ILevelService>(levelService);
        }
    }
}
