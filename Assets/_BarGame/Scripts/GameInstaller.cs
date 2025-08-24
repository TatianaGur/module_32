using VContainer;
using VContainer.Unity;
using UnityEngine;

public class GameInstaller : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        base.Configure(builder);
        builder.RegisterComponentInHierarchy<InDanceFloorTrigger>();
    }
}