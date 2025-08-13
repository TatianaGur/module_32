//using VContainer;
//using VContainer.Unity;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);
        Container.DeclareSignal<TargetsArrayChangedSignal>();
    }










    /*: LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        base.Configure(builder);
        builder.RegisterComponentInHierarchy<InDanceFloorTrigger>();
    }*/
}
