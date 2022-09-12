// using LostArkLogger.Event.Events;
//
// namespace LostArkLogger.Event;
//
// public delegate void EventConsumer<in T>(T obj) where T : Event;
//
// public class EventDispatcher
// {
//     
//     public void test()
//     {
//         Listen<SkillStartEvent>((Event) => { Console.WriteLine(Event.Id); });
//     }
//     
//     public void Listen<T>(EventConsumer<T> action) where T : Event
//     {
//         
//     }
//
//     public void Dispatch(Event e)
//     {
//         EventMap.TryGetValue(e.GetType(), out var list);
//         Console.WriteLine("Dispatching evnet: " + e.GetType().Name);
//
//         foreach (var eventMapKey in EventMap.Keys)
//         {
//             Console.WriteLine(" " + eventMapKey.Name + " registered listenrrs: " + EventMap[eventMapKey].Count);
//         }
//
//         if (list == null)
//             return;
//
//         Console.WriteLine("Start");
//         foreach (var action in list)
//         {
//             // action.Invoke(e);
//         }
//
//         Console.WriteLine("Next event?");
//     }
// }