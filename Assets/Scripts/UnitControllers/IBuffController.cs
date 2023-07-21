using System;
using Skills.Modificators.Buffs;

namespace UnitControllers
{
    public interface IBuffController : IDisposable
    {
        event Action<IBuffController, IBuff> BuffAdded;
        void AddBuff(IBuff newBuff);
        void RemoveAll();
        void RemoveBuff(IBuff buff);
    }
}