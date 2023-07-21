using Core;

namespace Skills.Modificators.Buffs
{
    public interface IBuff
    {
        BuffUniqueCode UniqueCode { get; }
        void Apply();
        bool IsBuffCanDelete();
        void UpdateBuff();
        void Reset();
        bool CanBeApplied();
    }
}
