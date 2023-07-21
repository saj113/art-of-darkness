namespace GUIScripts.Menu.Talent
{
    public interface ITalentInfo : ISkillInfo
    {
        ISkillInfo ExtensionLeft1 {get;}
        ISkillInfo ExtensionLeft2 {get;}
        ISkillInfo ExtensionRight1 {get;}
        ISkillInfo ExtensionRight2 {get;}
        ISkillInfo BonusExtension { get; }
    }
}