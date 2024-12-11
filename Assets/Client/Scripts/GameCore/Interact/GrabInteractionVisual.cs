using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace Client
{
    [RequireComponent(typeof(XRGrabInteractable))]
    public class GrabInteractionVisual : MonoBehaviour
    {
        private XRGrabInteractable _grabInteractable;
        private LineRenderer _lineRenderer;

        private bool _isSelected;

        private void Awake()
        {
            _grabInteractable = GetComponent<XRGrabInteractable>();
            _lineRenderer = GetComponentInChildren<LineRenderer>();
        }

        private void Start()
        {
            _lineRenderer.gameObject.SetActive(false);
            _lineRenderer.positionCount = 2;
            _lineRenderer.useWorldSpace = true;
        }

        private void OnEnable()
        {
            _grabInteractable.selectEntered.AddListener(OnSelectEnter);
            _grabInteractable.selectExited.AddListener(OnSelectExit);
        }

        private void OnDisable()
        {
            _grabInteractable.selectEntered.RemoveListener(OnSelectEnter);
            _grabInteractable.selectExited.RemoveListener(OnSelectExit);
        }

        //Todo: I should switch to IUpdateMono for optimization
        private void Update()
        {
            if (!_isSelected)
                return;

            if (Physics.Raycast(transform.position, Vector3.down, out var hit, Mathf.Infinity))
            {
                _lineRenderer.SetPosition(0, transform.position);
                _lineRenderer.SetPosition(1, hit.point);
            }
        }

        private void OnSelectEnter(SelectEnterEventArgs arg0)
        {
            _isSelected = true;
            _lineRenderer.gameObject.SetActive(true);
        }

        private void OnSelectExit(SelectExitEventArgs arg0)
        {
            _isSelected = false;
            _lineRenderer.gameObject.SetActive(false);
        }
    }
}