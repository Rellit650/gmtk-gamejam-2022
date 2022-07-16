using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room
{
    public Vector2Int roomCoordinate;
    public Dictionary<string, Room> neighbors;
    public Room(int xCoordinate, int yCoordinate)
    {
        roomCoordinate = new Vector2Int(xCoordinate, yCoordinate);
        neighbors = new Dictionary<string, Room>();
    }
    public Room(Vector2Int roomCoordinate)
    {
        this.roomCoordinate = roomCoordinate;
        neighbors = new Dictionary<string, Room>();
    }
    public List<Vector2Int> NeighborCoordinates()
    {
        List<Vector2Int> neighborCoordinates = new List<Vector2Int>();
        neighborCoordinates.Add(new Vector2Int(roomCoordinate.x, roomCoordinate.y - 1));
        neighborCoordinates.Add(new Vector2Int(roomCoordinate.x + 1, roomCoordinate.y));
        neighborCoordinates.Add(new Vector2Int(roomCoordinate.x, roomCoordinate.y + 1));
        neighborCoordinates.Add(new Vector2Int(roomCoordinate.x - 1, roomCoordinate.y));
        return neighborCoordinates;
    }
    public void Connect(Room neighbor)
    {
        string direction = "";
        if (neighbor.roomCoordinate.y < roomCoordinate.y)
        {
            direction = "N";
        }
        if (neighbor.roomCoordinate.x > roomCoordinate.x)
        {
            direction = "E";
        }
        if (neighbor.roomCoordinate.y > roomCoordinate.y)
        {
            direction = "S";
        }
        if (neighbor.roomCoordinate.x < roomCoordinate.x)
        {
            direction = "W";
        }
        neighbors.Add(direction, neighbor);
    }

}
