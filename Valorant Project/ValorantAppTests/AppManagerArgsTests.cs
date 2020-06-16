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
            AgentManagerArgs agentArgs = new AgentManagerArgs(
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

            Assert.AreEqual(agentArgs.Name, "Reyna");
            Assert.AreEqual(agentArgs.TypeID, 1);
            Assert.AreEqual(agentArgs.SignatureAbilityName, "Leer");
            Assert.AreEqual(agentArgs.SignatureAbilityDiscription, "Equip an ethereal, destructible eye. Activate to cast the eye a short distance forward. The eye will nearsight all enemies who look at it.");
            Assert.AreEqual(agentArgs.UltamateAbilityName, "Empress");
            Assert.AreEqual(agentArgs.UltamateAbilityDiscription, "Instantly enter a frenzy, increasing firing, equip and reload speed dramatically. Gain infinite charges of Soul Harvest abilities. Scoring a kill renews the duration.");
            Assert.AreEqual(agentArgs.AbilityOneName, "Devour");
            Assert.AreEqual(agentArgs.AbilityOneDiscription, "Enemies killed by Reyna leave behind Soul Orbs that last 3 seconds. Instantly consume a nearby soul orb, rapidly healing for a short duration. Health gained through this skill exceeding 100 will decay over time. If Empress is active, this skill will automatically cast and not consume the Soul Orb.");
            Assert.AreEqual(agentArgs.AbilityTwoName, "Dismiss");
            Assert.AreEqual(agentArgs.AbilityTwoDiscription, "Instantly consume a nearby Soul Orb, becoming instangible for a short duration. If Empress is active, also become invisible.");
            Assert.AreEqual(agentArgs.Bio, "Reyna is a VALORANT agent with an aggressive, duel-centric playstyle. Reyna's unique mechanic are the so-called Soul Orbs, which drop when she kills an opponent and which, upon consumption, give her various bonuses — from massive healing to invisibility. Reyna's ultimate ability is Empress, which turns her into a rapid-fire death machine, dramatically increasing all her combat stats. Every kill renews Empress' duration, and a skilled Reyna player can wipe away the entire team in one swift offense.");
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
