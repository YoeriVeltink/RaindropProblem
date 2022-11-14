using System;
using System.Collections.Generic;
class World{

    static void Tick(ref Human testSubject, ref List<Raindrop> raindrops, float deltaTime, int travelDistance, int interRaindropDistance)
    {
        Console.Write("TICK   Raindrop count: ");
        Console.Write(raindrops.Count());
        Console.Write("--");
        Console.Write(testSubject.wetness);
        Console.Write(" raindrops collided --- Distance walked: ");
        Console.Write(testSubject.getLeftBorder());
        Console.Write(" Out of ");
        Console.Write(travelDistance);
        List<Raindrop> collidedRaindrops = new List<Raindrop>();
        int topCollisions = 0;
        int sideCollisions = 0;
        foreach (Raindrop raindrop in raindrops)
        {
            raindrop.move(deltaTime);
            if (raindrop.xPos > testSubject.getLeftBorder() && raindrop.xPos < testSubject.getRightBorder()
                && raindrop.yPos > testSubject.getBottomBorder() && raindrop.yPos < testSubject.getTopBorder())
            {
                //collision with Human
                testSubject.feelsRaindrop();
                collidedRaindrops.Add(raindrop);
                topCollisions++;
            }
            else if (raindrop.yPos < 0)
                collidedRaindrops.Add(raindrop); //collision with ground
        }

        testSubject.move(deltaTime);

        foreach (Raindrop raindrop in raindrops)
        {
            raindrop.move(deltaTime);
            if (raindrop.xPos > testSubject.getLeftBorder() && raindrop.xPos < testSubject.getRightBorder()
                && raindrop.yPos > testSubject.getBottomBorder() && raindrop.yPos < testSubject.getTopBorder())
            {
                //collision with Human
                testSubject.feelsRaindrop();
                collidedRaindrops.Add(raindrop);
                sideCollisions++;
            }
            else if (raindrop.yPos < 0)
                collidedRaindrops.Add(raindrop); //collision with ground
        }

        foreach (Raindrop collidedRaindrop in collidedRaindrops)
        {
            raindrops.Remove(collidedRaindrop);
        }

        for(int i = testSubject.getLeftBorder(); i <= travelDistance; i += interRaindropDistance)
        {
            raindrops.Add(new Raindrop(i));
        }

        Console.WriteLine("---Top: " + topCollisions + " --- Side: " + sideCollisions);
    }

    static void Main(string[] args){

        int interRaindropDistance = int.Parse(args[0]); //10 centimeter in millimeters
        int travelDistance = 100000; //100 meters in millimeters

        Human testSubject1 = new Human(1000); //walking
        Human testSubject2 = new Human(5000); //running

        List<Raindrop> raindrops = new List<Raindrop>();

        float deltaTime = 0.01f; //one hundredth of a second

        Console.WriteLine("START PROGRAM");
        while(testSubject1.getLeftBorder() < travelDistance)
        {
            Tick(ref testSubject1, ref raindrops, deltaTime, travelDistance, interRaindropDistance);
        }
        Console.WriteLine("Walking wetness " + testSubject1.wetness);
        raindrops = new List<Raindrop>();
        while(testSubject2.getLeftBorder() < travelDistance)
        {
            Tick(ref testSubject2, ref raindrops, deltaTime, travelDistance, interRaindropDistance);
        }
        Console.WriteLine("Walking wetness " + testSubject1.wetness);
        Console.WriteLine("Running wetness " + testSubject2.wetness);
        Console.WriteLine("END PROGRAM");
    }
}

//Human is a 2D 2-by-1 meter rectangle (hitbox) position coord is topleft corner 
public class Human
{
    public int wetness { get; private set; } = 0;

    int thickness = 1000;
    int height = 2000;
    int xPos = -1;
    int yPos = 2;


    int speed; //movement is one dimensional and positive along x-axis in millimeters per second

    public int getLeftBorder() { return xPos; }
    public int getRightBorder() { return xPos+thickness; }
    public int getTopBorder() { return yPos; }
    public int getBottomBorder() { return yPos-height; }

    public void move(float deltaTime)
    {
        xPos += (int)(speed * deltaTime);
    }

    public void feelsRaindrop()
    {
        wetness++;
    }

    public Human(int speed)
    {
        this.speed = speed;
    }
}


public class Raindrop
{
    public int xPos { get; private set; }
    public int yPos { get; private set; }

    int speed = -9000; //movement is one dimensional and negative along y-axis in milimeters per second 

    public void move(float deltaTime)
    {
        yPos += (int)(speed * deltaTime);
    }

    public Raindrop(int startX)
    {
        xPos = startX;
        yPos = 2001;
    }
}