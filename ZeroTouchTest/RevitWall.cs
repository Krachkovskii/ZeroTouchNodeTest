using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Wall = Autodesk.Revit.DB.Wall;
using RevitServices.Persistence;
using RevitServices.Transactions;
using DN = Revit.Elements;

namespace ZeroTouchTest
{
    internal class RevitWall
    {
        private RevitWall(){}

        public Autodesk.Revit.DB.Wall Create(Autodesk.DesignScript.Geometry.Curve dynamoCurve, DN.Level lvl, bool structural)
        {
            Document doc = DocumentManager.Instance.CurrentDBDocument;

            Level revitLevel = lvl.InternalElement as Level;
            ElementId lvlId = revitLevel.Id;

            Curve crv = Revit.GeometryConversion.ProtoToRevitCurve.ToRevitType(dynamoCurve, true);


            TransactionManager.Instance.EnsureInTransaction(doc);

            Wall NewWall = Wall.Create(doc, crv, lvlId, structural);

            TransactionManager.Instance.TransactionTaskDone();

            return NewWall;
        }
    }
}
