using LaserAssetPackage.Scripts.Laser.Dto;
using LaserAssetPackage.Scripts.Laser.Logic.Query;
using UnityEngine;

namespace LaserAssetPackage.Scripts.Laser.Scripting
{
    

    /// <summary>
    /// Example to show that by extending the <see cref="LaserActorAware"/> class all the queries and events inside it are made available.
    /// To see the full list of available queries, see <see cref="LaserActorAware"/>. 
    /// </summary>
    public class ScriptingExample : LaserActorAware
    {

        public bool iscomplete = false;
        public IQueryableLaserTarget _mySelectedTarget;
        public GameObject button;
        public GameObject magic;
        private void Start()
        {
            SubscribeToSomeRandomEvents();
            FindMyTargetAndSubscribeToIt();
        }

        private void FindMyTargetAndSubscribeToIt()
        {
            _mySelectedTarget = FindLaserActorByRootName<IQueryableLaserTarget>("nonblocking_receiver1");
            _mySelectedTarget = FindLaserActorByRootName<IQueryableLaserTarget>("nonblocking_receiver2");
            _mySelectedTarget = FindLaserActorByRootName<IQueryableLaserTarget>("nonblocking_receiver3");
            _mySelectedTarget.OnNewEmitterReceived += Method_To_Run_When_New_Emitter_Hits_My_Target;
        }
        

        private void Method_To_Run_When_New_Emitter_Hits_My_Target(IQueryableLaserReceiver sender, LaserHit laserHit)
        {
            AudioSource laser = GetComponent<AudioSource>();
            laser.Play();
            UnityEngine.Debug.Log("레이저");
            iscomplete = true;


        }

        private void SubscribeToSomeRandomEvents()
        {
            OnAllTargetsHit += LevelComplete;
            OnAnyReceiverHit += ReceiverHit;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            OnAllTargetsHit -= LevelComplete;
            OnAnyReceiverHit -= ReceiverHit;
        }

        private void LevelComplete()
        {
            UnityEngine.Debug.Log("yay level complete");
        }

        private void ReceiverHit(IQueryableLaserReceiver receiver)
        {
            int emittersHittingThisReceiver = GetNumberOfAffectingEmitters(receiver);
            if (emittersHittingThisReceiver > 1)
            {
                UnityEngine.Debug.Log("that receiver will burn");
            }
        }
    }
}