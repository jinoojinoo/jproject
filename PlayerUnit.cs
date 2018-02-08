using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnit : UnitBase
{
    private Rigidbody m_myRigidbody;
    private Rigidbody MyRigidbody
    {
        get
        {
            if (m_myRigidbody == null)
                m_myRigidbody = this.GetComponent<Rigidbody>();
            return m_myRigidbody;
        }
    }
    public override void Start()
    {
        base.Start();
        UnitManager.Instance.AddUnit(this);
    }


    public float m_unitSpeed = 1.0f;
    private Vector2 m_unitDir = Vector2.zero;

    public Transform m_movetargetCamera;
    private const int MAX_MOVE = 50;
    private Vector2 m_unitPosition = Vector2.zero;



    protected override void UpdateMove()
    {
        Vector3 curpos = MyTrans.localPosition;

        Vector2 dirx = m_unitDir;
        dirx.y = 0.0f;
        Vector2 diry = m_unitDir;
        diry.x = 0.0f;

        if (Physics2D.Raycast(curpos, dirx, MAX_MOVE))
            m_unitDir.x = 0.0f;
        if (Physics2D.Raycast(curpos, diry, MAX_MOVE))
            m_unitDir.y = 0.0f;

        Debug.Log("m_incollision : " + m_incollision + " , cur curpos  : " + curpos);
         
        m_unitPosition.x += m_unitDir.x * Time.deltaTime * m_unitSpeed;
        int stepx = (int)(m_unitPosition.x / MAX_MOVE);
        m_unitPosition.x = m_unitPosition.x - (stepx * MAX_MOVE);
        curpos.x += stepx * MAX_MOVE;

        m_unitPosition.y += m_unitDir.y * Time.deltaTime * m_unitSpeed;
        int stepy = (int)(m_unitPosition.y / MAX_MOVE);
        m_unitPosition.y = m_unitPosition.y - (stepy * MAX_MOVE);
        curpos.y += stepy * MAX_MOVE;

        Debug.Log("stepx : " + stepx + " , stepy : " + stepy);

        if (curpos.x < 0.0f)
            curpos.x = 0.0f;
        if (curpos.y < 0.0f)
            curpos.y = 0.0f;

        MyTrans.localPosition = curpos;
        m_movetargetCamera.localPosition = MyTrans.localPosition;
    }

    private bool m_incollision = false;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        m_incollision = true;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        m_incollision = false;
    }

    protected override void UpdateMoveDir()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            m_unitDir.x = -1;
        if (Input.GetKeyDown(KeyCode.RightArrow))
            m_unitDir.x = 1;
        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            m_unitDir.x = 0;
            m_unitPosition.x = 0.0f;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
            m_unitDir.y = 1;
        if (Input.GetKeyDown(KeyCode.DownArrow))
            m_unitDir.y = -1;
        if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow))
        {
            m_unitDir.y = 0;
            m_unitPosition.y = 0.0f;
        }

    }

    private int m_number;
    private void MakeTarget()
    {
        float x = m_number % 2 == 0 ? -100.0f : 100.0f;

    }
}