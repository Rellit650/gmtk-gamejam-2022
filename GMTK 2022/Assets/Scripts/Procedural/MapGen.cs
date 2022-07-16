using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGen : MonoBehaviour
{

    [SerializeField]
    private int numberOfRooms;
    private Room[,] rooms;
    void Start()
    {
        GenerateDungeon();
        PrintGrid();
    }
    // Generate the floor
    private Room GenerateDungeon()
    {
        // Setup of all necessary containers
        int gridSize = numberOfRooms;
        rooms = new Room[gridSize, gridSize];
        Vector2Int initialRoomCoordinate = new Vector2Int((gridSize / 2) - 1, (gridSize / 2) - 1);
        Queue<Room> roomsToCreate = new Queue<Room>();
        roomsToCreate.Enqueue(new Room(initialRoomCoordinate.x, initialRoomCoordinate.y));
        List<Room> createdRooms = new List<Room>();

        // Generating necessary rooms + assigning neighbors
        while (roomsToCreate.Count > 0 && createdRooms.Count < numberOfRooms)
        {
            Room currentRoom = roomsToCreate.Dequeue();
            rooms[currentRoom.roomCoordinate.x, currentRoom.roomCoordinate.y] = currentRoom;
            createdRooms.Add(currentRoom);
            AddNeighbors(currentRoom, roomsToCreate);
        }

        // Connecting neighbors
        foreach (Room room in createdRooms)
        {
            List<Vector2Int> neighborCoordinates = room.NeighborCoordinates();
            foreach (Vector2Int coordinate in neighborCoordinates)
            {
                Room neighbor = rooms[coordinate.x, coordinate.y];
                if (neighbor != null)
                {
                    room.Connect(neighbor);
                }
            }
        }
        return rooms[initialRoomCoordinate.x, initialRoomCoordinate.y];
    }

    // Assigning neighbors
    private void AddNeighbors(Room currentRoom, Queue<Room> roomsToCreate)
    {
        List<Vector2Int> neighborCoordinates = currentRoom.NeighborCoordinates();
        List<Vector2Int> availableNeighbors = new List<Vector2Int>();
        foreach (Vector2Int coordinate in neighborCoordinates)
        {
            if (rooms[coordinate.x, coordinate.y] == null)
            {
                availableNeighbors.Add(coordinate);
            }
        }

        int numberOfNeighbors = Random.Range(1, availableNeighbors.Count);
        for (int neighborIndex = 0; neighborIndex < numberOfNeighbors; neighborIndex++)
        {
            float randomNumber = Random.value;
            float roomFrac = 1f / availableNeighbors.Count;
            Vector2Int chosenNeighbor = new Vector2Int(0, 0);
            foreach (Vector2Int coordinate in availableNeighbors)
            {
                if (randomNumber < roomFrac)
                {
                    chosenNeighbor = coordinate;
                    break;
                }
                else
                {
                    roomFrac += 1f / availableNeighbors.Count;
                }
            }
            roomsToCreate.Enqueue(new Room(chosenNeighbor));
            availableNeighbors.Remove(chosenNeighbor);
        }
    }

    // Debug to print grid, can be used later for placement/generation
    private void PrintGrid()
    {
        for (int rowIndex = 0; rowIndex < rooms.GetLength(1); rowIndex++)
        {
            string row = "";
            for (int columnIndex = 0; columnIndex < rooms.GetLength(0); columnIndex++)
            {
                if (rooms[columnIndex, rowIndex] == null)
                {
                    row += "X";
                }
                else
                {
                    row += "R";
                }
            }
            Debug.Log(row);
        }
    }
}
