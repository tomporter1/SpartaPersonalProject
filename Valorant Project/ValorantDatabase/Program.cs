using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace ValorantDatabase
{
    class Program
    {
        static void Main(string[] args)
        {
            //AgentType agentType = new AgentType()
            //{
            //    TypeName = "Duelist"
            //};
            //Agents newAgent = new Agents()
            //{
            //    AgentType = agentType,
            //    AgentName = "Jet"
            //};


            //using (ValorantContext db = new ValorantContext())
            //{
            //    Agents selectedAgent = db.Agents.Where(a => a.AgentName == "Jet").FirstOrDefault();
            //    if (selectedAgent == null)
            //    {
            //        db.AgentType.Add(agentType);
            //        db.Agents.Add(newAgent);
            //        db.SaveChanges();
            //    }
            //}

            //using (ValorantContext db = new ValorantContext())
            //{
            //    foreach (Agents agent in db.Agents.Include(a => a.AgentType))
            //        Console.WriteLine($" {agent} is a {agent.AgentType}");
            //}
        }
    }
}
