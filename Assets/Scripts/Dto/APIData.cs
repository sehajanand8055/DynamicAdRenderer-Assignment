using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using Newtonsoft.Json;

namespace Dto
{
    [System.Serializable]
    public class APIData
    {
        public List<Layer> layers;
    }

    [System.Serializable]
    public class Layer
    {
        public string type;
        public string path;
        public List<Operation> operations;
        public List<Placement> placement;
    }

    [System.Serializable]
    public class Placement
    {
        public Position position;
        
    }

    [System.Serializable]
    public class Position
    {
        public int x;
        public int y;
        public int width;
        public int height;
    }

    [System.Serializable]
    public class Operation
    {
        public string name;
        public string argument;
    }
}