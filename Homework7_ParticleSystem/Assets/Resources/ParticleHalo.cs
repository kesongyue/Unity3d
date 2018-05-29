using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleHalo : MonoBehaviour {
    private ParticleSystem particleSys;
    private ParticleSystem.Particle[] particleArray;
    private ParticleData[] particleData;

    public int count = 10000;       // 粒子数量  
    public float size = 0.03f;      // 粒子大小  
    public float minRadius = 6.0f;  // 最小半径  
    public float maxRadius = 10.0f; // 最大半径  
    public bool clockwise = true;   // 顺时针|逆时针  
    public float speed = 2f;        // 速度  
    public float pingPong = 0.02f;  // 游离范围  
    private int diff = 10;  // 师兄博客说是速度差分层数  

    public Gradient colorGradient;
    private void Start()
    {
        
        particleArray = new ParticleSystem.Particle[count];
        particleData = new ParticleData[count];

        particleSys = this.GetComponent<ParticleSystem>();
        particleSys.startSpeed = 0;
        particleSys.startSize = size;
        particleSys.loop = false;
        particleSys.maxParticles = count;
        particleSys.Emit(count);
        particleSys.GetParticles(particleArray);

        setParticlePosition();
    }

    void setParticlePosition()
    {
        float midRadius, minRate, maxRate, radius,angle,theta,time;
        //初始化粒子位置；
        for (int i = 0; i < count; i++)
        {
            midRadius = (maxRadius + minRadius) / 2;
            minRate = Random.Range(1.0f, midRadius / minRadius);
            maxRate = Random.Range(midRadius / maxRadius, 1.0f);
            radius = Random.Range(minRadius * minRate, maxRadius * maxRate);
            angle = Random.Range(0.0f, 360.0f);
            theta = angle / 180 * Mathf.PI;
            time = Random.Range(0.0f, 360.0f);

            particleData[i] = new ParticleData(radius,angle,time);
            //particleArray[i].position = new Vector3(particleData[i].radius * Mathf.Cos(theta), 0f, particleData[i].radius * Mathf.Sin(theta));
           particleArray[i].position = new Vector3(particleData[i].radius * Mathf.Cos(theta), particleData[i].radius * Mathf.Sin(theta),0f);
        }
        particleSys.SetParticles(particleArray, particleArray.Length);
    }

    private void Update()
    {
        float theta;
        for (int i = 0; i < count; i++)
        {
            if (clockwise)
            {
                particleData[i].angle -= (i % diff + 1) * (speed / particleData[i].radius / diff);
            }
            else
            {
                particleData[i].angle += (i % diff + 1) * (speed / particleData[i].radius / diff);
            }
            // 保证angle在到360度，这个方法有点技巧。
            particleData[i].angle = (360.0f + particleData[i].angle) % 360.0f;
            theta = particleData[i].angle / 180 * Mathf.PI;
            particleData[i].time += Time.deltaTime;
            particleData[i].radius += Mathf.PingPong(particleData[i].time / minRadius / maxRadius, pingPong) - pingPong / 2.0f;
           // particleArray[i].position = new Vector3(particleData[i].radius * Mathf.Cos(theta), 0f, particleData[i].radius * Mathf.Sin(theta));
             particleArray[i].position = new Vector3(particleData[i].radius * Mathf.Cos(theta), particleData[i].radius * Mathf.Sin(theta), 0f);


        }
        changeColor();
        particleSys.SetParticles(particleArray, particleArray.Length);
    }

    void changeColor()
    {
        float colorValue;
        for (int i = 0; i < count; i++)
        {
            //改变颜色
            colorValue = (Time.realtimeSinceStartup - Mathf.Floor(Time.realtimeSinceStartup));
            colorValue += particleData[i].angle/360;
            while (colorValue > 1)
                colorValue--;
            particleArray[i].color = colorGradient.Evaluate(colorValue);
            //particleArray[i].color = colorGradient.Evaluate(Random.value);
        }
    }
}

public class ParticleData
{
    public float radius, angle, time;
    public ParticleData(float radius_,float angle_,float time_)
    {
        radius = radius_;  //半径
        angle = angle_;     //角度
        time = time_;       //开始运动的时间
    }
}
