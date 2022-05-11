using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BezierCurveDrawer : MonoBehaviour
{
    [SerializeField] public List<RectTransform> points;    // 베지어 곡선의 경로를 결정하는 점들 (p0, p1, p2)
    [SerializeField] private List<RectTransform> arrows;    // 베지어 곡선을 이루는 화살표들

    [Header("스케일")]
    [SerializeField] private float minScale;    // 화살표 최소 크기
    [SerializeField] private float maxScale;    // 화살표 최대 크기

    [Header("색상")]
    [SerializeField] private Color baseColor;       // 기본 색상
    [SerializeField] private Color highlightColor;  // 강조 색상

    private RectTransform rectTransform;
    private Camera mainCam;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        mainCam = Camera.main;
    }
    private void LateUpdate()
    {
        // 마우스 좌표를 현재 캔버스의 좌표로 변경한다.
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rectTransform, Input.mousePosition,
            mainCam, out var mousePos);

        // 베지어 곡선의 끝점을 마우스 좌표로 설정한다.
        points[2].anchoredPosition = mousePos;

        // 베지어 곡선을 그린다.
        DrawBezier();
    }

    public void SetHighLight(bool enable)
    {
        // 강조 여부에 따른 색상
        var color = enable ? highlightColor : baseColor;

        // 화살표 전체의 색상을 변경함
        foreach (var arrow in arrows)
            arrow.GetComponent<Image>().color = color;
    }

    private Vector2 CurvePoint(float t)
    {
        // 포인트가 3개보다 적다면, 종료
        if (points.Count < 3) return Vector2.zero;

        // B(t) = (1 − t)^2 * P0 + 2 * t * (1 - t) * P1 + t^2 * P2
        var s = 1 - t;
        var p0 = Mathf.Pow(s, 2) * points[0].anchoredPosition;
        var p1 = (2 * t * s) * points[1].anchoredPosition;
        var p2 = Mathf.Pow(t, 2) * points[2].anchoredPosition;

        // 최종 값 반환
        return p0 + p1 + p2;
    }

    private void DrawBezier()
    {
        // 화살표 개수가 2개보다 적다면, 종료
        if (arrows.Count < 2) return;

        // 화살표 개수의 역수를 구함
        var reverse = 1f / (arrows.Count - 1);

        // 전체 화살표를 순회하며
        for (var i = 0; i < arrows.Count; i++)
        {
            // 화살표 위치를 베지어 방정식을 이용하여 계산
            arrows[i].anchoredPosition = CurvePoint(i * reverse);

            // i가 0이 아닐 때,
            if (i > 0)
            {
                // 바로 전 화살표 위치를 이용하여 회전 벡터를 구함
                var rot = (arrows[i].anchoredPosition - arrows[i - 1].anchoredPosition).normalized;

                // FromToRotation을 이용하여 쿼터니언 각으로 변경하여 대입
                arrows[i].localRotation = Quaternion.FromToRotation(Vector3.right, rot);
            }
        }
    }

    // 스케일을 조정하는 함수
    private void ReScale()
    {
        // 화살표 개수가 1개보다 적다면, 종료
        if (arrows.Count < 1) return;

        // 크기 간격 계산
        var interval = (maxScale - minScale) / arrows.Count;

        // 화살표 크기를 순차적으로 설정해줌
        for (var i = 0; i < arrows.Count; i++)
            arrows[i].localScale = Vector3.one * (minScale + interval * i);
    }

    // 인스펙터에서 표시될 수 있도록 OnDrawGizmos를 이용함
    private void OnDrawGizmos()
    {
        // 베지어 곡선 그리기
        DrawBezier();
    }

    // 인스펙터상에서 변수의 값이 변경될 때 호출된다.
    private void OnValidate()
    {
        // 스케일 재조정
        ReScale();

        // 하이라이트 끄기
        SetHighLight(false);
    }
}
