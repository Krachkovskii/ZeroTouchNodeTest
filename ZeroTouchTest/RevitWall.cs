using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DB = Autodesk.Revit.DB;

namespace ZeroTouchTest
{
    internal class RevitWall
    {
        private RevitWall(){}

        public DB.Wall Create()
        {
            DB.Wall NewWall = DB.Wall.Create(DB.Document doc, DB.Curve curve, DB.ElementId typeId, DB.ElementId levelId, double height, double offset, bool flip, bool structural);
            return NewWall;
        }
    }
}
