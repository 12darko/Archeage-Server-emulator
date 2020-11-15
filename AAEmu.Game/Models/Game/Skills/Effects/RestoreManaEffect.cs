﻿using System;
using AAEmu.Commons.Utils;
using AAEmu.Game.Core.Packets.G2C;
using AAEmu.Game.Models.Game.Skills.Templates;
using AAEmu.Game.Models.Game.Units;

namespace AAEmu.Game.Models.Game.Skills.Effects
{
    public class RestoreManaEffect : EffectTemplate
    {
        public bool UseFixedValue { get; set; }
        public int FixedMin { get; set; }
        public int FixedMax { get; set; }
        public bool UseLevelValue { get; set; }
        public float LevelMd { get; set; }
        public int LevelVaStart { get; set; }
        public int LevelVaEnd { get; set; }
        public bool Percent { get; set; }

        public override bool OnActionTime => false;

        public override void Apply(Unit caster, SkillCaster casterObj, BaseUnit target, SkillCastTarget targetObj, CastAction castObj,
            Skill skill, SkillObject skillObject, DateTime time)
        {
            _log.Debug("RestoreManaEffect");

            if (!(target is Unit))
                return;
            var trg = (Unit)target;
            var min = 0;
            var max = 0;
            if (UseFixedValue)
            {
                min += FixedMin;
                max += FixedMax;
            }

            var unk = 0f;
            var unk2 = 1f;
            var skillLevel = 1;
            if (skill != null)
            {
                skillLevel = (skill.Level - 1) * skill.Template.LevelStep + skill.Template.AbilityLevel;
                if (skillLevel >= skill.Template.AbilityLevel)
                    unk = 0.15f * (skillLevel - skill.Template.AbilityLevel + 1);
                unk2 = (1 + unk) * 1.3f;
            }

            if (UseLevelValue)
            {
                var levelMd = (unk + 1) * LevelMd;
                min += (int)(caster.LevelDps * levelMd + 0.5f);
                max += (int)((((skillLevel - 1) * 0.020408163f * (LevelVaEnd - LevelVaStart) + LevelVaStart) * 0.0099999998f + 1f) *
                             caster.LevelDps * levelMd + 0.5f);
            }

            // TODO ...
            // min += (int)((caster.MDps + caster.MDpsInc) * 0.001f * unk2 + 0.5f);
            // max += (int)((caster.MDps + caster.MDpsInc) * 0.001f * unk2 + 0.5f);

            var value = Rand.Next(min, max);
            trg.BroadcastPacket(new SCUnitHealedPacket(castObj, casterObj, trg.ObjId, 1, value), true);
            trg.Mp += value;
            trg.Mp = Math.Min(trg.Mp, trg.MaxMp);
            trg.BroadcastPacket(new SCUnitPointsPacket(trg.ObjId, trg.Hp, trg.Mp, trg.HighAbilityRsc), true);
        }
    }
}