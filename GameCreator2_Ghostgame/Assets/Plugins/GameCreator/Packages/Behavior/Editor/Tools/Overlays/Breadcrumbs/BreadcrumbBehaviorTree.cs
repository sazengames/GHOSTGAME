using UnityEditor.Overlays;

namespace GameCreator.Editor.Behavior
{
    [Overlay(
        id = ID,
        displayName = NAME,
        editorWindowType = typeof(WindowBehaviorTree),
        defaultDisplay = true,
        defaultDockZone = DockZone.BottomToolbar,
        defaultDockPosition = DockPosition.Top,
        defaultDockIndex = 1,
        defaultLayout = Layout.HorizontalToolbar
    )]
    
    internal class BreadcrumbBehaviorTree : TBreadcrumb
    { }
}