﻿using System;
using ValorantDatabase;

namespace BussinessLayer.Args
{
    public class AgentArgs : SuperArgs
    {
        public string Name { get; private set; }
        public int TypeID { get; private set; }
        public string SignatureAbilityName { get; private set; }
        public string SignatureAbilityDiscription { get; private set; }
        public string UltamateAbilityName { get; private set; }
        public string UltamateAbilityDiscription { get; private set; }
        public string AbilityOneName { get; private set; }
        public string AbilityOneDiscription { get; private set; }
        public string AbilityTwoName { get; private set; }
        public string AbilityTwoDiscription { get; private set; }
        public string Bio { get; private set; }

        public AgentArgs(string name, int typeID, string signatureAbilityName, string signatureAbilityDiscription, string ultamateAbilityName, string ultamateAbilityDiscription, string abilityOneName, string abilityOneDiscription, string abilityTwoName, string abilityTwoDiscription, string bio)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            TypeID = typeID;
            SignatureAbilityName = signatureAbilityName;
            SignatureAbilityDiscription = signatureAbilityDiscription;
            UltamateAbilityName = ultamateAbilityName;
            UltamateAbilityDiscription = ultamateAbilityDiscription;
            AbilityOneName = abilityOneName;
            AbilityOneDiscription = abilityOneDiscription;
            AbilityTwoName = abilityTwoName;
            AbilityTwoDiscription = abilityTwoDiscription;
            Bio = bio;
        }

        public AgentArgs(string name, object typeObj, string signatureAbilityName, string signatureAbilityDiscription, string ultamateAbilityName, string ultamateAbilityDiscription, string abilityOneName, string abilityOneDiscription, string abilityTwoName, string abilityTwoDiscription, string bio)
        {
            AgentType type = (AgentType)typeObj;

            Name = name ?? throw new ArgumentNullException(nameof(name));
            TypeID = type.TypeId;
            SignatureAbilityName = signatureAbilityName;
            SignatureAbilityDiscription = signatureAbilityDiscription;
            UltamateAbilityName = ultamateAbilityName;
            UltamateAbilityDiscription = ultamateAbilityDiscription;
            AbilityOneName = abilityOneName;
            AbilityOneDiscription = abilityOneDiscription;
            AbilityTwoName = abilityTwoName;
            AbilityTwoDiscription = abilityTwoDiscription;
            Bio = bio;
        }
    }
}