using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Cyberevolver.Unity
{
    public static class EventTriggerExtension
    {
        /// <summary>
        /// Just adding event action. Unity has not as simple method as this.
        /// </summary>
        /// <param name="eventTrigger"></param>
        /// <param name="typeAction"></param>
        /// <param name="action"></param>
        public static void Add(this EventTrigger eventTrigger, EventTriggerType typeAction, UnityAction<BaseEventData> action)
        {
            EventTrigger.Entry entry = new EventTrigger.Entry
            {
                eventID = typeAction
            };
            entry.callback.AddListener(action);
            eventTrigger.triggers.Add(entry);
        }
    }
}

