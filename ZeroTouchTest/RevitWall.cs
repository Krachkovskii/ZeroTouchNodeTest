using Autodesk.Revit.DB;
using Wall = Autodesk.Revit.DB.Wall;
using RevitServices;
using RevitServices.Persistence;
using RevitServices.Transactions;

namespace ZeroTouchTest
{
    internal class RevitWall
    {
        private RevitWall(){}

        public Wall Create(Autodesk.DesignScript.Geometry.Curve dynamoCurve, Revit.Elements.Level lvl, bool structural)
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
