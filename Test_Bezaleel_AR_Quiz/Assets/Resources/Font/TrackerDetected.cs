using UnityEngine;
using Vuforia;


public class TrackerDetected : MonoBehaviour, ITrackableEventHandler
{

    #region PROTECTED_MEMBER_VARIABLES

    protected TrackableBehaviour mTrackableBehaviour;
    protected TrackableBehaviour.Status m_PreviousStatus;
    protected TrackableBehaviour.Status m_NewStatus;

    #endregion // PROTECTED_MEMBER_VARIABLES

    #region UNITY_MONOBEHAVIOUR_METHODS

    protected virtual void Start()
    {
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
    }

    protected virtual void OnDestroy()
    {
        if (mTrackableBehaviour)
            mTrackableBehaviour.UnregisterTrackableEventHandler(this);
    }

    #endregion // UNITY_MONOBEHAVIOUR_METHODS

    #region PUBLIC_METHODS

    public void OnTrackableStateChanged(
        TrackableBehaviour.Status previousStatus,
        TrackableBehaviour.Status newStatus)
    {

        m_PreviousStatus = previousStatus;
        m_NewStatus = newStatus;

        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {

            //Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " found");
            OnTrackerDetected(mTrackableBehaviour.TrackableName);

        }
        else if (previousStatus == TrackableBehaviour.Status.TRACKED &&
                 newStatus == TrackableBehaviour.Status.NO_POSE)
        {

            //Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");
            OnTrackerLost();

        }
        else
        {

            OnTrackerLost();

        }

    }

    #endregion // PUBLIC_METHODS

    #region EDITING_SITE_PROTECTED_METHODS

    protected void OnTrackerDetected(string trackerName)
    {
        PlayerPrefs.SetString("TrackerName", trackerName);
        //Debug.Log("<color=red>Tracker Name : " + trackerName + "</color>");
    }

    protected void OnTrackerLost()
    {
        PlayerPrefs.SetString("TrackerName", "");
    }

    #endregion

}
