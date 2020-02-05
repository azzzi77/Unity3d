using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Profiling;

namespace Geekbrains
{
    public sealed class BotController : BaseController, IExecute, IInitialization
    {
        private readonly int _countBot = 4;
        private readonly HashSet<Bot> _getBotList  = new HashSet<Bot>();
        private Bot BotsGen;

        public void Initialization()
        {
            for (var index = 0; index < _countBot; index++)
            {
                //todo разных противников
                if (index % 2 == 0)
                {
                   // BotsGen = ServiceLocatorMonoBehaviour.GetService<Reference>().Bot1;
                      BotsGen = Resources.Load<Bot>("Ethan");
                }
                else
                {
                    //BotsGen = ServiceLocatorMonoBehaviour.GetService<Reference>().Bot2;
                      BotsGen = Resources.Load<Bot>("Ethan2");
                }

                var tempBot = Object.Instantiate(BotsGen,
                    Patrol.GenericPoint(ServiceLocatorMonoBehaviour.GetService<CharacterController>().transform),
                    Quaternion.identity);

                tempBot.Agent.avoidancePriority = index;
                tempBot.Target = ServiceLocatorMonoBehaviour.GetService<CharacterController>().transform; 
                
                AddBotToList(tempBot);
            }
        }

        private void AddBotToList(Bot bot)
        {
            if (!_getBotList.Contains(bot))
            {
                _getBotList.Add(bot);
                bot.OnDieChange += RemoveBotToList;
            }
        }

        private void RemoveBotToList(Bot bot)
        {
            if (!_getBotList.Contains(bot))
            {
                return;
            }

            bot.OnDieChange -= RemoveBotToList;
            _getBotList.Remove(bot);
        }

        public void Execute()
        {
            if (!IsActive)
            {
                return;
            }

            Profiler.BeginSample("CheckBots");

            for (var i = 0; i < _getBotList.Count; i++)
            {
                var bot = _getBotList.ElementAt(i);
                bot.Tick();
            }

            Profiler.EndSample();

        }
    }
}
