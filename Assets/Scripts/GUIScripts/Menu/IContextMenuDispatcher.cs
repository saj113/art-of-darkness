using System;

namespace GUIScripts.Menu
{
    public interface IContextMenuDispatcher
    {
         event Action Back;
         event Action CloseAll;
    }
}