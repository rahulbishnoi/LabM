using System.Drawing;
using C1.Win.C1FlexGrid;

namespace FlexGridHelper
{
    
     class MyRenderer : C1.Win.C1FlexGrid.GridRendererOffice2007Blue
    {
        public override void OnDrawCell(C1FlexGridBase flex, OwnerDrawCellEventArgs e, C1FlexGridRenderer.CellType cellType)
        {
            try
            {

                if (cellType == CellType.Highlight)
                {
                    if (e.Style.Name == "Modified")
                    {  
                        cellType = CellType.RowHeaderSelectedHot;

                        CellRange rg = flex.GetCellRange(e.Row, 1, e.Row, flex.Cols.Count - 1);
                        rg.Style = flex.Styles["Modified"]; 
                    }
                    else
                    if (e.Style.Name == "Added")
                    {
                        cellType = CellType.RowHeaderSelectedHot;

                        CellRange rg = flex.GetCellRange(e.Row, 1, e.Row, flex.Cols.Count - 1);
                        rg.Style = flex.Styles["Added"];
                    }
                    else
                    if (e.Style.Name == "Detached")
                    {
                        cellType = CellType.RowHeaderSelectedHot;

                        CellRange rg = flex.GetCellRange(e.Row, 1, e.Row, flex.Cols.Count - 1);
                        rg.Style = flex.Styles["Detached"];
                    }
                    else
                    {
                       cellType = CellType.Normal;
                       // cellType = CellType.ColumnHeaderSelected;
                    }
                }

                base.OnDrawCell(flex, e, cellType);
            }catch{}
        }
    }
	
	
}
