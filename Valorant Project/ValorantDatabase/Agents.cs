using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ValorantDatabase
{
    public partial class Agents
    {
        public Agents()
        {
            GameLogs = new HashSet<GameLogs>();
        }
        private int _agentId;
        private string _agentName;
        private int _agentTypeId;
        private string _signatureAbilityName;
        private string _signatureAbilityDiscription;
        private string _ultamateAbilityName;
        private string _ultamateAbilityDiscription;
        private string _abilityOneName;
        private string _abilityOneDiscription;
        private string _abilityTwoName;
        private string _abilityTwoDiscription;
        private string _bio;
        private string _imagePath;


        public int AgentId { get { return _agentId; } set { _agentId = value; } }
        public string AgentName
        {
            get { return _agentName; }
            set { _agentName = value.Length <= 20 ? value : value.Substring(0, 20); }
        }
        public int AgentTypeId { get { return _agentTypeId; } set { _agentTypeId = value; } }
        public string SignatureAbilityName
        {
            get { return _signatureAbilityName; }
            set { _signatureAbilityName = value.Length <= 30 ? value : value.Substring(0, 30); }
        }
        public string SignatureAbilityDiscription
        {
            get { return _signatureAbilityDiscription; }
            set { _signatureAbilityDiscription = value.Length <= 500 ? value : value.Substring(0, 500); }
        }
        public string UltamateAbilityName
        {
            get { return _ultamateAbilityName; }
            set { _ultamateAbilityName = value.Length <= 30 ? value : value.Substring(0, 30); }
        }
        public string UltamateAbilityDiscription
        {
            get { return _ultamateAbilityDiscription; }
            set { _ultamateAbilityDiscription = value.Length <= 500 ? value : value.Substring(0, 500); }
        }
        public string AbilityOneName
        {
            get { return _abilityOneName; }
            set { _abilityOneName = value.Length <= 30 ? value : value.Substring(0, 30); }
        }
        public string AbilityOneDiscription
        {
            get { return _abilityOneDiscription; }
            set { _abilityOneDiscription = value.Length <= 500 ? value : value.Substring(0, 500); }
        }
        public string AbilityTwoName
        {
            get { return _abilityTwoName; }
            set { _abilityTwoName = value.Length <= 30 ? value : value.Substring(0, 30); }
        }
        public string AbilityTwoDiscription
        {
            get { return _abilityTwoDiscription; }
            set { _abilityTwoDiscription = value.Length <= 500 ? value : value.Substring(0, 500); }
        }
        public string Bio
        {
            get { return _bio; }
            set { _bio = value.Length <= 700 ? value : value.Substring(0, 700); }
        }

        public string ImagePath
        {
            get { return _imagePath; }
            set { _imagePath = value.Length <= 35 ? value : value.Substring(0, 35); }
        }

        public virtual AgentType AgentType { get; set; }
        public virtual ICollection<GameLogs> GameLogs { get; set; }
    }
}
