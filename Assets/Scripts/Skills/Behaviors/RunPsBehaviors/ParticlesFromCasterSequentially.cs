namespace Skills.Behaviors.RunPsBehaviors
{
    public class ParticlesFromCasterSequentially 
    {
        //private readonly IDictionary<IStats, float> _elapsedTimeByTarget = 
        //    new Dictionary<IStats, float>();

        //private readonly IList<IStats> _affectedTargets = new List<IStats>(); 

        //protected override void ForwardCollisionCore(OnTrigger sender, IStats target, Vector3 pos)
        //{
        //    float targetTimeElapsed;
        //    if (_elapsedTimeByTarget.TryGetValue(target, out targetTimeElapsed))
        //    {
        //        if (Parameters.AffectEverySeconds < 1 || !IsTargetTimeElapsedMaximum(targetTimeElapsed))
        //        {
        //            return;
        //        }

        //        _elapsedTimeByTarget[target] = 0.0f;
        //    }
        //    else
        //    {
        //        _elapsedTimeByTarget.Add(target, 0.0f);
        //        _affectedTargets.Add(target);
        //    }

        //    InstantiateCollisionParticles(pos);
        //    ExecuteSkillModificators(target);
        //}

        //protected override void UpdateVirtual()
        //{
        //    foreach (var affectedTarget in _affectedTargets)
        //    {
        //        _elapsedTimeByTarget[affectedTarget] += Time.deltaTime;
        //    }
        //}

        //private bool IsTargetTimeElapsedMaximum(float timeElapsed)
        //{
        //    return timeElapsed >= Parameters.AffectEverySeconds;
        //}
    }
}
