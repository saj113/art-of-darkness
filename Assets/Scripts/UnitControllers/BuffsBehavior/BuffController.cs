using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using Core.UnityFramework;
using Skills.Modificators.Buffs;

namespace UnitControllers.BuffsBehavior
{
    public class BuffController : IBuffController
    {
        private readonly HashSet<IBuff> _buffCollection = new HashSet<IBuff>();
        private readonly IList<IBuff> _buffsForDelete = new List<IBuff>();
        private readonly IUnityUpdateEvents _unityUpdateEvents;

        public BuffController(IUnityUpdateEvents unityUpdateEvents)
        {
            _unityUpdateEvents = unityUpdateEvents;
            _unityUpdateEvents.FixedUpdateFired += Update;
        }

        public event Action<IBuffController, IBuff> BuffAdded;

        public void AddBuff(IBuff newBuff)
        {
            var existUniqueBuff = GetBuffByUnique(newBuff.UniqueCode);
            if (existUniqueBuff != null)
            {
                RemoveBuff(existUniqueBuff);
            }

            _buffCollection.Add(newBuff);
            OnBuffAdded(newBuff);
            newBuff.Apply();
        }

        public void RemoveBuff(IBuff buff)
        {
            if (_buffCollection.Contains(buff))
            {
                _buffCollection.Remove(buff);
                buff.Reset();
            }
        }

        public void RemoveAll()
        {
            foreach (var buff in _buffCollection)
            {
                buff.Reset();
            }

            _buffCollection.Clear();
        }

        private void Update(float deltaTime)
        {
            foreach (var buff in _buffCollection)
            {
                if (buff.IsBuffCanDelete())
                {
                    _buffsForDelete.Add(buff);
                }
                else
                {
                    buff.UpdateBuff();
                }
            }

            foreach (var buff in _buffsForDelete)
            {
                RemoveBuff(buff);
            }

            _buffsForDelete.Clear();
        }

        private IBuff GetBuffByUnique(BuffUniqueCode uniqueCode)
        {
            return _buffCollection.FirstOrDefault(p =>
                p.UniqueCode != BuffUniqueCode.NoUnique
                && p.UniqueCode == uniqueCode);
        }

        protected virtual void OnBuffAdded(IBuff buff)
        {
            var handler = BuffAdded;
            if (handler != null)
            {
                handler(this, buff);
            }
        }

        public void Dispose()
        {
            _unityUpdateEvents.FixedUpdateFired -= Update;
        }
    }
}
