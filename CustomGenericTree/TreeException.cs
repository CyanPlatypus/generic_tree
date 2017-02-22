using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GenericTree
{
    public class TreeNodeWithoutParentIsNotARootException : Exception
    {
        public TreeNodeWithoutParentIsNotARootException() { }
        public TreeNodeWithoutParentIsNotARootException(string message) : base(message) { }
        public TreeNodeWithoutParentIsNotARootException(string message, Exception inner) : base(message, inner) { }
        protected TreeNodeWithoutParentIsNotARootException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

    public class AttemptToRemoveTreeRoot : Exception
    {
        public AttemptToRemoveTreeRoot() { }
        public AttemptToRemoveTreeRoot(string message) : base(message) { }
        public AttemptToRemoveTreeRoot(string message, Exception inner) : base(message, inner) { }
        protected AttemptToRemoveTreeRoot(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

    public class TheSameChildOfOneParentException : Exception
    {
        public TheSameChildOfOneParentException() { }
        public TheSameChildOfOneParentException(string message) : base(message) { }
        public TheSameChildOfOneParentException(string message, Exception inner) : base(message, inner) { }
        protected TheSameChildOfOneParentException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

    public class ParentIsAChildException : Exception
    {
        public ParentIsAChildException() { }
        public ParentIsAChildException(string message) : base(message) { }
        public ParentIsAChildException(string message, Exception inner) : base(message, inner) { }
        protected ParentIsAChildException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
