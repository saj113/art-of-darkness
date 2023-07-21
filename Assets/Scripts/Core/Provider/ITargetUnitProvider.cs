using System.Collections.Generic;
using Skills;
using Stats;
using UnityEngine;

namespace Core.Provider
{
    public interface ITargetUnitProvider
    {
        IEnumerable<IStats> GetAll();
        IEnumerable<IStats> Get(Tag tag);
        IStats Get(Tag tag, Vector2 position);
        IEnumerable<IStats> Get(Tag finder, TargetUnitRelation relation);
        IEnumerable<IStats> Get(ICharacteristics finder,
                                TargetUnitRelation relation,
                                Bounds startPoint,
                                Direction direction,
                                float distance);
        IEnumerable<IStats> Get(Tag finder,
                                TargetUnitRelation relation,
                                Bounds startPoint,
                                int direction,
                                float distance);
        IEnumerable<IStats> Get(Tag finder,
            TargetUnitRelation relation,
            float x,
            int direction,
            float distance);
    }
}
