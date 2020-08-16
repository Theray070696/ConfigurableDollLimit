using BepInEx;
using BepInEx.Configuration;
using R2API;
using RoR2;
using CharacterMaster = On.RoR2.CharacterMaster; 

namespace Theray070696
{
    [BepInPlugin("io.github.Theray070696.configurabledolllimit", "Configurable Doll Limit", "1.0.0")]
    [BepInDependency("com.bepis.r2api", BepInDependency.DependencyFlags.HardDependency)]
    public class ConfigurableDollLimit : BaseUnityPlugin
    {
        private int DollLimit = 3;
        
        public void Awake()
        {
            ConfigEntry<int> c = Config.Bind("Doll Count", "dollCount", DollLimit, "What should the maximum amount of deployed dolls be?");
            DollLimit = c.Value;
            
            CharacterMaster.GetDeployableSameSlotLimit += OnGetDeployableSameSlotLimit;
        }

        private int OnGetDeployableSameSlotLimit(CharacterMaster.orig_GetDeployableSameSlotLimit orig, RoR2.CharacterMaster self, DeployableSlot slot)
        {
            if(slot != DeployableSlot.DeathProjectile)
            {
                return orig.Invoke(self, slot);
            }

            return DollLimit;
        }
    }
}