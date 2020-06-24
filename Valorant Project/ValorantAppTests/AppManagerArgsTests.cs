using BussinessLayer;
using NUnit.Framework;

namespace ValorantAppTests
{
    public class AppManagerArgsTests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void CreateArgsClass()
        {
            AgentArgs agentArgs = new AgentArgs(
                "Reyna",
                1,
                "Leer",
                "Equip an ethereal, destructible eye. Activate to cast the eye a short distance forward. The eye will nearsight all enemies who look at it.",
                "Empress",
                "Instantly enter a frenzy, increasing firing, equip and reload speed dramatically. Gain infinite charges of Soul Harvest abilities. Scoring a kill renews the duration.",
                "Devour",
                "Enemies killed by Reyna leave behind Soul Orbs that last 3 seconds. Instantly consume a nearby soul orb, rapidly healing for a short duration. Health gained through this skill exceeding 100 will decay over time. If Empress is active, this skill will automatically cast and not consume the Soul Orb.",
                "Dismiss",
                "Instantly consume a nearby Soul Orb, becoming instangible for a short duration. If Empress is active, also become invisible.",
                "Reyna is a VALORANT agent with an aggressive, duel-centric playstyle. Reyna's unique mechanic are the so-called Soul Orbs, which drop when she kills an opponent and which, upon consumption, give her various bonuses — from massive healing to invisibility. Reyna's ultimate ability is Empress, which turns her into a rapid-fire death machine, dramatically increasing all her combat stats. Every kill renews Empress' duration, and a skilled Reyna player can wipe away the entire team in one swift offense.");

            Assert.That(agentArgs.Name, Is.EqualTo("Reyna"));
            Assert.That(agentArgs.TypeID, Is.EqualTo(1));
            Assert.That(agentArgs.SignatureAbilityName, Is.EqualTo("Leer"));
            Assert.That(agentArgs.SignatureAbilityDiscription, Is.EqualTo("Equip an ethereal, destructible eye. Activate to cast the eye a short distance forward. The eye will nearsight all enemies who look at it."));
            Assert.That(agentArgs.UltamateAbilityName, Is.EqualTo("Empress"));
            Assert.That(agentArgs.UltamateAbilityDiscription, Is.EqualTo("Instantly enter a frenzy, increasing firing, equip and reload speed dramatically. Gain infinite charges of Soul Harvest abilities. Scoring a kill renews the duration."));
            Assert.That(agentArgs.AbilityOneName, Is.EqualTo("Devour"));
            Assert.That(agentArgs.AbilityOneDiscription, Is.EqualTo("Enemies killed by Reyna leave behind Soul Orbs that last 3 seconds. Instantly consume a nearby soul orb, rapidly healing for a short duration. Health gained through this skill exceeding 100 will decay over time. If Empress is active, this skill will automatically cast and not consume the Soul Orb."));
            Assert.That(agentArgs.AbilityTwoName, Is.EqualTo("Dismiss"));
            Assert.That(agentArgs.AbilityTwoDiscription, Is.EqualTo("Instantly consume a nearby Soul Orb, becoming instangible for a short duration. If Empress is active, also become invisible."));
            Assert.That(agentArgs.Bio, Is.EqualTo("Reyna is a VALORANT agent with an aggressive, duel-centric playstyle. Reyna's unique mechanic are the so-called Soul Orbs, which drop when she kills an opponent and which, upon consumption, give her various bonuses — from massive healing to invisibility. Reyna's ultimate ability is Empress, which turns her into a rapid-fire death machine, dramatically increasing all her combat stats. Every kill renews Empress' duration, and a skilled Reyna player can wipe away the entire team in one swift offense."));
        }

        //[Test]
        //public void CreateArgsClassWithNullName()
        //{
        //   var result  = new AgentManagerArgs(
        //        null,
        //        1,
        //        "Leer",
        //        "Equip an ethereal, destructible eye. Activate to cast the eye a short distance forward. The eye will nearsight all enemies who look at it.",
        //        "Empress",
        //        "Instantly enter a frenzy, increasing firing, equip and reload speed dramatically. Gain infinite charges of Soul Harvest abilities. Scoring a kill renews the duration.",
        //        "Devour",
        //        "Enemies killed by Reyna leave behind Soul Orbs that last 3 seconds. Instantly consume a nearby soul orb, rapidly healing for a short duration. Health gained through this skill exceeding 100 will decay over time. If Empress is active, this skill will automatically cast and not consume the Soul Orb.",
        //        "Dismiss",
        //        "Instantly consume a nearby Soul Orb, becoming instangible for a short duration. If Empress is active, also become invisible.",
        //        "Reyna is a VALORANT agent with an aggressive, duel-centric playstyle. Reyna's unique mechanic are the so-called Soul Orbs, which drop when she kills an opponent and which, upon consumption, give her various bonuses — from massive healing to invisibility. Reyna's ultimate ability is Empress, which turns her into a rapid-fire death machine, dramatically increasing all her combat stats. Every kill renews Empress' duration, and a skilled Reyna player can wipe away the entire team in one swift offense.");

        //    Assert.Throws()
        //}
    }
}
