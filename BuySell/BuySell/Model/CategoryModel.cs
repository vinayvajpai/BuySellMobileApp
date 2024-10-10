using System;
using System.Collections.Generic;

namespace BuySell.Model
{
    public class CategoryModel
    {
        public class Roots
        {
            public string Root { get; set; }
            public Dictionary<Nodes, List<SubRoots>> Node { get; set; }

        }
        public class Nodes : Roots
        {
            public string NodeTitle { get; set; }
            public string Gender { get; set; }
            public bool IsShowMore { get; set; }
        }
        public class SubRoots : Nodes
        {
            public string SubRoot { get; set; }
        }
        public class Key : Nodes
        {
           
        }

        public class SelectSubRootCategory
        {
            public Key Key { get; set; }
            public List<object> Value { get; set; }
        }
    }
}
