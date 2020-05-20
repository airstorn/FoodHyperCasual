using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICookable
{
   void Cook(float cookState);
   float GetReadiness();
   float GetTotalDuration();
   Transform GetTransform();
}
