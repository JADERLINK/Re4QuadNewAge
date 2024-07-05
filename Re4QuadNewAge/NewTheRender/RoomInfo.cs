using System;
using System.Collections.Generic;
using System.Text;
using Re4QuadExtremeEditor.src.JSON;

namespace NewAgeTheRender
{
    public class RoomInfo
    {
        public RoomListObj RoomListObj { get; private set; }
        public Dictionary<string, RoomModel> RoomModelDict { get; private set; }

        public RoomInfo(RoomListObj RoomListObj, Dictionary<string, RoomModel> RoomModelDict) 
        {
            this.RoomModelDict = RoomModelDict;
            this.RoomListObj = RoomListObj;
        }

        public override string ToString()
        {
            return RoomListObj?.ToString() ?? "Null";
        }

        public override bool Equals(object obj)
        {
            return obj is RoomInfo r && r.RoomListObj.Equals(RoomListObj);
        }

        public override int GetHashCode()
        {
            return RoomListObj.GetHashCode();
        }

    }
}
