using UnityEngine;
using UnityEngine.AI;

namespace Geekbrains
{
    
    public static class Patrol
    {
        const int RNDSTART = 5;
        const int RNDSTOP = 40;

        public static Vector3 GenericPoint(Transform agent)
        {
            //todo перемещение по точкам
            Vector3 result;

            var dis = Random.Range(RNDSTART, RNDSTOP);
            var randomPoint = Random.insideUnitSphere * dis;

            NavMesh.SamplePosition(agent.position + randomPoint, 
                out var hit, dis, NavMesh.AllAreas);
         
            result = hit.position;

            return result;
        }
    }
}
