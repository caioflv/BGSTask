using UnityEngine;

namespace BGSTask
{
    //All controllers essential for the game to function derive from this class.
    //From there we can order the initialization of each controller.
    
    public class Controller : MonoBehaviour
    {
        public virtual void Init() { }
    }
}

